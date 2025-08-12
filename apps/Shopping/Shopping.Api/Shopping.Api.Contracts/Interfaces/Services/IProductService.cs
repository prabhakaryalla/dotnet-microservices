using Shopping.Api.Contracts.Models;

namespace Shopping.Api.Contracts.Interfaces.Services;

public interface IProductService
{
    public List<Product> GetProducts();
}
