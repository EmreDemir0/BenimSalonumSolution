using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BenimSalonum.Entities.Tables;
using BenimSalonumAPI.DataAccess.Context;
using BenimSalonumAPI.Services;
using BenimSalonumAPI.Services.Interfaces;
using System.Security.Claims;
using System.Linq;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Sadece yetkili kullanıcılar erişebilir
    public class KullaniciYonetimController : ControllerBase
    {
        private readonly BenimSalonumContext _context;
        private readonly ILogService _logService;
        private readonly ILisansService _lisansService;

        public KullaniciYonetimController(
            BenimSalonumContext context,
            ILogService logService,
            ILisansService lisansService)
        {
            _context = context;
            _logService = logService;
            _lisansService = lisansService;
        }

        // Firma kullanıcılarını getir
        [HttpGet]
        public async Task<IActionResult> GetKullanicilar()
        {
            try
            {
                // Kullanıcının bilgilerini al
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Kullanıcı bilgisi bulunamadı.");
                }

                var user = await _context.Kullanicilar.FindAsync(int.Parse(userId));
                if (user == null || !user.FirmaId.HasValue)
                {
                    return NotFound("Kullanıcıya bağlı firma bilgisi bulunamadı.");
                }

                // Firma kullanıcılarını getir
                var kullanicilar = await _context.Kullanicilar
                    .Where(k => k.FirmaId == user.FirmaId)
                    .Select(k => new
                    {
                        k.Id,
                        k.KullaniciAdi,
                        k.Adi,
                        k.Soyadi,
                        k.Gorevi,
                        k.Aktif,
                        k.AnaKullanici,
                        k.KayitTarihi,
                        k.SonGirisTarihi,
                        YoneticiAdi = k.Yonetici != null ? k.Yonetici.KullaniciAdi : null
                    })
                    .ToListAsync();

                await _logService.LogInformationAsync(
                    $"Firma kullanıcıları listelendi. Toplam: {kullanicilar.Count}", 
                    "KullaniciYonetimController");

                return Ok(kullanicilar);
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync("Kullanıcılar listelenirken hata oluştu", ex, "KullaniciYonetimController");
                return StatusCode(500, "Kullanıcılar listelenirken bir hata oluştu.");
            }
        }

        // Kullanıcı detayı getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKullanici(int id)
        {
            try
            {
                // Kullanıcının bilgilerini al
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Kullanıcı bilgisi bulunamadı.");
                }

                var user = await _context.Kullanicilar.FindAsync(int.Parse(userId));
                if (user == null || !user.FirmaId.HasValue)
                {
                    return NotFound("Kullanıcıya bağlı firma bilgisi bulunamadı.");
                }

                // İstenen kullanıcıyı getir
                var kullanici = await _context.Kullanicilar.FindAsync(id);
                if (kullanici == null || kullanici.FirmaId != user.FirmaId)
                {
                    return NotFound("Kullanıcı bulunamadı veya bu firmaya ait değil.");
                }

                // Kullanıcı bilgilerini döndür (parola hariç)
                var kullaniciDetay = new
                {
                    kullanici.Id,
                    kullanici.KullaniciAdi,
                    kullanici.Adi,
                    kullanici.Soyadi,
                    kullanici.Gorevi,
                    kullanici.Aktif,
                    kullanici.Durumu,
                    kullanici.AnaKullanici,
                    kullanici.YoneticiId,
                    kullanici.HatirlatmaSorusu,
                    kullanici.KayitTarihi,
                    kullanici.SonGirisTarihi
                };

                await _logService.LogInformationAsync(
                    $"Kullanıcı detayı görüntülendi: {kullanici.KullaniciAdi}", 
                    "KullaniciYonetimController");

                return Ok(kullaniciDetay);
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync("Kullanıcı detayı getirilirken hata oluştu", ex, "KullaniciYonetimController");
                return StatusCode(500, "Kullanıcı detayı getirilirken bir hata oluştu.");
            }
        }

        // Yeni alt kullanıcı oluştur
        [HttpPost]
        public async Task<IActionResult> CreateAltKullanici([FromBody] KullaniciTable kullanici)
        {
            try
            {
                // Kullanıcının bilgilerini al
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Kullanıcı bilgisi bulunamadı.");
                }

                var user = await _context.Kullanicilar.FindAsync(int.Parse(userId));
                if (user == null || !user.FirmaId.HasValue)
                {
                    return NotFound("Kullanıcıya bağlı firma bilgisi bulunamadı.");
                }

                // Sadece ana kullanıcılar alt kullanıcı oluşturabilir
                if (!user.AnaKullanici)
                {
                    return Forbid("Alt kullanıcı oluşturma yetkiniz yok.");
                }

                // Lisans kontrolü yap
                var lisansGecerli = await _lisansService.LisansGecerliMiAsync(user.FirmaId.Value);
                if (!lisansGecerli)
                {
                    return BadRequest("Geçerli bir lisansınız yok. Lütfen lisansınızı yenileyin.");
                }

                // Kullanıcı sayısı limitini kontrol et
                var lisans = await _lisansService.LisansBilgisiGetirAsync(user.FirmaId.Value);
                var mevcutKullaniciSayisi = await _context.Kullanicilar
                    .CountAsync(k => k.FirmaId == user.FirmaId);

                if (mevcutKullaniciSayisi >= lisans.KullaniciSayisiLimiti)
                {
                    return BadRequest($"Kullanıcı sayısı limitine ulaşıldı ({lisans.KullaniciSayisiLimiti}). Daha fazla kullanıcı eklemek için lisansınızı yükseltin.");
                }

                // Aynı kullanıcı adı ile kayıt var mı kontrol et
                var existingUser = await _context.Kullanicilar
                    .FirstOrDefaultAsync(k => k.KullaniciAdi == kullanici.KullaniciAdi);

                if (existingUser != null)
                {
                    return BadRequest("Bu kullanıcı adı zaten kullanılıyor.");
                }

                // Yeni kullanıcı oluştur (alt kullanıcı olarak)
                var yeniKullanici = new KullaniciTable
                {
                    KullaniciAdi = kullanici.KullaniciAdi,
                    Adi = kullanici.Adi,
                    Soyadi = kullanici.Soyadi,
                    Parola = kullanici.Parola,
                    Gorevi = string.IsNullOrEmpty(kullanici.Gorevi) ? "User" : kullanici.Gorevi, // Default User rolü
                    FirmaId = user.FirmaId,
                    AnaKullanici = false,
                    YoneticiId = user.Id,
                    Aktif = true,
                    Durumu = true,
                    KayitTarihi = DateTime.Now
                };

                await _context.Kullanicilar.AddAsync(yeniKullanici);
                await _context.SaveChangesAsync();

                await _logService.LogInformationAsync(
                    $"Yeni alt kullanıcı oluşturuldu: {yeniKullanici.KullaniciAdi}", 
                    "KullaniciYonetimController");

                // Kullanıcı bilgilerini döndür (parola hariç)
                var kullaniciDetay = new
                {
                    yeniKullanici.Id,
                    yeniKullanici.KullaniciAdi,
                    yeniKullanici.Adi,
                    yeniKullanici.Soyadi,
                    yeniKullanici.Gorevi,
                    yeniKullanici.Aktif,
                    yeniKullanici.AnaKullanici,
                    yeniKullanici.YoneticiId,
                    yeniKullanici.KayitTarihi
                };

                return Ok(kullaniciDetay);
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync("Alt kullanıcı oluşturulurken hata oluştu", ex, "KullaniciYonetimController");
                return StatusCode(500, "Alt kullanıcı oluşturulurken bir hata oluştu.");
            }
        }

        // Kullanıcı güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKullanici(int id, [FromBody] KullaniciTable kullanici)
        {
            try
            {
                // Kullanıcının bilgilerini al
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Kullanıcı bilgisi bulunamadı.");
                }

                var user = await _context.Kullanicilar.FindAsync(int.Parse(userId));
                if (user == null || !user.FirmaId.HasValue)
                {
                    return NotFound("Kullanıcıya bağlı firma bilgisi bulunamadı.");
                }

                // Güncellenecek kullanıcıyı kontrol et
                var mevcutKullanici = await _context.Kullanicilar.FindAsync(id);
                if (mevcutKullanici == null || mevcutKullanici.FirmaId != user.FirmaId)
                {
                    return NotFound("Kullanıcı bulunamadı veya bu firmaya ait değil.");
                }

                // Sadece ana kullanıcılar veya kullanıcının kendisi güncelleyebilir
                if (!user.AnaKullanici && user.Id != id)
                {
                    return Forbid("Bu kullanıcıyı güncelleme yetkiniz yok.");
                }

                // Ana kullanıcı değiştirilmesini engelle
                if (mevcutKullanici.AnaKullanici && !kullanici.AnaKullanici && user.Id != id)
                {
                    return BadRequest("Ana kullanıcı statüsü değiştirilemez.");
                }

                // Kullanıcı bilgilerini güncelle
                mevcutKullanici.Adi = kullanici.Adi;
                mevcutKullanici.Soyadi = kullanici.Soyadi;
                
                // Parola sadece boş değilse güncellenir
                if (!string.IsNullOrEmpty(kullanici.Parola))
                {
                    mevcutKullanici.Parola = kullanici.Parola;
                }
                
                // Ana kullanıcı ise rol güncellenebilir
                if (user.AnaKullanici)
                {
                    mevcutKullanici.Gorevi = string.IsNullOrEmpty(kullanici.Gorevi) ? "User" : kullanici.Gorevi;
                    mevcutKullanici.Aktif = kullanici.Aktif;
                    mevcutKullanici.Durumu = kullanici.Durumu;
                }

                mevcutKullanici.HatirlatmaSorusu = kullanici.HatirlatmaSorusu;
                mevcutKullanici.HatirlatmaCevap = kullanici.HatirlatmaCevap;

                await _context.SaveChangesAsync();

                await _logService.LogInformationAsync(
                    $"Kullanıcı güncellendi: {mevcutKullanici.KullaniciAdi}", 
                    "KullaniciYonetimController");

                // Kullanıcı bilgilerini döndür (parola hariç)
                var kullaniciDetay = new
                {
                    mevcutKullanici.Id,
                    mevcutKullanici.KullaniciAdi,
                    mevcutKullanici.Adi,
                    mevcutKullanici.Soyadi,
                    mevcutKullanici.Gorevi,
                    mevcutKullanici.Aktif,
                    mevcutKullanici.Durumu,
                    mevcutKullanici.AnaKullanici,
                    mevcutKullanici.YoneticiId,
                    mevcutKullanici.HatirlatmaSorusu,
                    mevcutKullanici.KayitTarihi,
                    mevcutKullanici.SonGirisTarihi
                };

                return Ok(kullaniciDetay);
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync("Kullanıcı güncellenirken hata oluştu", ex, "KullaniciYonetimController");
                return StatusCode(500, "Kullanıcı güncellenirken bir hata oluştu.");
            }
        }

        // Kullanıcı aktif/pasif durumunu değiştir
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ToggleUserStatus(int id)
        {
            try
            {
                // Kullanıcının bilgilerini al
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Kullanıcı bilgisi bulunamadı.");
                }

                var user = await _context.Kullanicilar.FindAsync(int.Parse(userId));
                if (user == null || !user.FirmaId.HasValue)
                {
                    return NotFound("Kullanıcıya bağlı firma bilgisi bulunamadı.");
                }

                // Sadece ana kullanıcılar kullanıcı durumunu değiştirebilir
                if (!user.AnaKullanici)
                {
                    return Forbid("Kullanıcı durumunu değiştirme yetkiniz yok.");
                }

                // Hedef kullanıcıyı kontrol et
                var targetUser = await _context.Kullanicilar.FindAsync(id);
                if (targetUser == null || targetUser.FirmaId != user.FirmaId)
                {
                    return NotFound("Kullanıcı bulunamadı veya bu firmaya ait değil.");
                }

                // Ana kullanıcının durumu değiştirilemez
                if (targetUser.AnaKullanici)
                {
                    return BadRequest("Ana kullanıcının durumu değiştirilemez.");
                }

                // Kullanıcı durumunu değiştir
                targetUser.Aktif = !targetUser.Aktif;
                targetUser.Durumu = targetUser.Aktif;

                await _context.SaveChangesAsync();

                await _logService.LogInformationAsync(
                    $"Kullanıcı durumu değiştirildi: {targetUser.KullaniciAdi}, Durum: {(targetUser.Aktif ? "Aktif" : "Pasif")}", 
                    "KullaniciYonetimController");

                return Ok(new
                {
                    targetUser.Id,
                    targetUser.KullaniciAdi,
                    targetUser.Aktif,
                    targetUser.Durumu
                });
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync("Kullanıcı durumu değiştirilirken hata oluştu", ex, "KullaniciYonetimController");
                return StatusCode(500, "Kullanıcı durumu değiştirilirken bir hata oluştu.");
            }
        }

        // Yeni ana kullanıcı ve firma oluştur (ilk kurulum)
        [HttpPost("setup")]
        [AllowAnonymous] // Herkes erişebilir (ilk kurulum için)
        public async Task<IActionResult> InitialSetup([FromBody] InitialSetupModel model)
        {
            try
            {
                // İlk ana kullanıcı var mı kontrol et
                var anyUser = await _context.Kullanicilar.AnyAsync();
                if (anyUser)
                {
                    return BadRequest("Sistem zaten kurulmuş. Bu işlemi yapamazsınız.");
                }

                // Yeni firma oluştur
                var firma = new FirmaTable
                {
                    FirmaAdi = model.FirmaAdi,
                    VergiNo = model.VergiNo,
                    VergiDairesi = model.VergiDairesi,
                    Adres = model.Adres,
                    Il = model.Il,
                    Ilce = model.Ilce,
                    Telefon = model.Telefon,
                    Email = model.Email,
                    WebSitesi = model.WebSitesi,
                    OlusturulmaTarihi = DateTime.Now
                };

                await _context.Firmalar.AddAsync(firma);
                await _context.SaveChangesAsync();

                // Ana kullanıcı oluştur
                var anaKullanici = new KullaniciTable
                {
                    KullaniciAdi = model.KullaniciAdi,
                    Adi = model.Adi,
                    Soyadi = model.Soyadi,
                    Parola = model.Parola,
                    Gorevi = "Admin", // Ana kullanıcı her zaman Admin rolündedir
                    FirmaId = firma.Id,
                    AnaKullanici = true,
                    Aktif = true,
                    Durumu = true,
                    KayitTarihi = DateTime.Now
                };

                await _context.Kullanicilar.AddAsync(anaKullanici);
                await _context.SaveChangesAsync();

                // Demo lisans oluştur (30 günlük)
                var lisans = new LisansTable
                {
                    LisansKodu = "DEMO-" + Guid.NewGuid().ToString().Substring(0, 8),
                    FirmaId = firma.Id,
                    BaslangicTarihi = DateTime.Now,
                    BitisTarihi = DateTime.Now.AddDays(30),
                    KullaniciSayisiLimiti = 3,
                    Aktif = true,
                    SonKontrolTarihi = DateTime.Now,
                    KalanKontrolGunu = 30,
                    LisansTuru = "Demo",
                    AktivasyonAnahtari = Guid.NewGuid().ToString(),
                    Notlar = "İlk kurulum demo lisansı"
                };

                await _context.Lisanslar.AddAsync(lisans);
                await _context.SaveChangesAsync();

                // Kullanıcı oluşturulduğunu logla
                await _logService.LogInformationAsync(
                    $"İlk kurulum tamamlandı. Firma: {firma.FirmaAdi}, Ana Kullanıcı: {anaKullanici.KullaniciAdi}",
                    "KullaniciYonetimController",
                    LogVisibility.AdminOnly);

                return Ok(new
                {
                    message = "Sistem başarıyla kuruldu.",
                    firma = new
                    {
                        firma.Id,
                        firma.FirmaAdi
                    },
                    kullanici = new
                    {
                        anaKullanici.Id,
                        anaKullanici.KullaniciAdi
                    },
                    lisans = new
                    {
                        lisans.LisansKodu,
                        lisans.BaslangicTarihi,
                        lisans.BitisTarihi,
                        lisans.LisansTuru
                    }
                });
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync("İlk kurulum sırasında hata oluştu", ex, "KullaniciYonetimController");
                return StatusCode(500, "İlk kurulum sırasında bir hata oluştu.");
            }
        }
    }

    // İlk kurulum için model
    public class InitialSetupModel
    {
        // Firma bilgileri
        public string FirmaAdi { get; set; }
        public string VergiNo { get; set; }
        public string VergiDairesi { get; set; }
        public string Adres { get; set; }
        public string Il { get; set; }
        public string Ilce { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string WebSitesi { get; set; }

        // Kullanıcı bilgileri
        public string KullaniciAdi { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Parola { get; set; }
    }
}
