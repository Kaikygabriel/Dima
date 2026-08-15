using Dima.Core.Handler;
using Dima.Core.Requests.Category;
using Dima.Pwa.Security;
using Dima.Pwa.Security.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Pwa.Pages.Categories;

public partial class  EditPage : ComponentBase
{
    [Parameter]
    public Guid Id { get; set; }
    
    [Inject]
    public ISnackbar Snackbar { get; set; }= null!;
    
    [Inject]
    public NavigationManager Nav { get; set; }= null!;

    [Inject] 
    public ICategoryHandler CategoryHandler { get; set; } = null!;
    
    [Inject]
    public ICookieAuthenticationStateProvider CookieAuthenticationStateProvider { get; set; } = null!;

    protected bool IsBusy;
    protected UpdateCategoryRequest? Category = null;
    private Guid _userId;
    
    protected override async Task OnInitializedAsync()
    {

        var user = await CookieAuthenticationStateProvider.GetAuthenticationStateAsync();
        var idTryParse = Guid.TryParse(user.User.Identity?.Name,out Guid idUser);
        if (!idTryParse)
        {
            Nav.NavigateTo("/Login");
            return;
        }

        _userId = idUser;
        var response = await CategoryHandler.GetById(new GetCategoryByIdRequest(Id) { UserId =  _userId});
        
        if (!response.IsSuccess || response.Data is null)
        {
            Nav.NavigateTo("/categories");
            return;
        }

        Category = new UpdateCategoryRequest()
        {
            Id = Id,
            Title = response.Data!.Title,
            Description = response.Data!.Description ?? "",
            UserId = _userId
        };
    }

    protected async Task OnValidSubmitAsync()
    {
        try
        {
            IsBusy = true;
            var response =
                await CategoryHandler.Update(Category!);
            if (!response.IsSuccess)
            {
                Snackbar.Add(response.Error?.Message ?? "Invalid", Severity.Error);
                return;
            }

            Nav.NavigateTo("/categories");
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
}