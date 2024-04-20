using System.ComponentModel.DataAnnotations;

namespace Food_Ordering_Application.Authentication
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "User Name Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password can't be empty")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Confirm your Password")]
        public string confirmPassword { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Phone No required")]
        public string PhoneNumber { get; set; }


    }
}
