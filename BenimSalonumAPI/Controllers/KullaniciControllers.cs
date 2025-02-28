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

        // 📌 1️⃣ TÜM KULLANICILARI GETİR (GET)
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _kullaniciRepository.GetAllAsync();
            return Ok(users);
        }

        // 📌 2️⃣ TEK KULLANICI GETİR (GET /{id})
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _kullaniciRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");
            return Ok(user);
        }

        // 📌 3️⃣ YENİ KULLANICI EKLE (POST)
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] KullaniciTable kullanici)
        {
            if (kullanici == null)
                return BadRequest("Geçersiz veri.");

            await _kullaniciRepository.AddAsync(kullanici);
            await _kullaniciRepository.SaveChangesAsync();
            return Ok("Kullanıcı başarıyla eklendi.");
        }

        // 📌 4️⃣ KULLANICI GÜNCELLE (PUT /{id})
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] KullaniciTable kullanici)
        {
            if (id != kullanici.Id)
                return BadRequest("ID eşleşmiyor.");

            await _kullaniciRepository.UpdateAsync(kullanici);
            await _kullaniciRepository.SaveChangesAsync();
            return Ok("Kullanıcı güncellendi.");
        }

        // 📌 5️⃣ KULLANICI SİL (DELETE /{id})
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _kullaniciRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            await _kullaniciRepository.RemoveAsync(user);
            await _kullaniciRepository.SaveChangesAsync();
            return Ok("Kullanıcı silindi.");
        }
    }

}
