using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using Repositories.Interfaces;
using WebApp.Controllers;

namespace WebAppMSTestProjet
{
    [TestClass]
    public class CategoryControllerUnitTests
    {
        [TestMethod]
        public void IndexTestMethod()
        {
            // Arrange
            // Mock<AppDbContext> db = new Mock<AppDbContext>();
            Mock<IRepository<Category>> catRepo =
                new Mock<IRepository<Category>>();
            CategoryController catCtrl = new CategoryController(catRepo.Object);

            Category c1 = new Category() { Id = 1, Name = "Book" };
            Category c2 = new Category() { Id = 2, Name = "Course" };
            List<Category> categories = new List<Category>() { c1, c2 };


            CategoryModel cm1 = new CategoryModel() { Id = 1, Name = "Book" };
            CategoryModel cm2 = new CategoryModel() { Id = 2, Name = "Course" };
            List<CategoryModel> categoryModels =
                new List<CategoryModel>() { cm1, cm2 };

            //db.Setup(d => d.Categories).Returns<Category>(c =>
            //new DbSet<Category>() { Id = c.Id, Name = c.Name });

            catRepo.Setup(repo => repo.GetAll()).Returns(categories);

            // Action
            var result = catCtrl.Index() as ViewResult;

            // Assert.AreEqual("Index", result.ViewName);

            var model = result.Model as List<CategoryModel>;

            //Assert.AreEqual(model[0], cm1);
            //Assert.AreEqual(model[2], cm2);

            //CollectionAssert.Contains(model, cm1);
            //CollectionAssert.Contains(model, cm2);

            Assert.AreEqual(model[0].Id, cm1.Id);
            Assert.AreEqual(model[0].Name, cm1.Name);
        }

        [TestMethod]
        public void CreateGetTestMethod()
        {
            Mock<IRepository<Category>> catRepo =
                new Mock<IRepository<Category>>();
            CategoryController catCtrl = new CategoryController(catRepo.Object);

            var result = catCtrl.Create() as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void CreatePostSuccessTestMethod()
        {
            Mock<IRepository<Category>> catRepo =
                new Mock<IRepository<Category>>();
            CategoryController catCtrl = new CategoryController(catRepo.Object);

            Category c1 = new Category() { Id = 1, Name = "Book" };
            CategoryModel cm1 = new CategoryModel() { Id = 1, Name = "Book" };

            catRepo.Setup(repo => repo.Add(c1));

            var result = catCtrl.Create(cm1) as RedirectToActionResult;

            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void CreatePostFailedTestMethod()
        {
            Mock<IRepository<Category>> catRepo =
                new Mock<IRepository<Category>>();
            CategoryController catCtrl = new CategoryController(catRepo.Object);

            CategoryModel cm1 = new CategoryModel() { Id = 1, Name = "Book" };

            catCtrl.ModelState.AddModelError("Name", "Name is required");

            var result = catCtrl.Create(cm1) as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }
    }
}
