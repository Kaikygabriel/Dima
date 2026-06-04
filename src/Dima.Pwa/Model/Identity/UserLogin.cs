using System.ComponentModel.DataAnnotations;

namespace Dima.Pwa.Model.Identity;

public class UserLogin
{
    [Required]
    [Length(minimumLength:4,50)]
    public string Password { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}