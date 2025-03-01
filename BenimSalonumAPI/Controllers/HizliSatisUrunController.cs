using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HizliSatisUrunController : ControllerBase
    {
        private readonly IRepository<HizliSatisUrunTable> _hizliSatisUrunRepository;

        public HizliSatisUrunController(IRepository<HizliSatisUrunTable> hizliSatisUrunRepository)
        {
            _hizliSatisUrunRepository = hizliSatisUrunRepository;
        }

        // 📌 1️⃣ TÜM HIZLI SATIŞ ÜRÜNLERİNİ GETİR (GET)
        [HttpGet]
        public async Task<IActionResult> GetAllHizliSatisUrunleri()
        {
            var urunListesi = await _hizliSatisUrunRepository.GetAllAsync();
            return Ok(urunListesi);
        }

        // 📌 2️⃣ TEK HIZLI SATIŞ ÜRÜNÜ GETİR (GET /{id})
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHizliSatisUrun(int id)
        {
            var urun = await _hizliSatisUrunRepository.GetByIdAsync(id);
            if (urun == null)
                return NotFound("Hızlı satış ürünü bulunamadı.");
            return Ok(urun);
        }

        // 📌 3️⃣ YENİ HIZLI SATIŞ ÜRÜNÜ EKLE (POST)
        [HttpPost]
        public async Task<IActionResult> CreateHizliSatisUrun([FromBody] HizliSatisUrunTable hizliSatisUrun)
        {
            if (hizliSatisUrun == null)
                return BadRequest("Geçersiz veri.");

            await _hizliSatisUrunRepository.AddAsync(hizliSatisUrun);
            await _hizliSatisUrunRepository.SaveChangesAsync();
            return Ok("Hızlı satış ürünü başarıyla eklendi.");
        }

        // 📌 4️⃣ HIZLI SATIŞ ÜRÜNÜ GÜNCELLE (PUT /{id})
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHizliSatisUrun(int id, [FromBody] HizliSatisUrunTable hizliSatisUrun)
        {
            if (id != hizliSatisUrun.Id)
                return BadRequest("ID eşleşmiyor.");

            await _hizliSatisUrunRepository.UpdateAsync(hizliSatisUrun);
            await _hizliSatisUrunRepository.SaveChangesAsync();
            return Ok("Hızlı satış ürünü güncellendi.");
        }

        // 📌 5️⃣ HIZLI SATIŞ ÜRÜNÜ SİL (DELETE /{id})
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHizliSatisUrun(int id)
        {
            var urun = await _hizliSatisUrunRepository.GetByIdAsync(id);
            if (urun == null)
                return NotFound("Hızlı satış ürünü bulunamadı.");

            await _hizliSatisUrunRepository.RemoveAsync(urun);
            await _hizliSatisUrunRepository.SaveChangesAsync();
            return Ok("Hızlı satış ürünü silindi.");
        }
    }
}
