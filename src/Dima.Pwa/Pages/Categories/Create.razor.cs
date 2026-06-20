using System.Security.Claims;
using Dima.Core.Handler;
using Dima.Core.Requests.Category;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Pwa.Pages.Categories;

public class CreateCategoryPage : ComponentBase
{
    protected bool IsBusy;
    protected CreateCategoryRequest Model = new();
    
    [Inject]
    public NavigationManager NavigationManager { get;private set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get;private set; } = null!;
    [Inject]
    public ICategoryHandler CategoryHandler { get;private set; } = null!;


    public async Task OnValidSubmit()
    {
        try
        {
            IsBusy = true;

            await Handler();
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task Handler()
    {
        var result = await CategoryHandler.Create(Model);
        
        if (!result.IsSuccess)
        {
            Snackbar.Add(result.Error?.Message ?? "Error", Severity.Error);
            return;
        }
        NavigationManager.NavigateTo("/");
    }
}