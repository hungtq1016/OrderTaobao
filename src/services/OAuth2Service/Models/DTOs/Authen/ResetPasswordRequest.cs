using System.ComponentModel.DataAnnotations;

namespace OAuth2Service.DTOs
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Email { get; set; }

        [Required] 
        public string Password { get; set; }
    }
}
