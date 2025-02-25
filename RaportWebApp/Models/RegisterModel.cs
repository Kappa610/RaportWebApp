using System.ComponentModel.DataAnnotations;

namespace RaportWebApp.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Imiê jest wymagane.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawid³owy format email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Has³o jest wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Potwierdzenie has³a jest wymagane.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Has³a nie s¹ identyczne.")]
        public string ConfirmPassword { get; set; }
    }
}