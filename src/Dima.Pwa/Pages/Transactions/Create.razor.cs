using Dima.Core.Handler;
using Dima.Core.Requests.Category;
using Dima.Core.Requests.Transaction;
using Dima.Pwa.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Pwa.Pages.Transactions;

public partial class CreatePage : ComponentBase
{
    [Inject]
    public ICategoryHandler CategoryHandler { get; set; }= null!;
    [Inject]
    public ITransactionHandler TransactionHandler { get; set; }= null!;
    [Inject]
    public NavigationManager Nav { get; set; } = null!;
    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;
    [Inject]
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    
    protected CreateTransactionRequest Request = new ();

    protected IEnumerable<GetCategoryCreateTransaction>? Categories = null;

    protected bool IsBusy;

    protected override async Task OnInitializedAsync()
    {
        var user = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var resultId = Guid.TryParse(user.User.Identity?.Name, out Guid idUser);
        if (!resultId)
        {
            Nav.NavigateTo("/");
            return;
        }
        
        Request.UserId = idUser;
        Categories = await CategoryHandler.GetAllCategoryToCreateTransaction(idUser,default);
        if(Categories is not null)
            Request.CategoryId = Categories.FirstOrDefault()?.CategoryId ?? Guid.Empty;
    }

    protected async Task OnValidSubmitAsync()
    {
        try
        {
            IsBusy = true;
            
            var result = await TransactionHandler.Create(Request);
            if (!result.IsSuccess)
            {
                SnackBar.Add(result.Error?.Message ?? "Invalid", Severity.Error);
                return;
            }
            Nav.NavigateTo("/transactions");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            IsBusy = false;
        }
    }
}