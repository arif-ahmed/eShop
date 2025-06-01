using Microsoft.AspNetCore.Mvc;

namespace eShop.Web.Controllers.Apis;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
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
}
