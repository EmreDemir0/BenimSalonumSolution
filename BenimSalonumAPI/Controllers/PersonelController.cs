using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonelController : ControllerBase
    {
        private readonly IRepository<PersonelTable> _personelRepository;

        public PersonelController(IRepository<PersonelTable> personelRepository)
        {
            _personelRepository = personelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersonel()
        {
            var personelListesi = await _personelRepository.GetAllAsync();
            return Ok(personelListesi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonel(int id)
        {
            var personel = await _personelRepository.GetByIdAsync(id);
            if (personel == null)
                return NotFound("Personel bulunamadı.");
            return Ok(personel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonel([FromBody] PersonelTable personel)
        {
            if (personel == null)
                return BadRequest("Geçersiz veri.");

            await _personelRepository.AddAsync(personel);
            await _personelRepository.SaveChangesAsync();
            return Ok("Personel başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonel(int id, [FromBody] PersonelTable personel)
        {
            if (id != personel.Id)
                return BadRequest("ID eşleşmiyor.");

            await _personelRepository.UpdateAsync(personel);
            await _personelRepository.SaveChangesAsync();
            return Ok("Personel güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonel(int id)
        {
            var personel = await _personelRepository.GetByIdAsync(id);
            if (personel == null)
                return NotFound("Personel bulunamadı.");

            await _personelRepository.RemoveAsync(personel);
            await _personelRepository.SaveChangesAsync();
            return Ok("Personel silindi.");
        }
    }
}
