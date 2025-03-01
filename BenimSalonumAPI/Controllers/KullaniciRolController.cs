using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciRolController : ControllerBase
    {
        private readonly IRepository<KullaniciRolTable> _kullaniciRolRepository;

        public KullaniciRolController(IRepository<KullaniciRolTable> kullaniciRolRepository)
        {
            _kullaniciRolRepository = kullaniciRolRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKullaniciRolleri()
        {
            var kullaniciRolListesi = await _kullaniciRolRepository.GetAllAsync();
            return Ok(kullaniciRolListesi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKullaniciRol(int id)
        {
            var kullaniciRol = await _kullaniciRolRepository.GetByIdAsync(id);
            if (kullaniciRol == null)
                return NotFound("Kullanıcı rolü bulunamadı.");
            return Ok(kullaniciRol);
        }

        [HttpPost]
        public async Task<IActionResult> CreateKullaniciRol([FromBody] KullaniciRolTable kullaniciRol)
        {
            if (kullaniciRol == null)
                return BadRequest("Geçersiz veri.");

            await _kullaniciRolRepository.AddAsync(kullaniciRol);
            await _kullaniciRolRepository.SaveChangesAsync();
            return Ok("Kullanıcı rolü başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKullaniciRol(int id, [FromBody] KullaniciRolTable kullaniciRol)
        {
            if (id != kullaniciRol.Id)
                return BadRequest("ID eşleşmiyor.");

            await _kullaniciRolRepository.UpdateAsync(kullaniciRol);
            await _kullaniciRolRepository.SaveChangesAsync();
            return Ok("Kullanıcı rolü güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKullaniciRol(int id)
        {
            var kullaniciRol = await _kullaniciRolRepository.GetByIdAsync(id);
            if (kullaniciRol == null)
                return NotFound("Kullanıcı rolü bulunamadı.");

            await _kullaniciRolRepository.RemoveAsync(kullaniciRol);
            await _kullaniciRolRepository.SaveChangesAsync();
            return Ok("Kullanıcı rolü silindi.");
        }
    }
}
