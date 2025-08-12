using Shopping.Api.Contracts.Models;

namespace Shopping.Api.Contracts.Interfaces.Managers;

public interface IProductManager
{
    public List<Product> GetProducts();
}
