using Shopping.Api.Contracts.Domain;

namespace Shopping.Api.Contracts.Services;

public interface IProductService
{
    public List<Product> GetProducts();
}
