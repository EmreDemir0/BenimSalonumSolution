using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FisController : ControllerBase
    {
        private readonly IRepository<FisTable> _fisRepository;

        public FisController(IRepository<FisTable> fisRepository)
        {
            _fisRepository = fisRepository;
        }

        // 📌 1️⃣ TÜM FİŞLERİ GETİR (GET)
        [HttpGet]
        public async Task<IActionResult> GetAllFisler()
        {
            var fisler = await _fisRepository.GetAllAsync();
            return Ok(fisler);
        }

        // 📌 2️⃣ TEK FİŞ GETİR (GET /{id})
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFis(int id)
        {
            var fis = await _fisRepository.GetByIdAsync(id);
            if (fis == null)
                return NotFound("Fiş bulunamadı.");
            return Ok(fis);
        }

        // 📌 3️⃣ YENİ FİŞ EKLE (POST)
        [HttpPost]
        public async Task<IActionResult> CreateFis([FromBody] FisTable fis)
        {
            if (fis == null)
                return BadRequest("Geçersiz veri.");

            await _fisRepository.AddAsync(fis);
            await _fisRepository.SaveChangesAsync();
            return Ok("Fiş başarıyla eklendi.");
        }

        // 📌 4️⃣ FİŞ GÜNCELLE (PUT /{id})
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFis(int id, [FromBody] FisTable fis)
        {
            if (id != fis.Id)
                return BadRequest("ID eşleşmiyor.");

            await _fisRepository.UpdateAsync(fis);
            await _fisRepository.SaveChangesAsync();
            return Ok("Fiş güncellendi.");
        }

        // 📌 5️⃣ FİŞ SİL (DELETE /{id})
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFis(int id)
        {
            var fis = await _fisRepository.GetByIdAsync(id);
            if (fis == null)
                return NotFound("Fiş bulunamadı.");

            await _fisRepository.RemoveAsync(fis);
            await _fisRepository.SaveChangesAsync();
            return Ok("Fiş silindi.");
        }
    }
}
