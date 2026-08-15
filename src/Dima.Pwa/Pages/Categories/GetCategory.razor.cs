using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Category;
using Dima.Core.Response;
using Dima.Pwa.Security;
using Dima.Pwa.Security.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Dima.Pwa.Pages.Categories;

public partial class GetCategoryPage : ComponentBase
{
    [Parameter]
    public Guid Id { get; set; }

    [Inject]
    public ICategoryHandler CategoryHandler { get; set; } = null!;
    
    [Inject]
    public NavigationManager Nav { get; set; } = null!;
    
    [Inject]
    public ICookieAuthenticationStateProvider CookieAuthenticationStateProvider { get; set; } = null!;

    protected Response<Category>? Category { get; private set; } = null;

    protected override async Task OnInitializedAsync()
    {
        if (!await CookieAuthenticationStateProvider.CheckAuthenticationAsync())
        {
            Nav.NavigateTo("/Login");
            return;
        }
        var user = await CookieAuthenticationStateProvider.GetAuthenticationStateAsync();
        var idTryParse = Guid.TryParse(user.User.Identity?.Name,out Guid idUser);
        if (!idTryParse)
        {
            Nav.NavigateTo("/Login");
            return;
        }

        Category = await CategoryHandler.GetById(new GetCategoryByIdRequest(Id){UserId = idUser});
    }
}