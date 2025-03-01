using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TanimlarController : ControllerBase
    {
        private readonly IRepository<TanimlarTable> _tanimlarRepository;

        public TanimlarController(IRepository<TanimlarTable> tanimlarRepository)
        {
            _tanimlarRepository = tanimlarRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTanimlar()
        {
            var tanimlarListesi = await _tanimlarRepository.GetAllAsync();
            return Ok(tanimlarListesi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTanim(int id)
        {
            var tanim = await _tanimlarRepository.GetByIdAsync(id);
            if (tanim == null)
                return NotFound("Tanım bulunamadı.");
            return Ok(tanim);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTanim([FromBody] TanimlarTable tanim)
        {
            if (tanim == null)
                return BadRequest("Geçersiz veri.");

            await _tanimlarRepository.AddAsync(tanim);
            await _tanimlarRepository.SaveChangesAsync();
            return Ok("Tanım başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTanim(int id, [FromBody] TanimlarTable tanim)
        {
            if (id != tanim.Id)
                return BadRequest("ID eşleşmiyor.");

            await _tanimlarRepository.UpdateAsync(tanim);
            await _tanimlarRepository.SaveChangesAsync();
            return Ok("Tanım güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTanim(int id)
        {
            var tanim = await _tanimlarRepository.GetByIdAsync(id);
            if (tanim == null)
                return NotFound("Tanım bulunamadı.");

            await _tanimlarRepository.RemoveAsync(tanim);
            await _tanimlarRepository.SaveChangesAsync();
            return Ok("Tanım silindi.");
        }
    }
}
