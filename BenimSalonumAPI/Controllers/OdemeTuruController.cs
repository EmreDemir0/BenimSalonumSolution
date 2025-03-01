using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdemeTuruController : ControllerBase
    {
        private readonly IRepository<OdemeTuruTable> _odemeTuruRepository;

        public OdemeTuruController(IRepository<OdemeTuruTable> odemeTuruRepository)
        {
            _odemeTuruRepository = odemeTuruRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOdemeTurleri()
        {
            var odemeTuruListesi = await _odemeTuruRepository.GetAllAsync();
            return Ok(odemeTuruListesi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOdemeTuru(int id)
        {
            var odemeTuru = await _odemeTuruRepository.GetByIdAsync(id);
            if (odemeTuru == null)
                return NotFound("Ödeme türü bulunamadı.");
            return Ok(odemeTuru);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOdemeTuru([FromBody] OdemeTuruTable odemeTuru)
        {
            if (odemeTuru == null)
                return BadRequest("Geçersiz veri.");

            await _odemeTuruRepository.AddAsync(odemeTuru);
            await _odemeTuruRepository.SaveChangesAsync();
            return Ok("Ödeme türü başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOdemeTuru(int id, [FromBody] OdemeTuruTable odemeTuru)
        {
            if (id != odemeTuru.Id)
                return BadRequest("ID eşleşmiyor.");

            await _odemeTuruRepository.UpdateAsync(odemeTuru);
            await _odemeTuruRepository.SaveChangesAsync();
            return Ok("Ödeme türü güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOdemeTuru(int id)
        {
            var odemeTuru = await _odemeTuruRepository.GetByIdAsync(id);
            if (odemeTuru == null)
                return NotFound("Ödeme türü bulunamadı.");

            await _odemeTuruRepository.RemoveAsync(odemeTuru);
            await _odemeTuruRepository.SaveChangesAsync();
            return Ok("Ödeme türü silindi.");
        }
    }
}
