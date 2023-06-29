using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interfaces;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {
        // AppDbContext _db;
        IRepository<Category> _catRepo;
        public CategoryController(IRepository<Category> catRepo)
        {
            _catRepo = catRepo;
        }

        public IActionResult Index()
        {
            //var categories =
            //    _db.Categories.Select(c => 
            //    new CategoryModel() { Id = c.Id, Name = c.Name}).ToList();

            var categories = _catRepo.GetAll().Select(c =>
            new CategoryModel() { Id = c.Id, Name = c.Name }).ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                Category cat = new Category() { Name = model.Name};
                _catRepo.Add(cat);
                _catRepo.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Create");
        }
    }
}
