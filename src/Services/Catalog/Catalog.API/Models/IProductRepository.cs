using Core.APP.Data;

namespace Catalog.API.Models
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(Guid id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
    }
}
