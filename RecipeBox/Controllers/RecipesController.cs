using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeBox.Controllers
{
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
    }
}