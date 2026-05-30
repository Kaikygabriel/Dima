using Microsoft.AspNetCore.Identity;

namespace Dima.Api.Models;

public class User : IdentityUser<Guid>
{
    public List<IdentityRole<Guid>>? Roles { get; set; }
}