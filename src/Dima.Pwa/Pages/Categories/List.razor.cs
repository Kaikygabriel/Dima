using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Category;
using Dima.Pwa.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Pwa.Pages.Categories;

public partial class ListPage : ComponentBase
{
    private const int PageSize = 25;
    
    protected int TotalPage;
    protected int CurrentPage;

    protected List<Category> Categories = [];
    
    [Inject]
    private ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    [Inject] 
    public ICategoryHandler CategoryHandler { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    protected string SearchTitle = "";
        
    protected override async Task OnInitializedAsync()
    {
        CurrentPage = 1;
        await GetCategories(CurrentPage);
    }

    protected async Task GetCategories(int page)
    {
        try
        {
            var user = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var userId = Guid.Parse(user.User!.Identity!.Name!);
            var request = new GetAllCategoryRequest()
            {
                Page = page,
                PageSize = PageSize,
                UserId = userId
            };
            var result = await CategoryHandler.GetAll(request);
            if (!result.IsSuccess)
            {
                Snackbar.Add(result.Error?.Message ?? "Not Found ", Severity.Error);
                return;
            }

            TotalPage = result.PageTotal;
            CurrentPage = result.CurrentPage;
            
            Categories = result.Data!;
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message , Severity.Error);
        }
    }

    public Func<Category, bool> SearchFunc => x =>
    {
        if (string.IsNullOrEmpty(SearchTitle))
            return true;

        if (x.Title.Equals(SearchTitle, StringComparison.CurrentCultureIgnoreCase))
            return true;

        if (x.Title.Contains(SearchTitle))
            return true;

        return false;
    };
}