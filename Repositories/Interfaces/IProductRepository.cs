using DAL.Entities;
using Models;

namespace Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<ProductModel> GetProductWithCategories();
    }
}
