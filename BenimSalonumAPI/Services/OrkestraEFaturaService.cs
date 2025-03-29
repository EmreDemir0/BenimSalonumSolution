using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;
using BenimSalonum.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using BenimSalonumAPI.DataAccess.Context;
using Microsoft.Extensions.Logging;

namespace BenimSalonumAPI.Services
{
    public class OrkestraEFaturaService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly BenimSalonumContext _context;
        private readonly ILogger<OrkestraEFaturaService> _logger;
        private readonly string _baseUrl;
        private readonly string _username;
        private readonly string _password;

        public OrkestraEFaturaService(
            HttpClient httpClient,
            IConfiguration configuration,
            BenimSalonumContext context,
            ILogger<OrkestraEFaturaService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _context = context;
            _logger = logger;

            // Orkestra API Bilgileri
            _baseUrl = configuration["OrkestraEFatura:BaseUrl"] ?? "https://efaturacim.orkestra.com.tr/servis.php";
            _username = configuration["OrkestraEFatura:Username"] ?? "ak.emredemir@gmail.com";
            _password = configuration["OrkestraEFatura:Password"] ?? "Emre1502";
        }

        /// <summary>
        /// Orkestra API'sine giriş yapar ve token alır
        /// </summary>
        /// <returns>Token bilgisi</returns>
        public async Task<string> GetTokenAsync()
        {
            try
            {
                var requestData = new
                {
                    cmd = "login",
                    user = _username,
                    pass = _password
                };

                var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync(_baseUrl, content);
                response.EnsureSuccessStatusCode();
                
                var responseString = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<Dictionary<string, object>>(responseString);
                
                if (responseObject != null && responseObject.ContainsKey("token"))
                {
                    return responseObject["token"].ToString() ?? string.Empty;
                }
                
                _logger.LogError($"Orkestra API giriş hatası: Token alınamadı. Yanıt: {responseString}");
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Orkestra API giriş hatası: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// E-Fatura/E-Arşiv faturası oluşturup Orkestra API'sine gönderir
        /// </summary>
        /// <param name="faturaId">Fatura ID</param>
        /// <returns>İşlem sonucu</returns>
        public async Task<(bool Success, string Message, string UUID)> SendEFaturaAsync(int faturaId)
        {
            try
            {
                // Token al
                var token = await GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    return (false, "Orkestra API'sine giriş yapılamadı.", string.Empty);
                }

                // Fatura bilgilerini al
                var fatura = await _context.Faturalar
                    .Include(f => f.FaturaDetaylari)
                    .FirstOrDefaultAsync(f => f.Id == faturaId);

                if (fatura == null)
                {
                    return (false, "Fatura bulunamadı.", string.Empty);
                }

                // Cari bilgilerini al
                var cari = await _context.Cariler
                    .FirstOrDefaultAsync(c => c.Id == fatura.CariId);

                if (cari == null)
                {
                    return (false, "Cari bilgileri bulunamadı.", string.Empty);
                }

                // XML oluştur
                var faturaXml = CreateEFaturaXml(fatura, cari);

                // İşlem tipini belirle (e-Fatura veya e-Arşiv)
                var islemTipi = fatura.FaturaTipi == 2 ? "efatura" : "earsiv";

                // ETTN numarası oluştur (Elektronik Ticaret Takip Numarası)
                var ettn = Guid.NewGuid().ToString();

                // İstek gövdesini hazırla
                var requestData = new
                {
                    cmd = "sendXML",
                    token = token,
                    tip = islemTipi,
                    ettn = ettn,
                    xml = faturaXml
                };

                var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync(_baseUrl, content);
                response.EnsureSuccessStatusCode();
                
                var responseString = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<Dictionary<string, object>>(responseString);
                
                // Log kaydet
                var log = new EFaturaLogTable
                {
                    FaturaId = faturaId,
                    IslemTuru = fatura.FaturaTipi,
                    IslemDurumu = 2, // Gönderildi
                    UUID = ettn,
                    BelgeNo = fatura.FaturaNo,
                    RequestData = JsonSerializer.Serialize(requestData).Substring(0, Math.Min(499, JsonSerializer.Serialize(requestData).Length)),
                    ResponseData = responseString.Substring(0, Math.Min(499, responseString.Length)),
                    EttnNo = ettn,
                    KullaniciId = 1 // Sistem kullanıcısı
                };

                if (responseObject != null && responseObject.ContainsKey("status") && responseObject["status"].ToString() == "success")
                {
                    // Başarılı yanıt
                    log.IslemDurumu = 4; // Başarılı
                    
                    if (responseObject.ContainsKey("uuid"))
                    {
                        log.UUID = responseObject["uuid"].ToString();
                    }
                    
                    if (responseObject.ContainsKey("pdf"))
                    {
                        log.PdfUrl = responseObject["pdf"].ToString();
                    }
                    
                    if (responseObject.ContainsKey("xml"))
                    {
                        log.XmlUrl = responseObject["xml"].ToString();
                    }
                    
                    if (responseObject.ContainsKey("zrf"))
                    {
                        log.ZrfUrl = responseObject["zrf"].ToString();
                    }

                    // Faturayı güncelle
                    fatura.FaturaDurumu = 3; // E-Fatura Gönderildi
                    fatura.EfaturaGuid = log.UUID;
                    fatura.EfaturaGonderimTarihi = DateTime.Now;
                    fatura.EfaturaBasarili = true;
                    fatura.EfaturaPdf = log.PdfUrl;
                    fatura.EfaturaXml = log.XmlUrl;
                    fatura.GuncellenmeTarihi = DateTime.Now;
                    fatura.GuncelleyenKullaniciId = 1; // Sistem kullanıcısı

                    await _context.SaveChangesAsync();
                    
                    _context.EFaturaLoglari.Add(log);
                    await _context.SaveChangesAsync();
                    
                    return (true, "E-Fatura başarıyla gönderildi.", log.UUID ?? string.Empty);
                }
                else
                {
                    // Başarısız yanıt
                    log.IslemDurumu = 3; // Başarısız
                    log.Aciklama = responseObject != null && responseObject.ContainsKey("message") 
                        ? responseObject["message"].ToString() 
                        : "Bilinmeyen hata";
                    
                    _context.EFaturaLoglari.Add(log);
                    await _context.SaveChangesAsync();
                    
                    return (false, log.Aciklama ?? "E-Fatura gönderimi başarısız.", string.Empty);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"E-Fatura gönderim hatası: {ex.Message}");
                return (false, $"E-Fatura gönderim hatası: {ex.Message}", string.Empty);
            }
        }

        /// <summary>
        /// E-Fatura XML içeriğini oluşturur
        /// </summary>
        /// <param name="fatura">Fatura</param>
        /// <param name="cari">Cari</param>
        /// <returns>XML içeriği</returns>
        private string CreateEFaturaXml(FaturaTable fatura, CariTable cari)
        {
            try
            {
                // UBL-TR formatında XML oluştur
                var ns = XNamespace.Get("urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
                var cac = XNamespace.Get("urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                var cbc = XNamespace.Get("urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                var ext = XNamespace.Get("urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");

                // Fatura tipi belirle
                var invoiceTypeCode = fatura.FaturaTipi == 2 ? "SATIS" : "EARSIV";

                // XML belgesini oluştur
                var doc = new XDocument(
                    new XDeclaration("1.0", "UTF-8", null),
                    new XElement(ns + "Invoice",
                        new XAttribute(XNamespace.Xmlns + "cac", cac),
                        new XAttribute(XNamespace.Xmlns + "cbc", cbc),
                        new XAttribute(XNamespace.Xmlns + "ext", ext),

                        // UBL Versiyonu
                        new XElement(cbc + "UBLVersionID", "2.1"),
                        
                        // Fatura Tipi
                        new XElement(cbc + "InvoiceTypeCode", invoiceTypeCode),
                        
                        // Fatura No
                        new XElement(cbc + "ID", fatura.FaturaNo),
                        
                        // Fatura Tarihi
                        new XElement(cbc + "IssueDate", fatura.FaturaTarihi.ToString("yyyy-MM-dd")),
                        
                        // Para Birimi
                        new XElement(cbc + "DocumentCurrencyCode", "TRY"),
                        
                        // Satıcı Bilgileri
                        new XElement(cac + "AccountingSupplierParty",
                            new XElement(cac + "Party",
                                // Satıcı VKN/TCKN
                                new XElement(cac + "PartyIdentification",
                                    new XElement(cbc + "ID", 
                                        new XAttribute("schemeID", "VKN"), 
                                        "11111111111" // Firma VKN
                                    )
                                ),
                                
                                // Satıcı Adı
                                new XElement(cac + "PartyName",
                                    new XElement(cbc + "Name", "Benim Salonum")
                                ),
                                
                                // Satıcı Adresi
                                new XElement(cac + "PostalAddress",
                                    new XElement(cbc + "StreetName", "Adres Satırı 1"),
                                    new XElement(cbc + "CitySubdivisionName", "İlçe"),
                                    new XElement(cbc + "CityName", "İl"),
                                    new XElement(cbc + "Country",
                                        new XElement(cbc + "Name", "Türkiye")
                                    )
                                ),
                                
                                // Satıcı Vergi Dairesi
                                new XElement(cac + "PartyTaxScheme",
                                    new XElement(cac + "TaxScheme",
                                        new XElement(cbc + "Name", "Vergi Dairesi")
                                    )
                                )
                            )
                        ),
                        
                        // Alıcı Bilgileri
                        new XElement(cac + "AccountingCustomerParty",
                            new XElement(cac + "Party",
                                // Alıcı VKN/TCKN
                                new XElement(cac + "PartyIdentification",
                                    new XElement(cbc + "ID", 
                                        new XAttribute("schemeID", cari.VergiNo?.Length == 11 ? "TCKN" : "VKN"), 
                                        cari.VergiNo ?? "11111111111"
                                    )
                                ),
                                
                                // Alıcı Adı
                                new XElement(cac + "PartyName",
                                    new XElement(cbc + "Name", cari.FaturaUnvani)
                                ),
                                
                                // Alıcı Adresi
                                new XElement(cac + "PostalAddress",
                                    new XElement(cbc + "StreetName", cari.Adres ?? "Adres belirtilmedi"),
                                    new XElement(cbc + "CitySubdivisionName", cari.Ilce ?? ""),
                                    new XElement(cbc + "CityName", cari.Il ?? ""),
                                    new XElement(cbc + "Country",
                                        new XElement(cbc + "Name", "Türkiye")
                                    )
                                ),
                                
                                // Alıcı Vergi Dairesi
                                new XElement(cac + "PartyTaxScheme",
                                    new XElement(cac + "TaxScheme",
                                        new XElement(cbc + "Name", cari.VergiDairesi ?? "")
                                    )
                                )
                            )
                        )
                    )
                );

                // Fatura kalemleri ekle
                var invoiceElement = doc.Element(ns + "Invoice");
                
                // KDV hesapla
                decimal toplamKdvTutari = 0;
                decimal faturaTutari = 0;
                
                // İndirim varsa
                decimal toplamIndirim = 0;
                
                // Fatura kalemlerini XML'e ekle
                foreach (var detay in fatura.FaturaDetaylari)
                {
                    var itemKdvTutari = detay.KdvTutar;
                    toplamKdvTutari += itemKdvTutari;
                    
                    var itemTutari = detay.BirimFiyat * detay.Miktar;
                    faturaTutari += itemTutari;
                    
                    var indirimTutari = detay.IndirimOrani > 0 
                        ? (detay.IndirimTuru == 1 ? itemTutari * (detay.IndirimOrani / 100) : detay.IndirimOrani)
                        : 0;
                    toplamIndirim += indirimTutari;
                    
                    // Fatura kalemi
                    invoiceElement.Add(
                        new XElement(cac + "InvoiceLine",
                            new XElement(cbc + "ID", detay.StokKodu ?? detay.Id.ToString()),
                            
                            // Miktar
                            new XElement(cbc + "InvoicedQuantity", 
                                new XAttribute("unitCode", "C62"), // Adet
                                detay.Miktar.ToString().Replace(",", ".")
                            ),
                            
                            // Tutar
                            new XElement(cbc + "LineExtensionAmount", 
                                new XAttribute("currencyID", "TRY"),
                                (itemTutari - indirimTutari).ToString("0.00").Replace(",", ".")
                            ),
                            
                            // İndirim varsa
                            indirimTutari > 0 ? 
                                new XElement(cac + "AllowanceCharge",
                                    new XElement(cbc + "ChargeIndicator", "false"),
                                    new XElement(cbc + "Amount", 
                                        new XAttribute("currencyID", "TRY"),
                                        indirimTutari.ToString("0.00").Replace(",", ".")
                                    )
                                ) : null,
                            
                            // KDV
                            new XElement(cac + "TaxTotal",
                                new XElement(cbc + "TaxAmount", 
                                    new XAttribute("currencyID", "TRY"),
                                    itemKdvTutari.ToString("0.00").Replace(",", ".")
                                ),
                                new XElement(cac + "TaxSubtotal",
                                    new XElement(cbc + "TaxAmount", 
                                        new XAttribute("currencyID", "TRY"),
                                        itemKdvTutari.ToString("0.00").Replace(",", ".")
                                    ),
                                    new XElement(cac + "TaxCategory",
                                        new XElement(cbc + "TaxExemptionReasonCode", "351"),
                                        new XElement(cac + "TaxScheme",
                                            new XElement(cbc + "Name", "KDV"),
                                            new XElement(cbc + "TaxTypeCode", "0015")
                                        )
                                    )
                                )
                            ),
                            
                            // Ürün bilgileri
                            new XElement(cac + "Item",
                                new XElement(cbc + "Name", detay.Aciklama ?? detay.UrunAdi),
                                new XElement(cbc + "Description", detay.UrunAdi)
                            ),
                            
                            // Fiyat bilgileri
                            new XElement(cac + "Price",
                                new XElement(cbc + "PriceAmount", 
                                    new XAttribute("currencyID", "TRY"),
                                    detay.BirimFiyat.ToString("0.00").Replace(",", ".")
                                )
                            )
                        )
                    );
                }
                
                // Vergi Toplam
                invoiceElement.Add(
                    new XElement(cac + "TaxTotal",
                        new XElement(cbc + "TaxAmount", 
                            new XAttribute("currencyID", "TRY"),
                            toplamKdvTutari.ToString("0.00").Replace(",", ".")
                        ),
                        
                        // KDV özeti
                        new XElement(cac + "TaxSubtotal",
                            new XElement(cbc + "TaxableAmount", 
                                new XAttribute("currencyID", "TRY"),
                                (faturaTutari - toplamIndirim).ToString("0.00").Replace(",", ".")
                            ),
                            new XElement(cbc + "TaxAmount", 
                                new XAttribute("currencyID", "TRY"),
                                toplamKdvTutari.ToString("0.00").Replace(",", ".")
                            ),
                            new XElement(cbc + "CalculationSequenceNumeric", "1"),
                            new XElement(cbc + "Percent", "18.0"),
                            new XElement(cac + "TaxCategory",
                                new XElement(cac + "TaxScheme",
                                    new XElement(cbc + "Name", "KDV"),
                                    new XElement(cbc + "TaxTypeCode", "0015")
                                )
                            )
                        )
                    )
                );
                
                // Fatura Toplamları
                decimal toplam = faturaTutari - toplamIndirim + toplamKdvTutari;
                
                invoiceElement.Add(
                    new XElement(cac + "LegalMonetaryTotal",
                        new XElement(cbc + "LineExtensionAmount", 
                            new XAttribute("currencyID", "TRY"),
                            faturaTutari.ToString("0.00").Replace(",", ".")
                        ),
                        new XElement(cbc + "TaxExclusiveAmount", 
                            new XAttribute("currencyID", "TRY"),
                            (faturaTutari - toplamIndirim).ToString("0.00").Replace(",", ".")
                        ),
                        new XElement(cbc + "TaxInclusiveAmount", 
                            new XAttribute("currencyID", "TRY"),
                            toplam.ToString("0.00").Replace(",", ".")
                        ),
                        new XElement(cbc + "AllowanceTotalAmount", 
                            new XAttribute("currencyID", "TRY"),
                            toplamIndirim.ToString("0.00").Replace(",", ".")
                        ),
                        new XElement(cbc + "PayableAmount", 
                            new XAttribute("currencyID", "TRY"),
                            toplam.ToString("0.00").Replace(",", ".")
                        )
                    )
                );
                
                return doc.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"XML oluşturma hatası: {ex.Message}");
                throw;
            }
        }
    }
}
