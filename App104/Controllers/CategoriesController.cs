using App104.DAl;
using App104.DTOS;
using App104.Entities;
using App104.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App104.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IRepository _repository;

       

        public CategoriesController(AppDbContext context, IRepository repository)
        {
            _context = context;

            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 2)
        {

            IEnumerable<Category> categories = await _repository
                .GetAllAsync();

            return Ok(categories);


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category category = await _repository.GetByIdAsync(id);

            if (category is null) return StatusCode(StatusCodes.Status404NotFound);
            
            return StatusCode(StatusCodes.Status200OK, category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDto categoryDto)
        {
            Category category = new()
            {
                Name = categoryDto.Name,
            };
            await _repository.AddAsync(category);

            await _repository.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCategoryDto categoryDto)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category is null) return StatusCode(StatusCodes.Status404NotFound);
           
            category.Name = categoryDto.Name;
           
            _repository.Update(category);

            await _repository.SaveChangesAsync();

            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent, category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
           
            Category category = await _repository.GetByIdAsync(id);
           
            if (category is null) return StatusCode(StatusCodes.Status404NotFound);
            
            _repository.Delete(category);

            await _repository.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent, category);
        }
    }
}
