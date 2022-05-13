using System.ComponentModel.DataAnnotations;

namespace RezhDumaASPCore_Backend.Model.Authentication
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
