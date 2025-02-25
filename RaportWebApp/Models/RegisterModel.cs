using System.ComponentModel.DataAnnotations;

namespace RaportWebApp.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Imi� jest wymagane.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawid�owy format email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Has�o jest wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Potwierdzenie has�a jest wymagane.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Has�a nie s� identyczne.")]
        public string ConfirmPassword { get; set; }
    }
}