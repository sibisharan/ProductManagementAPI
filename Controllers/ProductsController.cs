using Microsoft.AspNetCore.Mvc;
using ProductsManagementApi.Models;
using ProductsManagementApi.Services;

namespace ProductsManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductsController()
        {
            _service = new ProductService();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _service.GetAllProducts();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var products = _service.GetAllProducts();
            product.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
            _service.SaveProducts(products);
            return Created("", product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            var products = _service.GetAllProducts();
            var existing = products.FirstOrDefault(p => p.Id == id);
            if (existing == null) return NotFound();

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Quantity = product.Quantity;

            _service.SaveProducts(products);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var products = _service.GetAllProducts();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            products.Remove(product);
            _service.SaveProducts(products);
            return NoContent();
        }
    }
}
