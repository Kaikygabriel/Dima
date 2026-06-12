using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Pwa.Security;

public interface ICookieAuthenticationStateProvider
{
    Task<bool> CheckAuthenticationAsync();
    Task<AuthenticationState> GetAuthenticationStateAsync();
    void NotifyAuthenticationStateChanged();
}