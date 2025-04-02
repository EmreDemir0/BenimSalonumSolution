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

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Sadece yetkili kullanıcılar erişebilir
    public class FirmaController : ControllerBase
    {
        private readonly BenimSalonumContext _context;
        private readonly ILisansService _lisansService;
        private readonly ILogService _logService;

        public FirmaController(
            BenimSalonumContext context, 
            ILisansService lisansService,
            ILogService logService)
        {
            _context = context;
            _lisansService = lisansService;
            _logService = logService;
        }

        // Firma detaylarını getir
        [HttpGet]
        public async Task<IActionResult> GetFirma()
        {
            try
            {
                // Kullanıcının firma ID'sini al
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

                var firma = await _context.Firmalar
                    .FirstOrDefaultAsync(f => f.Id == user.FirmaId);

                if (firma == null)
                {
                    return NotFound("Firma bilgisi bulunamadı.");
                }

                await _logService.LogInformationAsync(
                    $"Firma bilgileri görüntülendi: {firma.FirmaAdi}", 
                    "FirmaController");

                return Ok(firma);
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync("Firma bilgileri getirilirken hata oluştu", ex, "FirmaController");
                return StatusCode(500, "Firma bilgileri getirilirken bir hata oluştu.");
            }
        }

        // Firma bilgilerini güncelle
        [HttpPut]
        public async Task<IActionResult> UpdateFirma([FromBody] FirmaTable firma)
        {
            try
            {
                // Kullanıcının firma ID'sini al
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

                // Sadece kendi firmasını güncelleyebilir
                if (user.FirmaId != firma.Id)
                {
                    return Forbid("Bu firmayı güncelleme yetkiniz yok.");
                }

                // Sadece ana kullanıcılar firma bilgilerini güncelleyebilir
                if (!user.AnaKullanici)
                {
                    return Forbid("Firma bilgilerini güncelleme yetkiniz yok.");
                }

                // Mevcut firmayı kontrol et
                var mevcutFirma = await _context.Firmalar.FindAsync(firma.Id);
                if (mevcutFirma == null)
                {
                    return NotFound("Güncellenecek firma bulunamadı.");
                }

                // Firma bilgilerini güncelle
                mevcutFirma.FirmaAdi = firma.FirmaAdi;
                mevcutFirma.VergiNo = firma.VergiNo;
                mevcutFirma.VergiDairesi = firma.VergiDairesi;
                mevcutFirma.MersisNo = firma.MersisNo;
                mevcutFirma.Adres = firma.Adres;
                mevcutFirma.Il = firma.Il;
                mevcutFirma.Ilce = firma.Ilce;
                mevcutFirma.Telefon = firma.Telefon;
                mevcutFirma.Email = firma.Email;
                mevcutFirma.WebSitesi = firma.WebSitesi;
                mevcutFirma.GuncellenmeTarihi = DateTime.Now;
                mevcutFirma.GuncelleyenKullaniciId = int.Parse(userId);

                await _context.SaveChangesAsync();

                await _logService.LogInformationAsync(
                    $"Firma bilgileri güncellendi: {mevcutFirma.FirmaAdi}", 
                    "FirmaController");

                return Ok(mevcutFirma);
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync("Firma bilgileri güncellenirken hata oluştu", ex, "FirmaController");
                return StatusCode(500, "Firma bilgileri güncellenirken bir hata oluştu.");
            }
        }

        // Lisans bilgilerini getir
        [HttpGet("lisans")]
        public async Task<IActionResult> GetLisans()
        {
            try
            {
                // Kullanıcının firma ID'sini al
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

                // Sadece ana kullanıcılar lisans bilgilerini görebilir
                if (!user.AnaKullanici)
                {
                    return Forbid("Lisans bilgilerini görüntüleme yetkiniz yok.");
                }

                var lisans = await _lisansService.LisansBilgisiGetirAsync(user.FirmaId.Value);
                if (lisans == null)
                {
                    return NotFound("Lisans bilgisi bulunamadı.");
                }

                await _logService.LogInformationAsync(
                    $"Lisans bilgileri görüntülendi: {lisans.LisansKodu}", 
                    "FirmaController");

                return Ok(new
                {
                    LisansKodu = lisans.LisansKodu,
                    BaslangicTarihi = lisans.BaslangicTarihi,
                    BitisTarihi = lisans.BitisTarihi,
                    KullaniciSayisiLimiti = lisans.KullaniciSayisiLimiti,
                    Aktif = lisans.Aktif,
                    LisansTuru = lisans.LisansTuru,
                    KalanGun = (lisans.BitisTarihi - DateTime.Now).Days,
                    SonKontrolTarihi = lisans.SonKontrolTarihi
                });
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync("Lisans bilgileri getirilirken hata oluştu", ex, "FirmaController");
                return StatusCode(500, "Lisans bilgileri getirilirken bir hata oluştu.");
            }
        }

        // Lisans etkinleştirme
        [HttpPost("lisans/activate")]
        public async Task<IActionResult> ActivateLisans([FromBody] string lisansKodu)
        {
            try
            {
                // Kullanıcının firma ID'sini al
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

                // Sadece ana kullanıcılar lisans etkinleştirebilir
                if (!user.AnaKullanici)
                {
                    return Forbid("Lisans etkinleştirme yetkiniz yok.");
                }

                // Firma bilgilerini al
                var firma = await _context.Firmalar.FindAsync(user.FirmaId.Value);
                if (firma == null)
                {
                    return NotFound("Firma bilgisi bulunamadı.");
                }

                // Lisansı aktifleştir
                var lisans = await _lisansService.LisansAktifleştirAsync(lisansKodu, firma.FirmaAdi, firma.Id);

                await _logService.LogInformationAsync(
                    $"Lisans etkinleştirildi: {lisansKodu}", 
                    "FirmaController");

                return Ok(new
                {
                    LisansKodu = lisans.LisansKodu,
                    BaslangicTarihi = lisans.BaslangicTarihi,
                    BitisTarihi = lisans.BitisTarihi,
                    KullaniciSayisiLimiti = lisans.KullaniciSayisiLimiti,
                    Aktif = lisans.Aktif,
                    LisansTuru = lisans.LisansTuru,
                    KalanGun = (lisans.BitisTarihi - DateTime.Now).Days
                });
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync("Lisans etkinleştirilirken hata oluştu", ex, "FirmaController");
                return StatusCode(500, "Lisans etkinleştirilirken bir hata oluştu.");
            }
        }

        // Lisans kontrolü
        [HttpGet("lisans/check")]
        public async Task<IActionResult> CheckLisans()
        {
            try
            {
                // Kullanıcının firma ID'sini al
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

                // Lisans kontrolü yap
                var lisansGecerli = await _lisansService.GunlukLisansKontrolEtAsync(user.FirmaId.Value);

                await _logService.LogInformationAsync(
                    $"Lisans kontrolü yapıldı: {(lisansGecerli ? "Geçerli" : "Geçersiz")}", 
                    "FirmaController");

                return Ok(new { LisansGecerli = lisansGecerli });
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync("Lisans kontrolü yapılırken hata oluştu", ex, "FirmaController");
                return StatusCode(500, "Lisans kontrolü yapılırken bir hata oluştu.");
            }
        }
    }
}
