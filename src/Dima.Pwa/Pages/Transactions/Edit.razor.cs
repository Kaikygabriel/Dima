using Dima.Core.Handler;
using Dima.Core.Requests.Category;
using Dima.Core.Requests.Transaction;
using Dima.Core.Response;
using Dima.Pwa.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Pwa.Pages.Transactions;

public partial class EditPage : ComponentBase
{
    [Parameter]
    public Guid Id { get; set; }

    protected UpdateTransactionRequest Request = new ();

    protected Error? Error;
    protected IEnumerable<GetCategoryCreateTransaction>? _categoryCreateTransactions;
    protected bool IsBusy;
    
    private Guid _userId;

    [Inject]
    public ICategoryHandler CategoryHandler { get; set; } = null!;
    
    [Inject]
    public ICookieAuthenticationStateProvider StateProvider { get; set; } = null!;
    
    [Inject]
    public ITransactionHandler TransactionHandler { get; set; } = null!;
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    
    [Inject]
    public NavigationManager Nav { get; set; } = null!;

    
    protected override async Task OnInitializedAsync()
    {
        var userClaims = await StateProvider.GetAuthenticationStateAsync();
        var tryConvertResult = Guid.TryParse(userClaims.User.Identity!.Name, out Guid userId);
        
        var transaction = await TransactionHandler.GetById(new GetTransactionsByIdRequest(Id));
        if (!transaction.IsSuccess)
        {
            Error = transaction.Error;
            return;
        }
        
        _userId = userId;
        
        _categoryCreateTransactions = await CategoryHandler.GetAllCategoryToCreateTransaction(_userId,CancellationToken.None);
        if (_categoryCreateTransactions is null)
        {
            Error = new Error("Categories Not Found", "Not Found");
            return;
        }

        Request = transaction.Data!;
    }

    protected async Task OnValidSubmitAsync()
    {
        try
        {
            IsBusy = true;
            var result = await TransactionHandler.Update(Request);
            if (!result.IsSuccess)
            {
                Snackbar.Add(result.Error?.Title ?? "Error", Severity.Error);
                Snackbar.Add(result.Error?.Message ?? "Error", Severity.Error);
            }
            Nav.NavigateTo("/launch/history");
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