using System.Net.Http.Json;
using System.Security.Claims;
using Dima.Core.Models.Accounts;
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
        await GetAuthenticationStateAsync();
        
        return _isAuthentication;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var claimsPrincipal = new ClaimsPrincipal();
        _isAuthentication = false;

        var user = await GetUserAsync();
        if (user is null)
        {
            return new AuthenticationState(claimsPrincipal);
        }

        var claimsOfUser = await GetClaimsFromUser(user);
        var id = new ClaimsIdentity(claimsOfUser, nameof(CookieAuthenticationProvider));
        Console.WriteLine(id.IsAuthenticated  + " od ");
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
            
            var response = await _httpClient.GetAsync("v1/Identity/manage/id");
            
            response.EnsureSuccessStatusCode();
            
            var responseId = await response.Content.ReadAsStringAsync();
            
            var arr = responseId.ToCharArray().ToList();
            arr.Remove(arr.First());
            arr.Remove(arr.Last());
            Console.WriteLine(new string(arr.ToArray()));
            
            var idConvert = Guid.Parse(new string(arr.ToArray()));
            
            user.Id = idConvert;
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine("EXCEÇÂO "+e.Message);
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
        
        var roleClaims = await GetRoleClaim();
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
        try
        {
            var roleClaims = await _httpClient.GetFromJsonAsync<RoleClaim[]>("/v1/Identity/Roles",CancellationToken.None);
            return roleClaims;
        }
        catch (Exception)
        {
            return null;
        }
    }
}