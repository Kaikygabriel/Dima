using Dima.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Dima.Api.Models;

public class User : IdentityUser<Guid>
{
    public List<IdentityRole<Guid>>? Roles { get; set; }
    public List<Voucher> VouchersUsed { get; set; } = [];
}