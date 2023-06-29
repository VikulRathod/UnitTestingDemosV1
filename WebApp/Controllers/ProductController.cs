using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Interfaces;
using System.Linq;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        IRepository<Category> _categoryRepo;
        IProductRepository _productRepo;

        public ProductController(IRepository<Category> categoryRepo,
            IProductRepository productRepo)
        {
            _categoryRepo = categoryRepo;
            _productRepo = productRepo;
        }

        public IActionResult Index()
        {
            var data = _productRepo.GetProductWithCategories();
            return View("Index", data);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryList = _categoryRepo.GetAll().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                _productRepo.Add(model);
                _productRepo.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _categoryRepo.GetAll().ToList();
            return View();
        }

        public IActionResult Edit(int Id)
        {
            Product model = _productRepo.Find(Id);
            ViewBag.CategoryList = _categoryRepo.GetAll().ToList();
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                _productRepo.Update(model);
                _productRepo.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _categoryRepo.GetAll().ToList();
            return View("Create", model);
        }

        public IActionResult Delete(int Id)
        {
            _productRepo.Delete(Id);
            _productRepo.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
