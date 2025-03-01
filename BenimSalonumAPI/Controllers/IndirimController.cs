using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndirimController : ControllerBase
    {
        private readonly IRepository<IndirimTable> _indirimRepository;

        public IndirimController(IRepository<IndirimTable> indirimRepository)
        {
            _indirimRepository = indirimRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIndirimler()
        {
            var indirimListesi = await _indirimRepository.GetAllAsync();
            return Ok(indirimListesi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIndirim(int id)
        {
            var indirim = await _indirimRepository.GetByIdAsync(id);
            if (indirim == null)
                return NotFound("İndirim bulunamadı.");
            return Ok(indirim);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIndirim([FromBody] IndirimTable indirim)
        {
            if (indirim == null)
                return BadRequest("Geçersiz veri.");

            await _indirimRepository.AddAsync(indirim);
            await _indirimRepository.SaveChangesAsync();
            return Ok("İndirim başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIndirim(int id, [FromBody] IndirimTable indirim)
        {
            if (id != indirim.Id)
                return BadRequest("ID eşleşmiyor.");

            await _indirimRepository.UpdateAsync(indirim);
            await _indirimRepository.SaveChangesAsync();
            return Ok("İndirim güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndirim(int id)
        {
            var indirim = await _indirimRepository.GetByIdAsync(id);
            if (indirim == null)
                return NotFound("İndirim bulunamadı.");

            await _indirimRepository.RemoveAsync(indirim);
            await _indirimRepository.SaveChangesAsync();
            return Ok("İndirim silindi.");
        }
    }
}
