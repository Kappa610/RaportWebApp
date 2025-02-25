using System.ComponentModel.DataAnnotations;

namespace RaportWebApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawid³owy format email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Has³o jest wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}