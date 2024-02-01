using System.ComponentModel.DataAnnotations;

namespace OAuth2Service.DTOs
{
    public class LoginRequest
    {
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
         ErrorMessage = "This must be an email address of Vietjet Air.")]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*[!@#$%^&*(),.?\"":{}|<>])(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$",
         ErrorMessage = "Password not allow.")]
        public string Password { get; set; }
    }
}
