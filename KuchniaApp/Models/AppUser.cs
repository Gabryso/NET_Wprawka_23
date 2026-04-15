using System.ComponentModel.DataAnnotations;

namespace KuchniaApp.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Login")]
        public string Login { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Hash hasła")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Display(Name = "Imię")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Recipe>? Recipes { get; set; }
    }
}