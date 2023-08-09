using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<RecipeCategory> JoinEntities { get; }
        // public ApplicationUser User { get; set; }
    }
}