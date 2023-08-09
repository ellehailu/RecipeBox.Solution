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
    public class RecipesController : Controller
    {
        private readonly RecipeBoxContext _db;
        public RecipesController(RecipeBoxContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Recipes.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Recipe recipe)
        {
            _db.Recipes.Add(recipe);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddCategory(int id)
        {
            Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipes => recipes.RecipeId == id);
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            return View(thisRecipe);
        }
        [HttpPost]
        public ActionResult AddCategory(Recipe recipe, int categoryId)
        {
            #nullable enable
                RecipeCategory? joinEntity = _db.RecipeCategories.FirstOrDefault(join => (join.CategoryId == categoryId && join.RecipeId == recipe.RecipeId));
            #nullable disable
            if (joinEntity == null && categoryId != 0)
            {
                _db.RecipeCategories.Add(new RecipeCategory() { CategoryId = categoryId, RecipeId = recipe.RecipeId });
                _db.SaveChanges();
            }
        return RedirectToAction("Details", new { id = recipe.RecipeId });
        }
        public ActionResult Details(int id)
        {
            Recipe thisRecipe = _db.Recipes
                .Include(recipe => recipe.JoinEntities)
                .ThenInclude(join => join.Category)
                .FirstOrDefault(recipe => recipe.RecipeId == id);
            return View(thisRecipe);
        }
    }
}