using System.ComponentModel.DataAnnotations;

namespace KuchniaApp.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa składnika jest wymagana.")]
        [MaxLength(100)]
        [Display(Name = "Nazwa składnika")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(30)]
        [Display(Name = "Jednostka")]
        public string? DefaultUnit { get; set; }

        public virtual ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    }
}