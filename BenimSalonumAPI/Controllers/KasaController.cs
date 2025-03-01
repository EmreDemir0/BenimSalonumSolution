using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KasaController : ControllerBase
    {
        private readonly IRepository<KasaTable> _kasaRepository;

        public KasaController(IRepository<KasaTable> kasaRepository)
        {
            _kasaRepository = kasaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKasalar()
        {
            var kasaListesi = await _kasaRepository.GetAllAsync();
            return Ok(kasaListesi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKasa(int id)
        {
            var kasa = await _kasaRepository.GetByIdAsync(id);
            if (kasa == null)
                return NotFound("Kasa bulunamadı.");
            return Ok(kasa);
        }

        [HttpPost]
        public async Task<IActionResult> CreateKasa([FromBody] KasaTable kasa)
        {
            if (kasa == null)
                return BadRequest("Geçersiz veri.");

            await _kasaRepository.AddAsync(kasa);
            await _kasaRepository.SaveChangesAsync();
            return Ok("Kasa başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKasa(int id, [FromBody] KasaTable kasa)
        {
            if (id != kasa.Id)
                return BadRequest("ID eşleşmiyor.");

            await _kasaRepository.UpdateAsync(kasa);
            await _kasaRepository.SaveChangesAsync();
            return Ok("Kasa güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKasa(int id)
        {
            var kasa = await _kasaRepository.GetByIdAsync(id);
            if (kasa == null)
                return NotFound("Kasa bulunamadı.");

            await _kasaRepository.RemoveAsync(kasa);
            await _kasaRepository.SaveChangesAsync();
            return Ok("Kasa silindi.");
        }
    }
}
