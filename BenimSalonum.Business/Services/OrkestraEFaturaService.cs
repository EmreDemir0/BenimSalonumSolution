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
using BenimSalonum.DAL.Context;
using Microsoft.Extensions.Logging;

namespace BenimSalonum.Business.Services
{
    public class OrkestraEFaturaService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly ILogger<OrkestraEFaturaService> _logger;
        private readonly string _baseUrl;
        private readonly string _username;
        private readonly string _password;

        public OrkestraEFaturaService(
            HttpClient httpClient,
            IConfiguration configuration,
            AppDbContext context,
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
                var fatura = await _context.FaturaTables
                    .Include("FaturaDetayTables")
                    .FirstOrDefaultAsync(f => f.Id == faturaId);

                if (fatura == null)
                {
                    return (false, "Fatura bulunamadı.", string.Empty);
                }

                // Cari bilgilerini al
                var cari = await _context.CariTables
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
                    
                    _context.EFaturaLogTables.Add(log);
                    await _context.SaveChangesAsync();
                    
                    return (true, "E-Fatura başarıyla gönderildi.", log.UUID ?? string.Empty);
                }
                else
                {
                    // Hata yanıtı
                    log.IslemDurumu = 3; // Hata
                    
                    if (responseObject != null && responseObject.ContainsKey("message"))
                    {
                        log.HataMesaji = responseObject["message"].ToString();
                    }
                    else
                    {
                        log.HataMesaji = "Bilinmeyen hata";
                    }

                    // Faturayı güncelle
                    fatura.EfaturaGonderimTarihi = DateTime.Now;
                    fatura.EfaturaBasarili = false;
                    fatura.EfaturaHata = log.HataMesaji;
                    fatura.GuncellenmeTarihi = DateTime.Now;
                    fatura.GuncelleyenKullaniciId = 1; // Sistem kullanıcısı

                    await _context.SaveChangesAsync();
                    
                    _context.EFaturaLogTables.Add(log);
                    await _context.SaveChangesAsync();
                    
                    return (false, $"E-Fatura gönderim hatası: {log.HataMesaji}", string.Empty);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"E-Fatura gönderim hatası: {ex.Message}");
                
                // Log kaydet
                var log = new EFaturaLogTable
                {
                    FaturaId = faturaId,
                    IslemTuru = 1, // Varsayılan olarak e-Fatura
                    IslemDurumu = 3, // Hata
                    HataMesaji = ex.Message,
                    KullaniciId = 1 // Sistem kullanıcısı
                };
                
                _context.EFaturaLogTables.Add(log);
                await _context.SaveChangesAsync();
                
