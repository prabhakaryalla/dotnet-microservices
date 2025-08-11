using Shopping.Api.Contracts.Domain;

namespace Shopping.Api.Contracts.Managers;

public interface IProductManager
{
    public List<Product> GetProducts();
}
