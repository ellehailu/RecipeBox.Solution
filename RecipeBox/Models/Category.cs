using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "The category's description can't be empty!")]
        public string Name { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public List<RecipeCategories> JoinEntities { get; }
        public ApplicationUser User { get; set; }
    }
}