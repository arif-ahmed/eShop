using eShop.Data.Contracts;
using eShop.Web.Models.ViewModels;
using eShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using eShop.Models.Catalog;

namespace eShop.Web.Controllers.Apis;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IRepository<Product> _repository;

    public ProductsController(IRepository<Product> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }



    [HttpGet]
    public IActionResult Get()
    {
        var products = new List<string>
        {
            "Product 1",
            "Product 2",
            "Product 3"
        };

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetProductById(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl
        };
    }

    public async Task<IActionResult> Post(CreateProductDTO createProductDTO) 
    {
        if (createProductDTO == null)
        {
            return BadRequest("Product data is required.");
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = createProductDTO.Name,
            Description = createProductDTO.Description,
            Price = createProductDTO.Price,
            ImageUrl = createProductDTO.ImageUrl,
            CategoryId = createProductDTO.CategoryId
        };

        await _repository.Add(product);

        return StatusCode(501, "Not Implemented");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDTO updateProductDTO)
    {
        if (updateProductDTO == null)
        {
            return BadRequest("Product data is required.");
        }
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        product.Name = updateProductDTO.Name;
        product.Description = updateProductDTO.Description;
        product.Price = updateProductDTO.Price;
        product.ImageUrl = updateProductDTO.ImageUrl;
        await _repository.Update(product);
        return NoContent();
    }

    [HttpPost("search")]
    public async Task<ActionResult<PagedResult<ProductResponse>>> SearchProducts([FromBody] ProductSearchRequest request)
    {
        if (request.Page <= 0) request.Page = 1;
        if (request.PageSize <= 0 || request.PageSize > 100) request.PageSize = 10;

        string query = request.Query?.Trim() ?? string.Empty;


        var response = await _repository.Filter(
            p => p.Name == request.Query,
            "Name",
            "asc",
            (request.Page - 1) * request.PageSize,
            request.PageSize);       

        return new PagedResult<ProductResponse>
        {
            CurrentPage = request.Page,
            TotalPages = (int)Math.Ceiling(response.TotalCount / (double)request.PageSize),
            TotalItems = response.TotalCount,
            Items = response.Items.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = (p as Product)?.Name ?? "",
                Description = (p as Product)?.Description ?? "",
                Price = (p as Product)?.Price ?? 0m,
                ImageUrl = (p as Product)?.ImageUrl ?? ""
            }).ToList()
        };

    }
}
