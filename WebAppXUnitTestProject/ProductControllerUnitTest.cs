using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using Repositories.Interfaces;
using WebApp.Controllers;

namespace WebAppXUnitTestProject
{
    public class ProductControllerUnitTest
    {
        //Arrange
        ProductModel pm1;
        ProductModel pm2;
        ProductModel pm3;
        List<ProductModel> products;

        Category c1;
        Category c2;
        List<Category> categories;

        Product p1;

        Mock<IProductRepository> mockProductRepo;
        Mock<IRepository<Category>> mockCategoryRepo;

        ProductController ctrl;
        public ProductControllerUnitTest()
        {
            pm1 = new ProductModel { Id = 1, Name = "ASP.NET Core MVC Book", Description = "ASP.NET Core MVC Book", UnitPrice = 999, CategoryId = 1, Category = "Books" };
            pm2 = new ProductModel { Id = 2, Name = "Angular Book", Description = "Angular Book", UnitPrice = 999, CategoryId = 1, Category = "Books" };
            pm3 = new ProductModel { Id = 3, Name = "Azure Certification Training", Description = "Azure Certification Training", UnitPrice = 19999, CategoryId = 2, Category = "Training" };

            products = new List<ProductModel> { pm1, pm2 };

            c1 = new Category { Id = 1, Name = "Books" };
            c2 = new Category { Id = 2, Name = "Training" };

            categories = new List<Category> { c1, c2 };

            mockProductRepo = new Mock<IProductRepository>();
            mockCategoryRepo = new Mock<IRepository<Category>>();

            p1 = new Product { Id = 4, Name = "AWS Certification Training", Description = "Azure Certification Training", UnitPrice = 19999, CategoryId = 2 };

            //mock
            ctrl = new ProductController(mockCategoryRepo.Object, mockProductRepo.Object);

        }

        [Fact]
        public void TestIndexMethod()
        {
            //setup
            mockProductRepo.Setup(repo => repo.GetProductWithCategories()).Returns(products);

            //Action
            var result = ctrl.Index() as ViewResult;
            var model = result.Model as List<Models.ProductModel>;

            //Asset
            //positive
            Assert.Contains(pm1, model);
            Assert.Contains(pm2, model);

            //negative
            Assert.DoesNotContain(pm3, model);
        }

        [Fact]
        public void TestCreateGetMethod()
        {
            //setup
            mockCategoryRepo.Setup(repo => repo.GetAll()).Returns(categories);

            //action
            var result = ctrl.Create() as ViewResult;
            var viewData = result.ViewData["CategoryList"] as List<Category>;

            //Assert
            Assert.Contains(c1,viewData);
            Assert.Contains(c2, viewData);
        }

        [Fact]
        public void TestCreatePostMethodSuccess()
        {
            //setup
            //mockProductRepo.Setup(repo => repo.Add(p1)).Callback((Product model) =>
            //{
            //    ProductModel pm = new ProductModel { Id = model.Id, CategoryId = model.CategoryId, Name = model.Name, UnitPrice = model.UnitPrice, Description = model.Description };
            //    products.Add(pm);
            //});

            mockProductRepo.Setup(repo => repo.Add(p1));

            //action
            var result = ctrl.Create(p1) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public void TestCreatePostMethodFailed()
        {
            p1.Name = null;

            //setup
            mockCategoryRepo.Setup(repo => repo.GetAll()).Returns(categories);

            if (string.IsNullOrEmpty(p1.Name))
                ctrl.ModelState.AddModelError("Name", "Please Enter Name");

            //action
            var result = ctrl.Create(p1) as ViewResult;
            var viewData = result.ViewData["CategoryList"] as List<Category>;

            //Assert
            Assert.Contains(c1, viewData);
            Assert.Contains(c2, viewData);
        }

        [Fact]
        public void TestEditGetMethod()
        {
            int id = 4;
            //setup
            mockCategoryRepo.Setup(repo => repo.GetAll()).Returns(categories);
            mockProductRepo.Setup(repo => repo.Find(id)).Returns(p1);

            //action
            var result = ctrl.Edit(id) as ViewResult;
            var viewData = result.ViewData["CategoryList"] as List<Category>;
            var model = result.Model as Product;

            //Assert
            Assert.Contains(c1, viewData);
            Assert.Contains(c2, viewData);

            Assert.Equal("Create", result.ViewName);
            Assert.Equal(p1, model);
        }

        [Fact]
        public void TestEditPostMethodSuccess()
        {
            p1.Name = "React Book";
            //setup
            mockProductRepo.Setup(repo => repo.Update(p1));

            //action
            var result = ctrl.Edit(p1) as RedirectToActionResult;

            //Assert
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public void TestEditPostMethodFailed()
        {
            p1.Name = null;

            //setup
            mockCategoryRepo.Setup(repo => repo.GetAll()).Returns(categories);

            if (string.IsNullOrEmpty(p1.Name))
                ctrl.ModelState.AddModelError("Name", "Please Enter Name");

            //action
            var result = ctrl.Edit(p1) as ViewResult;
            var viewData = result.ViewData["CategoryList"] as List<Category>;

            //Assert
            Assert.Contains(c1, viewData);
            Assert.Contains(c2, viewData);

            Assert.Equal("Create", result.ViewName);
        }

        [Fact]
        public void TestDeleteMethod()
        {
            int id = 1;
            //setup
            mockProductRepo.Setup(repo => repo.Delete(id));

            //action
            var result = ctrl.Delete(id) as RedirectToActionResult;

            //assert
            Assert.Equal("Index", result.ActionName);
        }
    }
}