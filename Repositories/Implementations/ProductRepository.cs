using DAL;
using DAL.Entities;
using Models;
using Repositories.Interfaces;
using System.Security.Cryptography;

namespace Repositories.Implementations
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext db) : base(db)
        {

        }
        public IEnumerable<ProductModel> GetProductWithCategories()
        {
            //var data = (from prd in _db.Products
            //            join cat in _db.Categories
            //            on prd.CategoryId equals cat.Id
            //            select new
            //            {
            //                prd.Id,
            //                prd.Name,
            //                prd.Description,
            //                prd.UnitPrice,
            //                Category = cat.Name
            //            }).ToList();

            //IList<ProductModel> model = new List<ProductModel>();
            //foreach (var item in data)
            //{
            //    ProductModel prd = new ProductModel
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //        Description = item.Description,
            //        UnitPrice = item.UnitPrice,
            //        Category = item.Category
            //    };
            //    model.Add(prd);
            //}

            List<ProductModel> model = new List<ProductModel>();

            model = (from p in _db.Products
                    join c in _db.Categories
                    on p.CategoryId equals c.Id
                    select new ProductModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        UnitPrice = p.UnitPrice,
                        CategoryId = p.CategoryId,
                        Category = c.Name
                    }).ToList();

            return model;
        }
    }
}
