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
