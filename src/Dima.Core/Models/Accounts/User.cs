namespace Dima.Core.Models.Accounts;

public class User
{
    public Guid Id { get; set; }
    
    public string Email { get; set; } = "";
    public bool IsEmailConfirmed { get; set; }
    
    public Dictionary<string, string> Claims { get; set; } = [];
}