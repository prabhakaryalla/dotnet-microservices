using Shopping.Api.Contracts.Domain;
using Shopping.Api.Contracts.Managers;
using Shopping.Api.Contracts.Services;

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
