using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "The category's description can't be empty!")]
        public string Name { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public List<RecipeCategory> JoinEntities { get; }
        public ApplicationUser User { get; set; }
    }
}