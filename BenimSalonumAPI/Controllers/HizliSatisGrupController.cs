using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HizliSatisGrupController : ControllerBase
    {
        private readonly IRepository<HizliSatisGrupTable> _hizliSatisGrupRepository;

        public HizliSatisGrupController(IRepository<HizliSatisGrupTable> hizliSatisGrupRepository)
        {
            _hizliSatisGrupRepository = hizliSatisGrupRepository;
        }

        // 📌 1️⃣ TÜM HIZLI SATIŞ GRUPLARINI GETİR (GET)
        [HttpGet]
        public async Task<IActionResult> GetAllHizliSatisGruplari()
        {
            var grupListesi = await _hizliSatisGrupRepository.GetAllAsync();
            return Ok(grupListesi);
        }

        // 📌 2️⃣ TEK HIZLI SATIŞ GRUBU GETİR (GET /{id})
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHizliSatisGrup(int id)
        {
            var grup = await _hizliSatisGrupRepository.GetByIdAsync(id);
            if (grup == null)
                return NotFound("Hızlı satış grubu bulunamadı.");
            return Ok(grup);
        }

        // 📌 3️⃣ YENİ HIZLI SATIŞ GRUBU EKLE (POST)
        [HttpPost]
        public async Task<IActionResult> CreateHizliSatisGrup([FromBody] HizliSatisGrupTable hizliSatisGrup)
        {
            if (hizliSatisGrup == null)
                return BadRequest("Geçersiz veri.");

            await _hizliSatisGrupRepository.AddAsync(hizliSatisGrup);
            await _hizliSatisGrupRepository.SaveChangesAsync();
            return Ok("Hızlı satış grubu başarıyla eklendi.");
        }

        // 📌 4️⃣ HIZLI SATIŞ GRUBU GÜNCELLE (PUT /{id})
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHizliSatisGrup(int id, [FromBody] HizliSatisGrupTable hizliSatisGrup)
        {
            if (id != hizliSatisGrup.Id)
                return BadRequest("ID eşleşmiyor.");

            await _hizliSatisGrupRepository.UpdateAsync(hizliSatisGrup);
            await _hizliSatisGrupRepository.SaveChangesAsync();
            return Ok("Hızlı satış grubu güncellendi.");
        }

        // 📌 5️⃣ HIZLI SATIŞ GRUBU SİL (DELETE /{id})
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHizliSatisGrup(int id)
        {
            var grup = await _hizliSatisGrupRepository.GetByIdAsync(id);
            if (grup == null)
                return NotFound("Hızlı satış grubu bulunamadı.");

            await _hizliSatisGrupRepository.RemoveAsync(grup);
            await _hizliSatisGrupRepository.SaveChangesAsync();
            return Ok("Hızlı satış grubu silindi.");
        }
    }
}
