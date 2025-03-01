using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepoController : ControllerBase
    {
        private readonly IRepository<DepoTable> _depoRepository;

        public DepoController(IRepository<DepoTable> depoRepository)
        {
            _depoRepository = depoRepository;
        }

        // 📌 1️⃣ TÜM DEPOLARI GETİR (GET)
        [HttpGet]
        public async Task<IActionResult> GetAllDepos()
        {
            var depolar = await _depoRepository.GetAllAsync();
            return Ok(depolar);
        }

        // 📌 2️⃣ TEK DEPO GETİR (GET /{id})
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepo(int id)
        {
            var depo = await _depoRepository.GetByIdAsync(id);
            if (depo == null)
                return NotFound("Depo bulunamadı.");
            return Ok(depo);
        }

        // 📌 3️⃣ YENİ DEPO EKLE (POST)
        [HttpPost]
        public async Task<IActionResult> CreateDepo([FromBody] DepoTable depo)
        {
            if (depo == null)
                return BadRequest("Geçersiz veri.");

            await _depoRepository.AddAsync(depo);
            await _depoRepository.SaveChangesAsync();
            return Ok("Depo başarıyla eklendi.");
        }

        // 📌 4️⃣ DEPO GÜNCELLE (PUT /{id})
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepo(int id, [FromBody] DepoTable depo)
        {
            if (id != depo.Id)
                return BadRequest("ID eşleşmiyor.");

            await _depoRepository.UpdateAsync(depo);
            await _depoRepository.SaveChangesAsync();
            return Ok("Depo güncellendi.");
        }

        // 📌 5️⃣ DEPO SİL (DELETE /{id})
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepo(int id)
        {
            var depo = await _depoRepository.GetByIdAsync(id);
            if (depo == null)
                return NotFound("Depo bulunamadı.");

            await _depoRepository.RemoveAsync(depo);
            await _depoRepository.SaveChangesAsync();
            return Ok("Depo silindi.");
        }
    }
}
