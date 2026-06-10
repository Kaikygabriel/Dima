using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Accounts;

public class RegisterRequest
{
    [Required]
    [EmailAddress] 
    public string Email { get; set; } = null!;
    [Required]
    [Length(minimumLength:4,maximumLength:70)]
    public string Password { get; set; }= null!;
}