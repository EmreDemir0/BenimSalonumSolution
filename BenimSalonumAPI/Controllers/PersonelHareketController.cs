using Microsoft.AspNetCore.Mvc;
using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonelHareketController : ControllerBase
    {
        private readonly IRepository<PersonelHareketTable> _personelHareketRepository;

        public PersonelHareketController(IRepository<PersonelHareketTable> personelHareketRepository)
        {
            _personelHareketRepository = personelHareketRepository;
        }

        // 📌 1️⃣ TÜM PERSONEL HAREKETLERİ GETİR (GET)
        [HttpGet]
        public async Task<IActionResult> GetAllPersonelHareket()
        {
            var personelHareketList = await _personelHareketRepository.GetAllAsync();
            return Ok(personelHareketList);
        }

        // 📌 2️⃣ TEK PERSONEL HAREKET GETİR (GET /{id})
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonelHareket(int id)
        {
            var personelHareket = await _personelHareketRepository.GetByIdAsync(id);
            if (personelHareket == null)
                return NotFound("Personel hareketi bulunamadı.");
            return Ok(personelHareket);
        }

        // 📌 3️⃣ YENİ PERSONEL HAREKETİ EKLE (POST)
        [HttpPost]
        public async Task<IActionResult> CreatePersonelHareket([FromBody] PersonelHareketTable personelHareket)
        {
            if (personelHareket == null)
                return BadRequest("Geçersiz veri.");

            await _personelHareketRepository.AddAsync(personelHareket);
            await _personelHareketRepository.SaveChangesAsync();
            return Ok("Personel hareketi başarıyla eklendi.");
        }

        // 📌 4️⃣ PERSONEL HAREKETİ GÜNCELLE (PUT /{id})
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonelHareket(int id, [FromBody] PersonelHareketTable personelHareket)
        {
            if (id != personelHareket.Id)
                return BadRequest("ID eşleşmiyor.");

            await _personelHareketRepository.UpdateAsync(personelHareket);
            await _personelHareketRepository.SaveChangesAsync();
            return Ok("Personel hareketi güncellendi.");
        }

        // 📌 5️⃣ PERSONEL HAREKETİ SİL (DELETE /{id})
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonelHareket(int id)
        {
            var personelHareket = await _personelHareketRepository.GetByIdAsync(id);
            if (personelHareket == null)
                return NotFound("Personel hareketi bulunamadı.");

            await _personelHareketRepository.RemoveAsync(personelHareket);
            await _personelHareketRepository.SaveChangesAsync();
            return Ok("Personel hareketi silindi.");
        }
    }
}
