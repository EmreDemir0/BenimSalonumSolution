using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CariController : ControllerBase
    {
        private readonly IRepository<CariTable> _cariRepository;

        public CariController(IRepository<CariTable> cariRepository)
        {
            _cariRepository = cariRepository;
        }

        // 📌 1️⃣ TÜM CARİLERİ GETİR (GET)
        [HttpGet]
        public async Task<IActionResult> GetAllCaris()
        {
            var caris = await _cariRepository.GetAllAsync();
            return Ok(caris);
        }

        // 📌 2️⃣ TEK CARI GETİR (GET /{id})
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCari(int id)
        {
            var cari = await _cariRepository.GetByIdAsync(id);
            if (cari == null)
                return NotFound("Cari bulunamadı.");
            return Ok(cari);
        }

        // 📌 3️⃣ YENİ CARI EKLE (POST)
        [HttpPost]
        public async Task<IActionResult> CreateCari([FromBody] CariTable cari)
        {
            if (cari == null)
                return BadRequest("Geçersiz veri.");

            await _cariRepository.AddAsync(cari);
            await _cariRepository.SaveChangesAsync();
            return Ok("Cari başarıyla eklendi.");
        }

        // 📌 4️⃣ CARI GÜNCELLE (PUT /{id})
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCari(int id, [FromBody] CariTable cari)
        {
            if (id != cari.Id)
                return BadRequest("ID eşleşmiyor.");

            await _cariRepository.UpdateAsync(cari);
            await _cariRepository.SaveChangesAsync();
            return Ok("Cari güncellendi.");
        }

        // 📌 5️⃣ CARI SİL (DELETE /{id})
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCari(int id)
        {
            var cari = await _cariRepository.GetByIdAsync(id);
            if (cari == null)
                return NotFound("Cari bulunamadı.");

            await _cariRepository.RemoveAsync(cari);
            await _cariRepository.SaveChangesAsync();
            return Ok("Cari silindi.");
        }
    }
}
