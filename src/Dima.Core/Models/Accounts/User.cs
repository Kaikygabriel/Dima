namespace Dima.Core.Models.Accounts;

public class User
{
    public string Email { get; set; } = "";
    public Dictionary<string, string> Claims { get; set; } = [];
}