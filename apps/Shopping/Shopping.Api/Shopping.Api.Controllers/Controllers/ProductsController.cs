using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Contracts.Domain;
using Shopping.Api.Contracts.Managers;

namespace Shopping.Api.Controllers.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductManager _productManager;

    public ProductsController(IProductManager productManager)
    {
        _productManager = productManager;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
    public ActionResult GetProducts()
    {
        var products = _productManager.GetProducts();
        return Ok(products);
    }
}
