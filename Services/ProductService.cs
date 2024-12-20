using System.Text.Json;
using ProductsManagementApi.Models;

namespace ProductsManagementApi.Services
{
    public class ProductService
    {
        private readonly string _filePath = "data/products.json";

        public List<Product> GetAllProducts()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        public void SaveProducts(List<Product> products)
        {
            var json = JsonSerializer.Serialize(products);
            File.WriteAllText(_filePath, json);
        }
    }
}
