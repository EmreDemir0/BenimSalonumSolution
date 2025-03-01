using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KasaHareketController : ControllerBase
    {
        private readonly IRepository<KasaHareketTable> _kasaHareketRepository;

        public KasaHareketController(IRepository<KasaHareketTable> kasaHareketRepository)
        {
            _kasaHareketRepository = kasaHareketRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKasaHareketleri()
        {
            var kasaHareketListesi = await _kasaHareketRepository.GetAllAsync();
            return Ok(kasaHareketListesi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKasaHareket(int id)
        {
            var kasaHareket = await _kasaHareketRepository.GetByIdAsync(id);
            if (kasaHareket == null)
                return NotFound("Kasa hareketi bulunamadı.");
            return Ok(kasaHareket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateKasaHareket([FromBody] KasaHareketTable kasaHareket)
        {
            if (kasaHareket == null)
                return BadRequest("Geçersiz veri.");

            await _kasaHareketRepository.AddAsync(kasaHareket);
            await _kasaHareketRepository.SaveChangesAsync();
            return Ok("Kasa hareketi başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKasaHareket(int id, [FromBody] KasaHareketTable kasaHareket)
        {
            if (id != kasaHareket.Id)
                return BadRequest("ID eşleşmiyor.");

            await _kasaHareketRepository.UpdateAsync(kasaHareket);
            await _kasaHareketRepository.SaveChangesAsync();
            return Ok("Kasa hareketi güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKasaHareket(int id)
        {
            var kasaHareket = await _kasaHareketRepository.GetByIdAsync(id);
            if (kasaHareket == null)
                return NotFound("Kasa hareketi bulunamadı.");

            await _kasaHareketRepository.RemoveAsync(kasaHareket);
            await _kasaHareketRepository.SaveChangesAsync();
            return Ok("Kasa hareketi silindi.");
        }
    }
}
