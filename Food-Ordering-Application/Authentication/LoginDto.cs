using System.ComponentModel.DataAnnotations;

namespace Food_Ordering_Application.Authentication
{
    public class LoginDto
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password can't be empty")]
        public string? Password { get; set; }
    }
}
