using System.ComponentModel.DataAnnotations;

namespace TrueSecProject.Models
{
    public class LoginAttempt
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}