using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace RecipeBox.Controllers
{
    [Authorize]
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
        public ActionResult AddRecipe(int id)
        {
            Category thisCategory = _db.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Description");
            return View(thisCategory);
        }

        [HttpPost]
        public ActionResult AddRecipe(Category category, int categoryId)
        {
#nullable enable
            RecipeCategory? joinEntity = _db.RecipeCategories.FirstOrDefault(join => (join.RecipeId == categoryId && join.CategoryId == category.CategoryId));
#nullable disable
            if (joinEntity == null && categoryId != 0)
            {
                _db.RecipeCategories.Add(new RecipeCategory() { RecipeId = categoryId, CategoryId = category.CategoryId });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = category.CategoryId });
        }
    }
}