using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StokController : ControllerBase
    {
        private readonly IRepository<StokTable> _stokRepository;

        public StokController(IRepository<StokTable> stokRepository)
        {
            _stokRepository = stokRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStoklar()
        {
            var stokListesi = await _stokRepository.GetAllAsync();
            return Ok(stokListesi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStok(int id)
        {
            var stok = await _stokRepository.GetByIdAsync(id);
            if (stok == null)
                return NotFound("Stok bulunamadı.");
            return Ok(stok);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStok([FromBody] StokTable stok)
        {
            if (stok == null)
                return BadRequest("Geçersiz veri.");

            await _stokRepository.AddAsync(stok);
            await _stokRepository.SaveChangesAsync();
            return Ok("Stok başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStok(int id, [FromBody] StokTable stok)
        {
            if (id != stok.Id)
                return BadRequest("ID eşleşmiyor.");

            await _stokRepository.UpdateAsync(stok);
            await _stokRepository.SaveChangesAsync();
            return Ok("Stok güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStok(int id)
        {
            var stok = await _stokRepository.GetByIdAsync(id);
            if (stok == null)
                return NotFound("Stok bulunamadı.");

            await _stokRepository.RemoveAsync(stok);
            await _stokRepository.SaveChangesAsync();
            return Ok("Stok silindi.");
        }
    }
}
