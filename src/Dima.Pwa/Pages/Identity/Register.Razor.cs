using Dima.Core.Handler;
using Dima.Core.Requests.Accounts;
using Dima.Pwa.Model.Identity;
using Dima.Pwa.Security;
using Dima.Pwa.Security.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Pwa.Pages.Identity;

public partial class RegisterPage : ComponentBase
{
    protected UserIdentity Model = new();
    protected string[] Errors = [];
    protected bool IsBusy ;
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    
    [Inject]
    public IUserHandler UserHandler { get;private set; }= null!;
    
    [Inject]
    public NavigationManager Nav { get;private set; } = null!;

    [Inject]
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; private set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var userAuth = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        if (userAuth.User.Identity is {IsAuthenticated : true})
            Nav.NavigateTo("/");
    }

    protected async Task TryOnValidSubmitAsync()
    {
        try
        {
            IsBusy = true;
            await Handler();
        }
        catch (Exception e)
        {
            ShowError(e.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task Handler()
    {
        var user = new RegisterRequest(Model.Email, Model.Password);

        var resultRegister = await UserHandler.RegisterAsync(user);
        if (!resultRegister.IsSuccess)
        {
            ShowError(resultRegister.Data ?? "Ocorreu um erro ao tentar registrar o usuário ! ");
            return;
        }

        var userLogin = new LoginRequest(Model.Email,Model.Password);
        var resultLogin = await UserHandler.LoginAsync(userLogin);
        if (!resultLogin.IsSuccess)
        {
            Nav.NavigateTo("/Loigin");
            return;
        }
        
        await AuthenticationStateProvider.GetAuthenticationStateAsync();
        AuthenticationStateProvider.NotifyAuthenticationStateChanged();
        
        Nav.NavigateTo("/");
    }
    
    private void ShowError(string message)
        => Snackbar.Add(message, Severity.Error, config =>
            {
                config.ShowCloseIcon = true;
                config.VisibleStateDuration = 4000; // 4 segundos visível
            });
    
}