                return (false, $"E-Fatura gönderim hatası: {ex.Message}", string.Empty);
            }
        }

        /// <summary>
        /// E-Fatura/E-Arşiv faturası durumunu sorgular
        /// </summary>
        /// <param name="uuid">UUID</param>
        /// <returns>Fatura durumu</returns>
        public async Task<(bool Success, string Status, string Message)> GetEFaturaStatusAsync(string uuid)
        {
            try
            {
                // Token al
                var token = await GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    return (false, string.Empty, "Orkestra API'sine giriş yapılamadı.");
                }

                // İstek gövdesini hazırla
                var requestData = new
                {
                    cmd = "status",
                    token = token,
                    uuid = uuid
                };

                var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync(_baseUrl, content);
                response.EnsureSuccessStatusCode();
                
                var responseString = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<Dictionary<string, object>>(responseString);
                
                if (responseObject != null && responseObject.ContainsKey("status"))
                {
                    var status = responseObject["status"].ToString();
                    var message = responseObject.ContainsKey("message") ? responseObject["message"].ToString() : string.Empty;
                    
                    return (true, status ?? string.Empty, message ?? string.Empty);
                }
                
                return (false, string.Empty, "Durum bilgisi alınamadı.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"E-Fatura durum sorgulama hatası: {ex.Message}");
                return (false, string.Empty, $"E-Fatura durum sorgulama hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// E-Fatura/E-Arşiv faturası PDF'ini mail olarak gönderir
        /// </summary>
        /// <param name="uuid">UUID</param>
        /// <param name="email">Mail adresi</param>
        /// <returns>İşlem sonucu</returns>
        public async Task<(bool Success, string Message)> SendEFaturaEmailAsync(string uuid, string email)
        {
            try
            {
                // Token al
                var token = await GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    return (false, "Orkestra API'sine giriş yapılamadı.");
                }

                // İstek gövdesini hazırla
                var requestData = new
                {
                    cmd = "sendMail",
                    token = token,
                    uuid = uuid,
                    email = email
                };

                var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync(_baseUrl, content);
                response.EnsureSuccessStatusCode();
                
                var responseString = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<Dictionary<string, object>>(responseString);
                
                if (responseObject != null && responseObject.ContainsKey("status") && responseObject["status"].ToString() == "success")
                {
                    // Log güncelle
                    var log = await _context.EFaturaLogTables
                        .FirstOrDefaultAsync(l => l.UUID == uuid);
                        
                    if (log != null)
                    {
                        log.MailGonderildi = true;
                        log.MailGonderimTarihi = DateTime.Now;
                        log.MailAdresi = email;
                        
                        await _context.SaveChangesAsync();
                    }
                    
                    return (true, "E-Fatura maili başarıyla gönderildi.");
                }
                else
                {
                    var message = responseObject != null && responseObject.ContainsKey("message") ? 
                        responseObject["message"].ToString() : "Bilinmeyen hata";
                    
                    return (false, $"E-Fatura mail gönderim hatası: {message}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"E-Fatura mail gönderim hatası: {ex.Message}");
                return (false, $"E-Fatura mail gönderim hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// UBL formatında e-Fatura XML'i oluşturur
        /// </summary>
        /// <param name="fatura">Fatura bilgileri</param>
        /// <param name="cari">Cari bilgileri</param>
        /// <returns>UBL formatında XML</returns>
        private string CreateEFaturaXml(FaturaTable fatura, CariTable cari)
        {
            try
            {
                // Burada Orkestra'nın beklediği UBL formatında XML oluşturulacak
                // Gerçek implementasyon Orkestra'nın dokümanlarına göre yapılmalı
                
                // Örnek basit UBL yapısı
                var ubl = new XElement(XName.Get("Invoice", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"),
                    new XElement("ID", fatura.FaturaNo),
                    new XElement("IssueDate", fatura.FaturaTarihi.ToString("yyyy-MM-dd")),
                    new XElement("InvoiceTypeCode", fatura.FaturaTuru == 1 ? "SATIS" : "ALIS"),
                    new XElement("DocumentCurrencyCode", "TRY"),
                    new XElement("AccountingSupplierParty",
                        new XElement("Party",
                            new XElement("PartyName",
                                new XElement("Name", "Şirket Adı")
                            ),
                            new XElement("PostalAddress",
                                new XElement("StreetName", "Şirket Adresi"),
                                new XElement("CityName", "İstanbul"),
                                new XElement("CountrySubentity", "Türkiye")
                            ),
                            new XElement("PartyTaxScheme",
                                new XElement("TaxScheme",
                                    new XElement("Name", "Vergi Dairesi"),
                                    new XElement("TaxTypeCode", "0123")
                                )
                            ),
                            new XElement("Contact",
                                new XElement("Telephone", "0212 123 45 67"),
                                new XElement("ElectronicMail", "info@sirket.com")
                            )
                        )
                    ),
                    new XElement("AccountingCustomerParty",
                        new XElement("Party",
                            new XElement("PartyName",
                                new XElement("Name", cari.CariUnvan)
                            ),
                            new XElement("PostalAddress",
                                new XElement("StreetName", cari.Adres),
                                new XElement("CityName", cari.Il),
                                new XElement("CountrySubentity", "Türkiye")
                            ),
                            new XElement("PartyTaxScheme",
                                new XElement("TaxScheme",
                                    new XElement("Name", cari.VergiDairesi),
                                    new XElement("TaxTypeCode", cari.VergiNo)
                                )
                            ),
                            new XElement("Contact",
                                new XElement("Telephone", cari.Telefon),
                                new XElement("ElectronicMail", cari.Email)
                            )
                        )
                    )
                );
                
                // Fatura kalemlerini ekle
                var invoiceLines = new XElement("InvoiceLines");
                
                foreach (var kalem in fatura.FaturaDetayTables)
                {
                    invoiceLines.Add(
                        new XElement("InvoiceLine",
                            new XElement("ID", kalem.SiraNo),
                            new XElement("InvoicedQuantity", kalem.Miktar),
                            new XElement("LineExtensionAmount", kalem.AraToplam),
                            new XElement("TaxTotal",
                                new XElement("TaxAmount", kalem.KdvTutar),
                                new XElement("TaxSubtotal",
                                    new XElement("TaxableAmount", kalem.AraToplam),
                                    new XElement("TaxAmount", kalem.KdvTutar),
                                    new XElement("TaxCategory",
                                        new XElement("TaxScheme",
                                            new XElement("Name", "KDV"),
                                            new XElement("TaxTypeCode", "0015")
                                        )
                                    ),
                                    new XElement("Percent", kalem.KdvOrani)
                                )
                            ),
                            new XElement("Item",
                                new XElement("Name", kalem.UrunAdi),
                                new XElement("SellersItemIdentification",
                                    new XElement("ID", kalem.StokKodu)
                                )
                            ),
                            new XElement("Price",
                                new XElement("PriceAmount", kalem.BirimFiyat)
                            )
                        )
                    );
                }
                
                ubl.Add(invoiceLines);
                
                // Toplam tutarları ekle
                ubl.Add(
                    new XElement("TaxTotal",
                        new XElement("TaxAmount", fatura.KdvToplam)
                    ),
                    new XElement("LegalMonetaryTotal",
                        new XElement("LineExtensionAmount", fatura.AraToplam),
                        new XElement("TaxExclusiveAmount", fatura.AraToplam),
                        new XElement("TaxInclusiveAmount", fatura.GenelToplam),
                        new XElement("AllowanceTotalAmount", fatura.IndirimTutar),
                        new XElement("PayableAmount", fatura.GenelToplam)
                    )
                );
                
                // XML'i string'e dönüştür
                return ubl.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"XML oluşturma hatası: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Orkestra API'sinden Mükellefleri sorgular
        /// </summary>
        /// <param name="vknTckn">VKN/TCKN</param>
        /// <returns>Mükellef bilgileri</returns>
        public async Task<(bool Success, object Response, string Message)> GetTaxpayerInfoAsync(string vknTckn)
        {
            try
            {
                // Token al
                var token = await GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    return (false, null, "Orkestra API'sine giriş yapılamadı.");
                }

                // İstek gövdesini hazırla
                var requestData = new
                {
                    cmd = "mükellef",
                    token = token,
                    vknTckn = vknTckn
                };

                var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync(_baseUrl, content);
                response.EnsureSuccessStatusCode();
                
                var responseString = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<Dictionary<string, object>>(responseString);
                
                if (responseObject != null && responseObject.ContainsKey("status") && responseObject["status"].ToString() == "success")
                {
                    return (true, responseObject, "Mükellef bilgileri başarıyla alındı.");
                }
                else
                {
                    var message = responseObject != null && responseObject.ContainsKey("message") ? 
                        responseObject["message"].ToString() : "Bilinmeyen hata";
                    
                    return (false, null, $"Mükellef bilgileri alınamadı: {message}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Mükellef sorgulama hatası: {ex.Message}");
                return (false, null, $"Mükellef sorgulama hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// E-Fatura ön izleme yapar
        /// </summary>
        /// <param name="faturaId">Fatura ID</param>
        /// <returns>Ön izleme HTML'i</returns>
        public async Task<(bool Success, string HtmlContent, string Message)> PreviewEFaturaAsync(int faturaId)
        {
            try
            {
                // Fatura bilgilerini al
                var fatura = await _context.FaturaTables
                    .Include("FaturaDetayTables")
                    .FirstOrDefaultAsync(f => f.Id == faturaId);

                if (fatura == null)
                {
                    return (false, string.Empty, "Fatura bulunamadı.");
                }

                // Cari bilgilerini al
                var cari = await _context.CariTables
                    .FirstOrDefaultAsync(c => c.Id == fatura.CariId);

                if (cari == null)
                {
                    return (false, string.Empty, "Cari bilgileri bulunamadı.");
                }

                // XML oluştur
                var faturaXml = CreateEFaturaXml(fatura, cari);

                // Token al
                var token = await GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    return (false, string.Empty, "Orkestra API'sine giriş yapılamadı.");
                }

                // İstek gövdesini hazırla
                var requestData = new
                {
                    cmd = "preview",
                    token = token,
                    xml = faturaXml
                };

                var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync(_baseUrl, content);
                response.EnsureSuccessStatusCode();
                
                var responseString = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<Dictionary<string, object>>(responseString);
                
                if (responseObject != null && responseObject.ContainsKey("status") && responseObject["status"].ToString() == "success")
                {
                    var htmlContent = responseObject.ContainsKey("html") ? 
                        responseObject["html"].ToString() : string.Empty;
                    
                    return (true, htmlContent ?? string.Empty, "E-Fatura ön izleme başarılı.");
                }
                else
                {
                    var message = responseObject != null && responseObject.ContainsKey("message") ? 
                        responseObject["message"].ToString() : "Bilinmeyen hata";
                    
                    return (false, string.Empty, $"E-Fatura ön izleme hatası: {message}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"E-Fatura ön izleme hatası: {ex.Message}");
                return (false, string.Empty, $"E-Fatura ön izleme hatası: {ex.Message}");
            }
        }
    }
}
