using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace RecipeBox.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Description { get; set; }
        public List<RecipeCategory> JoinEntities { get; }
        public ApplicationUser User { get; set; }
    }
}