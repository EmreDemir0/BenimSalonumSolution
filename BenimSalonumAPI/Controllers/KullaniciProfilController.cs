using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using BenimSalonum.Entities.DTOs;
using BenimSalonum.Entities.Tables;
using BenimSalonumAPI.DataAccess.Context;
using System.Security.Claims;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KullaniciProfilController : ControllerBase
    {
        private readonly BenimSalonumContext _context;

        public KullaniciProfilController(BenimSalonumContext context)
        {
            _context = context;
        }

        // GET: api/KullaniciProfil
        [HttpGet]
        public async Task<ActionResult<KullaniciProfilDTO>> GetKullaniciProfil()
        {
            // JWT token'dan kullanıcı ID'sini al
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int kullaniciId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }

            var kullanici = await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.Id == kullaniciId);

            if (kullanici == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var kullaniciProfil = new KullaniciProfilDTO
            {
                Id = kullanici.Id,
                KullaniciAdi = kullanici.KullaniciAdi,
                Adi = kullanici.Adi,
                Soyadi = kullanici.Soyadi,
                Gorevi = kullanici.Gorevi,
                Email = kullanici.Email,
                Telefon = kullanici.Telefon,
                ProfilResmiUrl = kullanici.ProfilResmiUrl,
                Adres = kullanici.Adres,
                Sehir = kullanici.Sehir,
                DogumTarihi = kullanici.DogumTarihi,
                Cinsiyet = kullanici.Cinsiyet,
                IkiFaktorluKimlikDogrulama = kullanici.IkiFaktorluKimlikDogrulama,
                SonGirisTarihi = kullanici.SonGirisTarihi
            };

            return kullaniciProfil;
        }

        // PUT: api/KullaniciProfil
        [HttpPut]
        public async Task<IActionResult> UpdateKullaniciProfil(KullaniciProfilGuncellemeDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // JWT token'dan kullanıcı ID'sini al
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int kullaniciId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }

            var kullanici = await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.Id == kullaniciId);

            if (kullanici == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            // Kullanıcı profilini güncelle
            kullanici.Adi = model.Adi;
            kullanici.Soyadi = model.Soyadi;
            kullanici.Email = model.Email;
            kullanici.Telefon = model.Telefon;
            kullanici.Adres = model.Adres;
            kullanici.Sehir = model.Sehir;
            kullanici.DogumTarihi = model.DogumTarihi;
            kullanici.Cinsiyet = model.Cinsiyet;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Profil bilgileri başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Profil güncellenirken bir hata oluştu.", error = ex.Message });
            }
        }

        // PUT: api/KullaniciProfil/ProfilResmi
        [HttpPut("ProfilResmi")]
        public async Task<IActionResult> UpdateProfilResmi(KullaniciProfilResmiGuncellemeDTO model)
        {
            if (string.IsNullOrEmpty(model.ProfilResmiUrl))
            {
                return BadRequest("Profil resmi URL'i geçersiz.");
            }

            // JWT token'dan kullanıcı ID'sini al
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int kullaniciId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }

            var kullanici = await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.Id == kullaniciId);

            if (kullanici == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            // Profil resmini güncelle
            kullanici.ProfilResmiUrl = model.ProfilResmiUrl;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Profil resmi başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Profil resmi güncellenirken bir hata oluştu.", error = ex.Message });
            }
        }

        // PUT: api/KullaniciProfil/SifreDegistir
        [HttpPut("SifreDegistir")]
        public async Task<IActionResult> SifreDegistir(SifreDegistirmeDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Şifrelerin eşleşip eşleşmediğini kontrol et
            if (model.YeniSifre != model.YeniSifreTekrar)
            {
                return BadRequest(new { message = "Yeni şifreler eşleşmiyor." });
            }

            // JWT token'dan kullanıcı ID'sini al
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int kullaniciId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }

            var kullanici = await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.Id == kullaniciId);

            if (kullanici == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            // Mevcut şifre doğrulaması (burada şifre karşılaştırması yapılmalı)
            if (kullanici.Parola != model.MevcutSifre) // Gerçek uygulamada hash karşılaştırma yapılmalı
            {
                return BadRequest(new { message = "Mevcut şifre yanlış." });
            }

            // Şifreyi güncelle
            kullanici.Parola = model.YeniSifre; // Gerçek uygulamada hash'leme yapılmalı
            kullanici.SifreDegistirmeTarihi = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                
                // Başarılı şifre değiştirme işleminden sonra tüm oturumları sonlandır
                var refreshTokens = await _context.RefreshTokens
                    .Where(r => r.UserId == kullaniciId.ToString() && !r.IsRevoked)
                    .ToListAsync();
                
                foreach (var token in refreshTokens)
                {
                    token.IsRevoked = true;
                    token.RevokedAt = DateTime.Now;
                    token.ReasonRevoked = "Şifre değiştirildi.";
                }
                
                await _context.SaveChangesAsync();
                
                return Ok(new { message = "Şifre başarıyla değiştirildi. Lütfen tekrar giriş yapın." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Şifre değiştirilirken bir hata oluştu.", error = ex.Message });
            }
        }

        // PUT: api/KullaniciProfil/IkiFaktorluDogrulama
        [HttpPut("IkiFaktorluDogrulama")]
        public async Task<IActionResult> IkiFaktorluDogrulamaAyarla(IkiFaktorluDogrulamaDTO model)
        {
            // JWT token'dan kullanıcı ID'sini al
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int kullaniciId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }

            var kullanici = await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.Id == kullaniciId);

            if (kullanici == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            // İki faktörlü doğrulama durumunu güncelle
            kullanici.IkiFaktorluKimlikDogrulama = model.Aktif;
            
            // İki faktörlü doğrulama aktifleştiriliyorsa, bir anahtar oluştur
            if (model.Aktif && string.IsNullOrEmpty(kullanici.IkiFaktorluDogrulamaAnahtari))
            {
                kullanici.IkiFaktorluDogrulamaAnahtari = Guid.NewGuid().ToString(); // Gerçek uygulamada daha güvenli bir yöntem kullanılmalı
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { 
                    message = model.Aktif ? 
                        "İki faktörlü kimlik doğrulama aktifleştirildi." : 
                        "İki faktörlü kimlik doğrulama devre dışı bırakıldı.",
                    secret = model.Aktif ? kullanici.IkiFaktorluDogrulamaAnahtari : null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "İki faktörlü kimlik doğrulama ayarı güncellenirken bir hata oluştu.", error = ex.Message });
            }
        }
    }
}
