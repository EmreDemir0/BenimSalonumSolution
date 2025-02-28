using BenimSalonum.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BenimSalonumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase where T : class
    {
        private readonly IRepository<T> _repository;

        public BaseController(IRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repository.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] T entity)
        {
            if (entity == null) return BadRequest();
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return Ok("Kayıt eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] T entity)
        {
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
            return Ok("Kayıt güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return NotFound();
            await _repository.RemoveAsync(item);
            await _repository.SaveChangesAsync();
            return Ok("Kayıt silindi.");
        }
    }
}
