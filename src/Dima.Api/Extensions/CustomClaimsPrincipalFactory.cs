using System.Security.Claims;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Dima.Api.Extensions;


public class CustomClaimsPrincipalFactory
    : UserClaimsPrincipalFactory<User>
{
    public CustomClaimsPrincipalFactory(
        UserManager<User> userManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        // Remove a claim Name padrão
        var nameClaim = identity.FindFirst(ClaimTypes.Name);

        if (nameClaim != null)
            identity.RemoveClaim(nameClaim);

        // Adiciona o valor que você quiser
        identity.AddClaim(new Claim(
            ClaimTypes.Name,
            user.Id.ToString()
        ));

        return identity;
    }
}