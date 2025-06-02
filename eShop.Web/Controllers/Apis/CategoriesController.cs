using eShop.Data;
using eShop.Data.Contracts;
using eShop.Models.Catalog;
using eShop.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Web.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly EShopDbContext _context;
        private readonly IRepository<Category> _repository;
        public CategoriesController(EShopDbContext context, IRepository<Category> repository) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _repository.GetAllAsync(x => x.Name == "A");
            var categories = _context.Categories.ToList(); // Simulate fetching categories from the database
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            // Simulate fetching a category by ID
            var category = $"Category {id}";
            if (string.IsNullOrEmpty(category))
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCategoryDTO category)
        {
            if (string.IsNullOrEmpty(category.CategoryName))
            {
                return BadRequest("Category cannot be empty.");
            }
            // Simulate creating a category

            _context.Categories.Add(new Category
            {
                Id = Guid.NewGuid(),
                Name = category.CategoryName
            });

            _context.SaveChanges(); // Simulate saving to the database

            return CreatedAtAction(nameof(GetById), new { id = Guid.NewGuid() }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return BadRequest("Category cannot be empty.");
            }
            // Simulate updating a category
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            // Simulate deleting a category
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid category ID.");
            }
            return NoContent();
        }
    }
}
