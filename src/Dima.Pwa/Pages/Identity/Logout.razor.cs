using Dima.Core.Handler;
using Dima.Pwa.Security;
using Dima.Pwa.Security.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Dima.Pwa.Pages.Identity;

public partial class LogoutPage : ComponentBase
{
    [Inject]
    public NavigationManager Nav { get;private set; } = null!;

    [Inject]
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; private set; } = null!;

    [Inject]
    public IUserHandler UserHandler { get;private set; }= null!;
    
    protected override async Task OnInitializedAsync()
    {
        if (!await AuthenticationStateProvider.CheckAuthenticationAsync())
            Nav.NavigateTo("/Login");

        await UserHandler.LogoutAsync();
        
        await AuthenticationStateProvider.GetAuthenticationStateAsync();
        AuthenticationStateProvider.NotifyAuthenticationStateChanged();
        Nav.NavigateTo("/Login");
    }
}