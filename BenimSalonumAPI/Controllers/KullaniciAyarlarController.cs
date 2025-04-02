using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
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
    public class KullaniciAyarlarController : ControllerBase
    {
        private readonly BenimSalonumContext _context;

        public KullaniciAyarlarController(BenimSalonumContext context)
        {
            _context = context;
        }

        // GET: api/KullaniciAyarlar
        [HttpGet]
        public async Task<ActionResult<KullaniciAyarlarDTO>> GetKullaniciAyarlar()
        {
            // JWT token'dan kullanıcı ID'sini al
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int kullaniciId))
            {
                return Unauthorized("Geçersiz kullanıcı kimliği.");
            }

            var kullaniciAyarlar = await _context.KullaniciAyarlar
                .FirstOrDefaultAsync(k => k.KullaniciId == kullaniciId);

            if (kullaniciAyarlar == null)
            {
                // Kullanıcı ayarları bulunamadıysa, varsayılan ayarlarla yeni bir kayıt oluştur
                kullaniciAyarlar = new KullaniciAyarlarTable
                {
                    KullaniciId = kullaniciId,
                    // Varsayılan değerler constructor içinde atanacak
                };

                _context.KullaniciAyarlar.Add(kullaniciAyarlar);
                await _context.SaveChangesAsync();
            }

            var ayarlarDTO = new KullaniciAyarlarDTO
            {
                Id = kullaniciAyarlar.Id,
                KullaniciId = kullaniciAyarlar.KullaniciId,
                EmailBildirimAktif = kullaniciAyarlar.EmailBildirimAktif,
                SMSBildirimAktif = kullaniciAyarlar.SMSBildirimAktif,
                UygulamaIciBildirimAktif = kullaniciAyarlar.UygulamaIciBildirimAktif,
                RandevuHatirlatmaZamani = kullaniciAyarlar.RandevuHatirlatmaZamani,
                Dil = kullaniciAyarlar.Dil,
                Tema = kullaniciAyarlar.Tema,
                CalismaBaslangicSaati = kullaniciAyarlar.CalismaBaslangicSaati,
                CalismaBitisSaati = kullaniciAyarlar.CalismaBitisSaati,
                OturumSuresi = kullaniciAyarlar.OturumSuresi,
                OtomatikKilitlemeAktif = kullaniciAyarlar.OtomatikKilitlemeAktif,
                OtomatikKilitlemeSuresi = kullaniciAyarlar.OtomatikKilitlemeSuresi
            };

            return ayarlarDTO;
        }

        // PUT: api/KullaniciAyarlar
        [HttpPut]
        public async Task<IActionResult> UpdateKullaniciAyarlar(KullaniciAyarlarGuncellemeDTO model)
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

            var kullaniciAyarlar = await _context.KullaniciAyarlar
                .FirstOrDefaultAsync(k => k.KullaniciId == kullaniciId);

            if (kullaniciAyarlar == null)
            {
                // Kullanıcı ayarları bulunamadıysa, yeni bir kayıt oluştur
                kullaniciAyarlar = new KullaniciAyarlarTable
                {
                    KullaniciId = kullaniciId
                };
                
                _context.KullaniciAyarlar.Add(kullaniciAyarlar);
            }

            // Kullanıcı ayarlarını güncelle
            kullaniciAyarlar.EmailBildirimAktif = model.EmailBildirimAktif;
            kullaniciAyarlar.SMSBildirimAktif = model.SMSBildirimAktif;
            kullaniciAyarlar.UygulamaIciBildirimAktif = model.UygulamaIciBildirimAktif;
            kullaniciAyarlar.RandevuHatirlatmaZamani = model.RandevuHatirlatmaZamani;
            kullaniciAyarlar.Dil = model.Dil;
            kullaniciAyarlar.Tema = model.Tema;
            kullaniciAyarlar.SonGuncellenmeTarihi = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Kullanıcı ayarları başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kullanıcı ayarları güncellenirken bir hata oluştu.", error = ex.Message });
            }
        }

        // PUT: api/KullaniciAyarlar/CalismaSaatleri
        [HttpPut("CalismaSaatleri")]
        public async Task<IActionResult> UpdateCalismaSaatleri(CalismaSaatleriGuncellemeDTO model)
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

            var kullaniciAyarlar = await _context.KullaniciAyarlar
                .FirstOrDefaultAsync(k => k.KullaniciId == kullaniciId);

            if (kullaniciAyarlar == null)
            {
                // Kullanıcı ayarları bulunamadıysa, yeni bir kayıt oluştur
                kullaniciAyarlar = new KullaniciAyarlarTable
                {
                    KullaniciId = kullaniciId
                };
                
                _context.KullaniciAyarlar.Add(kullaniciAyarlar);
            }

            // Çalışma saatlerini güncelle
            kullaniciAyarlar.CalismaBaslangicSaati = model.CalismaBaslangicSaati;
            kullaniciAyarlar.CalismaBitisSaati = model.CalismaBitisSaati;
            kullaniciAyarlar.SonGuncellenmeTarihi = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Çalışma saatleri başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Çalışma saatleri güncellenirken bir hata oluştu.", error = ex.Message });
            }
        }

        // PUT: api/KullaniciAyarlar/GuvenlikAyarlari
        [HttpPut("GuvenlikAyarlari")]
        public async Task<IActionResult> UpdateGuvenlikAyarlari(GuvenlikAyarlariGuncellemeDTO model)
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

            var kullaniciAyarlar = await _context.KullaniciAyarlar
                .FirstOrDefaultAsync(k => k.KullaniciId == kullaniciId);

            if (kullaniciAyarlar == null)
            {
                // Kullanıcı ayarları bulunamadıysa, yeni bir kayıt oluştur
                kullaniciAyarlar = new KullaniciAyarlarTable
                {
                    KullaniciId = kullaniciId
                };
                
                _context.KullaniciAyarlar.Add(kullaniciAyarlar);
            }

            // Güvenlik ayarlarını güncelle
            kullaniciAyarlar.OturumSuresi = model.OturumSuresi;
            kullaniciAyarlar.OtomatikKilitlemeAktif = model.OtomatikKilitlemeAktif;
            kullaniciAyarlar.OtomatikKilitlemeSuresi = model.OtomatikKilitlemeSuresi;
            kullaniciAyarlar.SonGuncellenmeTarihi = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Güvenlik ayarları başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Güvenlik ayarları güncellenirken bir hata oluştu.", error = ex.Message });
            }
        }
    }
}
