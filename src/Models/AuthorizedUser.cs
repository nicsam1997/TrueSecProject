using System.ComponentModel.DataAnnotations;

namespace TrueSecProject.Models;

public class AuthorizedUser
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public string Role { get; set; } = null!;
    
    public AuthorizedUserEntity toEntity()
    {
        return new AuthorizedUserEntity
        {
            Username = this.Username,
            Password = this.Password,
            Role = this.Role
        };
    }
}