using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciLogController : ControllerBase
    {
        private readonly IRepository<KullaniciLogTable> _kullaniciLogRepository;

        public KullaniciLogController(IRepository<KullaniciLogTable> kullaniciLogRepository)
        {
            _kullaniciLogRepository = kullaniciLogRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKullaniciLogs()
        {
            var kullaniciLogListesi = await _kullaniciLogRepository.GetAllAsync();
            return Ok(kullaniciLogListesi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKullaniciLog(int id)
        {
            var kullaniciLog = await _kullaniciLogRepository.GetByIdAsync(id);
            if (kullaniciLog == null)
                return NotFound("Kullanıcı logu bulunamadı.");
            return Ok(kullaniciLog);
        }

        [HttpPost]
        public async Task<IActionResult> CreateKullaniciLog([FromBody] KullaniciLogTable kullaniciLog)
        {
            if (kullaniciLog == null)
                return BadRequest("Geçersiz veri.");

            await _kullaniciLogRepository.AddAsync(kullaniciLog);
            await _kullaniciLogRepository.SaveChangesAsync();
            return Ok("Kullanıcı logu başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKullaniciLog(int id, [FromBody] KullaniciLogTable kullaniciLog)
        {
            if (id != kullaniciLog.Id)
                return BadRequest("ID eşleşmiyor.");

            await _kullaniciLogRepository.UpdateAsync(kullaniciLog);
            await _kullaniciLogRepository.SaveChangesAsync();
            return Ok("Kullanıcı logu güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKullaniciLog(int id)
        {
            var kullaniciLog = await _kullaniciLogRepository.GetByIdAsync(id);
            if (kullaniciLog == null)
                return NotFound("Kullanıcı logu bulunamadı.");

            await _kullaniciLogRepository.RemoveAsync(kullaniciLog);
            await _kullaniciLogRepository.SaveChangesAsync();
            return Ok("Kullanıcı logu silindi.");
        }
    }
}
