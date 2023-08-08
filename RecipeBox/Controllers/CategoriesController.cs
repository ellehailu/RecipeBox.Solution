using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeBox.Controllers
{
    public class CategoriesController : Controller  
    {
        private readonly RecipeBoxContext _db;
        public CategoriesController(RecipeBoxContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            return View(_db.Categories.ToList());
        }
        public ActionResult Details(int id)
        {
            Category thisCategory = _db.Categories
                .Include(category => category.JoinEntities)
                .ThenInclude(join => join.Recipe)
                .FirstOrDefault(category => category.CategoryId == id);
            return View(thisCategory);
        }
        public ActionResult Details(int id)
        {
            Category thisCategory = _db.Categories
                .Include(category => category.JoinEntities)
                .ThenInclude(join => join.CategoryId == id);
                .FirstOrDefault(category => category.CategoryId == id);
            return View(thisCategory);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}