using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Contracts.Domain;
using Shopping.Api.Contracts.Interfaces.Managers;
using Shopping.Api.Contracts.Models;
using Shopping.Api.Controllers.Attributes;
using System.Net;

namespace Shopping.Api.Controllers.Controllers.v1;

// <summary>
/// This api provides functionality of shopping.
/// </summary>
[ApiVersion("1")]
[Route("v{version:apiVersion}/product")]
public class ProductsController : ApiBaseController
{
    private readonly IProductManager _productManager;

    public ProductsController(IProductManager productManager)
    {
        _productManager = productManager;
    }
    

    /// <summary>
    ///  Gets List of Products
    /// </summary>
    /// <returns>
    /// <response code="200">Successful</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not found</response>
    /// <response code="500">Internal server error</response>
    /// </returns>
    [HttpGet]
    [ModelValidation]
    [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Product>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StatusResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(StatusResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(StatusResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(StatusResponse))]

    public ActionResult GetProducts()
    {
        try
        {
            var products = _productManager.GetProducts();
            return Ok(products);
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, ex.Message);
            return InternalServerError(ex);
        }
    }
}
