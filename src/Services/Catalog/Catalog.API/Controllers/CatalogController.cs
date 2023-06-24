using Catalog.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    public class CatalogController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("catalog/products")]
        public async Task<IEnumerable<Product>> Index() 
        { 
            return await _productRepository.GetAll();
        }

        [HttpGet("catalog/products/{id}")]
        public async Task<Product> ProductDetails(Guid id) //Entidade simples, não precisa de DTO
        {
            return await _productRepository.GetById(id);
        }
    }
}
