using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenimSalonum.Entities.Interfaces;
using BenimSalonum.Entities.Tables;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StokHareketController : ControllerBase
    {
        private readonly IRepository<StokHareketTable> _stokHareketRepository;

        public StokHareketController(IRepository<StokHareketTable> stokHareketRepository)
        {
            _stokHareketRepository = stokHareketRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStokHareketleri()
        {
            var stokHareketListesi = await _stokHareketRepository.GetAllAsync();
            return Ok(stokHareketListesi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStokHareket(int id)
        {
            var stokHareket = await _stokHareketRepository.GetByIdAsync(id);
            if (stokHareket == null)
                return NotFound("Stok hareketi bulunamadı.");
            return Ok(stokHareket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStokHareket([FromBody] StokHareketTable stokHareket)
        {
            if (stokHareket == null)
                return BadRequest("Geçersiz veri.");

            await _stokHareketRepository.AddAsync(stokHareket);
            await _stokHareketRepository.SaveChangesAsync();
            return Ok("Stok hareketi başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStokHareket(int id, [FromBody] StokHareketTable stokHareket)
        {
            if (id != stokHareket.Id)
                return BadRequest("ID eşleşmiyor.");

            await _stokHareketRepository.UpdateAsync(stokHareket);
            await _stokHareketRepository.SaveChangesAsync();
            return Ok("Stok hareketi güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStokHareket(int id)
        {
            var stokHareket = await _stokHareketRepository.GetByIdAsync(id);
            if (stokHareket == null)
                return NotFound("Stok hareketi bulunamadı.");

            await _stokHareketRepository.RemoveAsync(stokHareket);
            await _stokHareketRepository.SaveChangesAsync();
            return Ok("Stok hareketi silindi.");
        }
    }
}
