using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuchniaApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa kategorii jest wymagana.")]
        [MaxLength(100)]
        [Display(Name = "Nazwa kategorii")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300)]
        [Display(Name = "Opis")]
        public string? Description { get; set; }

        public virtual ICollection<Recipe>? Recipes { get; set; }
    }
}