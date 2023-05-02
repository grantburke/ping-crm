using System.ComponentModel.DataAnnotations;

namespace Ping.Data.Models;

public class User : BaseEntity
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain a minimum of eight characters, at least one uppercase letter, one lowercase letter, one number and one special character.")]
    public string Password { get; set; }

    [Required]
    public Role Role { get; set; }
}

public enum Role
{
    Owner = 1,
    User = 2
}
