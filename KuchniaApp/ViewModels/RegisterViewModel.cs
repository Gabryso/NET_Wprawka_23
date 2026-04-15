using System.ComponentModel.DataAnnotations;

namespace KuchniaApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Login jest wymagany.")]
        [MaxLength(100)]
        [Display(Name = "Login")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Imię jest wymagane.")]
        [MaxLength(100)]
        [Display(Name = "Imię")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [MaxLength(100)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres email.")]
        [MaxLength(200)]
        [Display(Name = "Adres email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [MinLength(6, ErrorMessage = "Hasło musi mieć co najmniej 6 znaków.")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Powtórz hasło.")]
        [Compare("Password", ErrorMessage = "Hasła nie są identyczne.")]
        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}