using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Dima.Core.Models.Accounts;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Pwa.Security;

public class CookieAuthenticationProvider : AuthenticationStateProvider,ICookieAuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private bool _isAuthentication;
    
    public CookieAuthenticationProvider(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient(Configuration.HttpClientName);
        _isAuthentication = false;
    }

    public async Task<bool> CheckAuthenticationAsync()
    {
        await CheckAuthenticationAsync();
        return _isAuthentication;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var claimsPrincipal = new ClaimsPrincipal();
        _isAuthentication = false;

        var user = await GetUserAsync();
        if (user is null)
            return new AuthenticationState(claimsPrincipal);

        var claimsOfUser = await GetClaimsFromUser(user);
        var id = new ClaimsIdentity(claimsOfUser, nameof(CookieAuthenticationProvider));
        claimsPrincipal = new ClaimsPrincipal(id);

        _isAuthentication = true;

        return new AuthenticationState(claimsPrincipal);
    }

    public void NotifyAuthenticationStateChanged()
        =>  NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    private async Task<User?> GetUserAsync()
    {
        try
        {
            var user = await _httpClient.GetFromJsonAsync<User?>("v1/Identity/manage/info");
            if (user is null)
                return null;
            
            var idConvert = Guid.TryParse(await _httpClient.GetFromJsonAsync<string>("v1/Identity/manage/id"), out Guid id);
            if (!idConvert)
                return null;
        
            user.Id = id;
            return user;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    private async Task<List<Claim>> GetClaimsFromUser(User user)
    {
        List<Claim> claims = new ()
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };
        claims.AddRange(
            user.Claims
                .Where(x => x.Key != ClaimTypes.Name && x.Key != ClaimTypes.Email)
                .Select(x => new Claim(x.Key, x.Value)));
        
        RoleClaim[]? roleClaims = await GetRoleClaim();
        if(roleClaims is null)
            return claims;
        
        claims.AddRange(
            roleClaims
                .Where(x=>!string.IsNullOrEmpty(x.Type) && !string.IsNullOrEmpty(x.Value))
                    .Select(x=> new Claim(x.Type,x.Value,x.ValueType,x.Issuer,x.OriginalIssuer))
            );
        
        return claims;
    }

    private async Task<RoleClaim[]?> GetRoleClaim()
    {
        RoleClaim[]? roleClaims;
        try
        {
            roleClaims = await _httpClient.GetFromJsonAsync<RoleClaim[]>("/v1/Identity/Roles",default);
            return roleClaims;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}