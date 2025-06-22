using eShop.Data;
using eShop.Data.Contracts;
using eShop.Models.Catalog;
using eShop.Web.Models;
using eShop.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShop.Web.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository<Category> _repository;
        public CategoriesController(IRepository<Category> repository) 
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _repository.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // Simulate fetching a category by ID
            var category = await _repository.GetByIdAsync(id); // This is just for simulation, avoid using .Wait() in production code
            
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO category)
        {
            if (string.IsNullOrEmpty(category.CategoryName))
            {
                return BadRequest("Category cannot be empty.");
            }
            // Simulate creating a category

            _repository.Add(new Category
            {
                Id = Guid.NewGuid(),
                Name = category.CategoryName
            });

            await _repository.SaveChangesAsync(); // Simulate saving to the database

            return CreatedAtAction(nameof(GetById), new { id = Guid.NewGuid() }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return BadRequest("Category cannot be empty.");
            }

            var categoryToUpdate = await _repository.GetByIdAsync(id);
            _repository.Update(categoryToUpdate);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Simulate deleting a category
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid category ID.");
            }

            await _repository.Delete(id);

            return NoContent();
        }
    }
}
