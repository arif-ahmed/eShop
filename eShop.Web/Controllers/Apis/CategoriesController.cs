using Microsoft.AspNetCore.Mvc;

namespace eShop.Web.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController() { }

        [HttpGet]
        public IActionResult Get()
        {
            var categories = new List<string>
            {
                "Category 1",
                "Category 2",
                "Category 3"
            };

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
        public IActionResult Create([FromBody] string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return BadRequest("Category cannot be empty.");
            }
            // Simulate creating a category
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
