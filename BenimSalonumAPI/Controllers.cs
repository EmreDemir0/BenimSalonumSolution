using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly IRepository<KullaniciTable> _kullaniciRepository;

        public KullaniciController(IRepository<KullaniciTable> kullaniciRepository)
        {
            _kullaniciRepository = kullaniciRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _kullaniciRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _kullaniciRepository.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KullaniciTable kullanici)
        {
            await _kullaniciRepository.AddAsync(kullanici);
            return CreatedAtAction(nameof(GetById), new { id = kullanici.Id }, kullanici);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, KullaniciTable kullanici)
        {
            if (id != kullanici.Id) return BadRequest();
            _kullaniciRepository.Update(kullanici);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var kullanici = _kullaniciRepository.GetByIdAsync(id).Result;
            if (kullanici == null) return NotFound();
            _kullaniciRepository.Remove(kullanici);
            return NoContent();
        }
    }
}
