using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuchniaApp.Models
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public virtual Recipe? Recipe { get; set; }

        public int IngredientId { get; set; }
        public virtual Ingredient? Ingredient { get; set; }

        [Required(ErrorMessage = "Podaj ilość.")]
        [MaxLength(50)]
        [Display(Name = "Ilość")]
        public string Quantity { get; set; } = string.Empty;
    }
}