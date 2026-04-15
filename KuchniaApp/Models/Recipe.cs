using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuchniaApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [MaxLength(200)]
        [Display(Name = "Tytuł")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        [Display(Name = "Krótki opis")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Instrukcje są wymagane.")]
        [Display(Name = "Sposób przygotowania")]
        public string Instructions { get; set; } = string.Empty;

        [Range(1, 600)]
        [Display(Name = "Czas przygotowania (min)")]
        public int PrepTimeMinutes { get; set; }

        [Range(0, 600)]
        [Display(Name = "Czas gotowania (min)")]
        public int CookTimeMinutes { get; set; }

        [Display(Name = "Data dodania")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Wybierz kategorię.")]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public int? AppUserId { get; set; }
        public virtual AppUser? AppUser { get; set; }

        public virtual ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    }
}