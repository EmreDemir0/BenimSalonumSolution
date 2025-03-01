using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KodController : ControllerBase
    {
        private readonly IRepository<KodTable> _kodRepository;

        public KodController(IRepository<KodTable> kodRepository)
        {
            _kodRepository = kodRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKodlar()
        {
            var kodListesi = await _kodRepository.GetAllAsync();
            return Ok(kodListesi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKod(int id)
        {
            var kod = await _kodRepository.GetByIdAsync(id);
            if (kod == null)
                return NotFound("Kod bulunamadı.");
            return Ok(kod);
        }

        [HttpPost]
        public async Task<IActionResult> CreateKod([FromBody] KodTable kod)
        {
            if (kod == null)
                return BadRequest("Geçersiz veri.");

            await _kodRepository.AddAsync(kod);
            await _kodRepository.SaveChangesAsync();
            return Ok("Kod başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKod(int id, [FromBody] KodTable kod)
        {
            if (id != kod.Id)
                return BadRequest("ID eşleşmiyor.");

            await _kodRepository.UpdateAsync(kod);
            await _kodRepository.SaveChangesAsync();
            return Ok("Kod güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKod(int id)
        {
            var kod = await _kodRepository.GetByIdAsync(id);
            if (kod == null)
                return NotFound("Kod bulunamadı.");

            await _kodRepository.RemoveAsync(kod);
            await _kodRepository.SaveChangesAsync();
            return Ok("Kod silindi.");
        }
    }
}
