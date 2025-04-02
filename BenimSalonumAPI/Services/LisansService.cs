using System;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using BenimSalonum.Entities.Tables;
using BenimSalonumAPI.Services.Interfaces;
using BenimSalonumAPI.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace BenimSalonumAPI.Services
{
    public class LisansService : ILisansService
    {
        private readonly BenimSalonumContext _context;
        private readonly ILogger<LisansService> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _lisansApiUrl;
        private readonly string _sifrelemeAnahtari;
        private readonly string _yerelDepoYolu;

        public LisansService(
            BenimSalonumContext context,
            ILogger<LisansService> logger,
            IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            
            // Lisans API ve şifreleme ayarları
            _lisansApiUrl = configuration["LisansAyarlari:ApiUrl"] ?? "https://api.benimsalonum.com/lisans";
            _sifrelemeAnahtari = configuration["LisansAyarlari:SifrelemeAnahtari"] ?? "BenimSalonumLisansSecureKey2025";
            _yerelDepoYolu = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BenimSalonum", "lisans.dat");
        }

        public async Task<bool> LisansKontrolEtAsync(string lisansKodu, int firmaId)
        {
            try
            {
                // Gerçek senaryoda, burada bir web API'sine istek yapılır
                // Bu örnekte simüle ediyoruz
                
                // Mevcut lisansı veritabanından kontrol et
                var mevcutLisans = await _context.Lisanslar
                    .FirstOrDefaultAsync(l => l.LisansKodu == lisansKodu && l.FirmaId == firmaId);
                
                // Gerçek uygulamada burada lisans sunucusuna istek gönderilecek
                // Şimdilik basit bir kontrol yapıyoruz
                if (mevcutLisans != null)
                {
                    // Lisansın geçerliliğini kontrol et
                    if (mevcutLisans.BitisTarihi >= DateTime.Now && mevcutLisans.Aktif)
                    {
                        // Lisans kontrolü başarılı, son kontrol tarihini güncelle
                        mevcutLisans.SonKontrolTarihi = DateTime.Now;
                        mevcutLisans.KalanKontrolGunu = 30; // 30 gün daha offline kullanım hakkı
                        
                        await _context.SaveChangesAsync();
                        
                        // Şifreli lisans bilgisini yerel depoya kaydet
                        await LisansBilgisiniKaydetAsync(mevcutLisans);
                        
                        _logger.LogInformation($"Lisans kontrolü başarılı: {lisansKodu}, Firma ID: {firmaId}");
                        return true;
                    }
                }
                
                _logger.LogWarning($"Lisans kontrolü başarısız: {lisansKodu}, Firma ID: {firmaId}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lisans kontrolü sırasında hata oluştu: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> OfflineLisansKontrolEtAsync(int firmaId)
        {
            try
            {
                // Yerel depodan şifreli lisans bilgisini oku
                var lisansBilgisi = await LisansBilgisiniOkuAsync(firmaId);
                
                if (lisansBilgisi != null)
                {
                    // Lisansın geçerliliğini kontrol et
                    if (lisansBilgisi.BitisTarihi >= DateTime.Now && lisansBilgisi.Aktif)
                    {
                        // Son kontrol tarihinden beri 30 günden fazla zaman geçti mi?
                        if (lisansBilgisi.SonKontrolTarihi.HasValue)
                        {
                            var gecenGunSayisi = (DateTime.Now - lisansBilgisi.SonKontrolTarihi.Value).TotalDays;
                            
                            if (gecenGunSayisi <= lisansBilgisi.KalanKontrolGunu)
                            {
                                _logger.LogInformation($"Offline lisans kontrolü başarılı: Firma ID: {firmaId}");
                                return true;
                            }
                        }
                    }
                }
                
                _logger.LogWarning($"Offline lisans kontrolü başarısız: Firma ID: {firmaId}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Offline lisans kontrolü sırasında hata oluştu: {ex.Message}");
                return false;
            }
        }

        public async Task<LisansTable> LisansAktifleştirAsync(string lisansKodu, string firmaAdi, int firmaId)
        {
            try
            {
                // Mevcut lisansı kontrol et
                var mevcutLisans = await _context.Lisanslar
                    .FirstOrDefaultAsync(l => l.FirmaId == firmaId);
                
                if (mevcutLisans != null)
                {
                    // Mevcut lisans varsa güncelle
                    mevcutLisans.LisansKodu = lisansKodu;
                    mevcutLisans.BaslangicTarihi = DateTime.Now;
                    mevcutLisans.BitisTarihi = DateTime.Now.AddYears(1); // 1 yıllık lisans
                    mevcutLisans.Aktif = true;
                    mevcutLisans.SonKontrolTarihi = DateTime.Now;
                    mevcutLisans.KalanKontrolGunu = 30;
                    mevcutLisans.AktivasyonAnahtari = GenerateActivationKey();
                    
                    await _context.SaveChangesAsync();
                    
                    // Şifreli lisans bilgisini yerel depoya kaydet
                    await LisansBilgisiniKaydetAsync(mevcutLisans);
                    
                    _logger.LogInformation($"Lisans güncellendi: {lisansKodu}, Firma ID: {firmaId}");
                    return mevcutLisans;
                }
                else
                {
                    // Yeni lisans oluştur
                    var yeniLisans = new LisansTable
                    {
                        LisansKodu = lisansKodu,
                        FirmaId = firmaId,
                        BaslangicTarihi = DateTime.Now,
                        BitisTarihi = DateTime.Now.AddYears(1), // 1 yıllık lisans
                        KullaniciSayisiLimiti = 5, // Varsayılan 5 kullanıcı
                        Aktif = true,
                        SonKontrolTarihi = DateTime.Now,
                        KalanKontrolGunu = 30,
                        LisansTuru = "Standart",
                        AktivasyonAnahtari = GenerateActivationKey(),
                        Notlar = $"{DateTime.Now} tarihinde aktivasyon yapıldı."
                    };
                    
                    await _context.Lisanslar.AddAsync(yeniLisans);
                    await _context.SaveChangesAsync();
                    
                    // Şifreli lisans bilgisini yerel depoya kaydet
                    await LisansBilgisiniKaydetAsync(yeniLisans);
                    
                    _logger.LogInformation($"Yeni lisans oluşturuldu: {lisansKodu}, Firma ID: {firmaId}");
                    return yeniLisans;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lisans aktivasyonu sırasında hata oluştu: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> GunlukLisansKontrolEtAsync(int firmaId)
        {
            try
            {
                // İnternet bağlantısı varsa online kontrol yap
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    // Lisans bilgisini veritabanından al
                    var lisans = await _context.Lisanslar
                        .FirstOrDefaultAsync(l => l.FirmaId == firmaId);
                    
                    if (lisans != null)
                    {
                        // Online kontrol yap
                        var sonuc = await LisansKontrolEtAsync(lisans.LisansKodu, firmaId);
                        if (sonuc)
                        {
                            return true;
                        }
                    }
                }
                
                // Online kontrol başarısız olduysa veya internet bağlantısı yoksa offline kontrol yap
                return await OfflineLisansKontrolEtAsync(firmaId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Günlük lisans kontrolü sırasında hata oluştu: {ex.Message}");
                // Hata durumunda offline kontrolü dene
                return await OfflineLisansKontrolEtAsync(firmaId);
            }
        }

        public async Task<bool> LisansGecerliMiAsync(int firmaId)
        {
            try
            {
                // Önce veritabanından kontrol et
                var lisans = await _context.Lisanslar
                    .FirstOrDefaultAsync(l => l.FirmaId == firmaId);
                
                if (lisans != null)
                {
                    // Lisans süresi doldu mu?
                    if (lisans.BitisTarihi < DateTime.Now)
                    {
                        return false;
                    }
                    
                    // Lisans aktif mi?
                    if (!lisans.Aktif)
                    {
                        return false;
                    }
                    
                    return true;
                }
                
                // Veritabanında lisans yoksa offline kontrol yap
                return await OfflineLisansKontrolEtAsync(firmaId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lisans geçerlilik kontrolü sırasında hata oluştu: {ex.Message}");
                // Hata durumunda offline kontrolü dene
                return await OfflineLisansKontrolEtAsync(firmaId);
            }
        }

        public async Task<LisansTable> LisansBilgisiGetirAsync(int firmaId)
        {
            try
            {
                // Veritabanından lisans bilgisini al
                var lisans = await _context.Lisanslar
                    .FirstOrDefaultAsync(l => l.FirmaId == firmaId);
                
                if (lisans == null)
                {
                    // Veritabanında yoksa şifreli depodan okumayı dene
                    lisans = await LisansBilgisiniOkuAsync(firmaId);
                }
                
                return lisans;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lisans bilgisi getirme sırasında hata oluştu: {ex.Message}");
                throw;
            }
        }

        #region Helper Methods
        
        private async Task LisansBilgisiniKaydetAsync(LisansTable lisans)
        {
            try
            {
                // Lisans verilerini serialize et
                var jsonData = JsonSerializer.Serialize(lisans);
                
                // Veriyi şifrele
                var encryptedData = EncryptString(jsonData);
                
                // Kaydetme klasörünü oluştur (yoksa)
                var directory = Path.GetDirectoryName(_yerelDepoYolu);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                // Şifreli veriyi dosyaya yaz
                await File.WriteAllTextAsync($"{_yerelDepoYolu}_{lisans.FirmaId}", encryptedData);
                
                _logger.LogInformation($"Lisans bilgisi yerel depoya kaydedildi: Firma ID: {lisans.FirmaId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lisans bilgisi kaydedilirken hata oluştu: {ex.Message}");
            }
        }
        
        private async Task<LisansTable> LisansBilgisiniOkuAsync(int firmaId)
        {
            try
            {
                var filePath = $"{_yerelDepoYolu}_{firmaId}";
                
                // Dosya yoksa null dön
                if (!File.Exists(filePath))
                {
                    return null;
                }
                
                // Şifreli veriyi oku
                var encryptedData = await File.ReadAllTextAsync(filePath);
                
                // Şifreyi çöz
                var jsonData = DecryptString(encryptedData);
                
                // JSON'dan nesneye dönüştür
                var lisans = JsonSerializer.Deserialize<LisansTable>(jsonData);
                
                return lisans;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lisans bilgisi okunurken hata oluştu: {ex.Message}");
                return null;
            }
        }
        
        private string GenerateActivationKey()
        {
            // 16 karakterlik rastgele bir aktivasyon anahtarı oluştur
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            
            return new string(Enumerable.Repeat(chars, 16)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        private string EncryptString(string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;
            
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_sifrelemeAnahtari.PadRight(32).Substring(0, 32));
                aes.IV = iv;
                
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        
                        array = memoryStream.ToArray();
                    }
                }
            }
            
            return Convert.ToBase64String(array);
        }
        
        private string DecryptString(string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);
            
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_sifrelemeAnahtari.PadRight(32).Substring(0, 32));
                aes.IV = iv;
                
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        
        #endregion
    }
}
