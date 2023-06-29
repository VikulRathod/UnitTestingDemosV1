using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Controllers;

namespace WebAppMSTestProjet
{
    [TestClass]
    public class ProductControllerUnitTestV1
    {
        #region Arrange

        Mock<IProductRepository> productRepo;
        Mock<IRepository<Category>> catRepo;
        ProductController prodCtrl;
        ProductModel pm1;
        ProductModel pm2;
        List<ProductModel> products;
        Category c1;
        Category c2;
        List<Category> categories;
        Product p1;

        #endregion Arrange

        public ProductControllerUnitTestV1()
        {
            productRepo =
                new Mock<IProductRepository>();
            catRepo =
               new Mock<IRepository<Category>>();
            prodCtrl =
                new ProductController(catRepo.Object, productRepo.Object);
            pm1 = new ProductModel()
            {
                Id = 1,
                Name = "SQL",
                Description = "SQL Book",
                UnitPrice = 100,
                CategoryId = 1,
                Category = "Book"
            };
            pm2 = new ProductModel()
            {
                Id = 2,
                Name = "C#",
                Description = "C# Course",
                UnitPrice = 200,
                CategoryId = 2,
                Category = "Course"
            };
            products = new List<ProductModel>() { pm1, pm2 };

            c1 = new Category() { Id = 1, Name = "Book" };
            c2 = new Category() { Id = 2, Name = "Course" };
            categories = new List<Category>() { c1, c2 };

            p1 = new Product()
            {
                Id = 3,
                Name = "MVC",
                UnitPrice = 200,
                Description = "MVC Training",
                CategoryId = 2
            };
        }

        [TestMethod]
        public void IndexTestMethod()
        {
            // Setup
            productRepo.Setup(repo =>
            repo.GetProductWithCategories()).Returns(products);

            // A : Action
            var result = prodCtrl.Index() as ViewResult;

            // S : Assert
            Assert.AreEqual("Index", result.ViewName);

            var model = result.Model as List<ProductModel>;
            CollectionAssert.Contains(model, pm1);
            CollectionAssert.Contains(model, pm2);
        }

        [TestMethod]
        public void CreateGetTestMethod()
        {
            // Setup
            catRepo.Setup(repo => repo.GetAll()).Returns(categories);

            var result = prodCtrl.Create() as ViewResult;

            var actualCategories =
                result.ViewData["CategoryList"] as List<Category>;

            CollectionAssert.Contains(actualCategories, c1);
            CollectionAssert.Contains(actualCategories, c2);
        }

        [TestMethod]
        public void CreatePostFailedTestMethod()
        {
            // Setup
            catRepo.Setup(repo => repo.GetAll()).Returns(categories);

            p1.Name = null;
            prodCtrl.ModelState.AddModelError("Name", "Name is required");

            var result = prodCtrl.Create(p1) as ViewResult;

            var actualCategories =
                result.ViewData["CategoryList"] as List<Category>;

            CollectionAssert.Contains(actualCategories, c1);
            CollectionAssert.Contains(actualCategories, c2);
        }

        [TestMethod]
        public void CreatePostSuccessTestMethod()
        {
            // Setup
            // catRepo.Setup(repo => repo.GetAll()).Returns(categories);

            ProductModel p3 = new ProductModel()
            {
                Id = p1.Id,
                Name = p1.Name,
                Description = p1.Description,
                UnitPrice = p1.UnitPrice,
                CategoryId = p1.CategoryId,
                Category = "Courses"
            };

            productRepo.Setup(repo =>
            repo.Add(p1)).Callback(() => products.Add(p3));

            var result = prodCtrl.Create(p1) as RedirectToActionResult;

            Assert.AreEqual("Index", result.ActionName);

            CollectionAssert.Contains(products, p3);
        }
    }
}
