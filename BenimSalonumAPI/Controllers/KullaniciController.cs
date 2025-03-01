using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly IRepository<KullaniciTable> _kullaniciRepository;

        public KullaniciController(IRepository<KullaniciTable> kullaniciRepository)
        {
            _kullaniciRepository = kullaniciRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKullanicilar()
        {
            var kullaniciListesi = await _kullaniciRepository.GetAllAsync();
            return Ok(kullaniciListesi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKullanici(int id)
        {
            var kullanici = await _kullaniciRepository.GetByIdAsync(id);
            if (kullanici == null)
                return NotFound("Kullanıcı bulunamadı.");
            return Ok(kullanici);
        }

        [HttpPost]
        public async Task<IActionResult> CreateKullanici([FromBody] KullaniciTable kullanici)
        {
            if (kullanici == null)
                return BadRequest("Geçersiz veri.");

            await _kullaniciRepository.AddAsync(kullanici);
            await _kullaniciRepository.SaveChangesAsync();
            return Ok("Kullanıcı başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKullanici(int id, [FromBody] KullaniciTable kullanici)
        {
            if (id != kullanici.Id)
                return BadRequest("ID eşleşmiyor.");

            await _kullaniciRepository.UpdateAsync(kullanici);
            await _kullaniciRepository.SaveChangesAsync();
            return Ok("Kullanıcı güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKullanici(int id)
        {
            var kullanici = await _kullaniciRepository.GetByIdAsync(id);
            if (kullanici == null)
                return NotFound("Kullanıcı bulunamadı.");

            await _kullaniciRepository.RemoveAsync(kullanici);
            await _kullaniciRepository.SaveChangesAsync();
            return Ok("Kullanıcı silindi.");
        }
    }
}
