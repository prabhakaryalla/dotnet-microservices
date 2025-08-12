using Newtonsoft.Json;
using Shopping.Client.Interfaces;
using Shopping.Client.Models;
using System.Net.Http;

namespace Shopping.Client.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ShoppingApiClient");
        }

        public async Task<List<Product>> GetProducts()
        {
            var response = await _httpClient.GetAsync("v1/product");
            var content = await response.Content.ReadAsStringAsync();
            var productList = JsonConvert.DeserializeObject<List<Product>>(content);
            return productList;
        }
    }
}
