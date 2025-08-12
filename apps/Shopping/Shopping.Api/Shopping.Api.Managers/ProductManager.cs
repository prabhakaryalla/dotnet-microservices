using Shopping.Api.Contracts.Interfaces.Managers;
using Shopping.Api.Contracts.Interfaces.Services;
using Shopping.Api.Contracts.Models;

namespace Shopping.Api.Managers;

public class ProductManager : IProductManager
{
    private readonly IProductService _productService;

    public ProductManager(IProductService productService)
    {
        _productService = productService;       
    }

    public List<Product> GetProducts()
    {
        return _productService.GetProducts();
    }
}
