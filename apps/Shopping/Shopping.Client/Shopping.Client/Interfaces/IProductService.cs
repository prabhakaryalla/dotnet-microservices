using Shopping.Client.Models;

namespace Shopping.Client.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetProducts();
}
