using Dima.Core.Commum.Extensions;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Transaction;
using Dima.Core.Response;
using Dima.Pwa.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Pwa.Pages.Transactions;

public partial class ListPage : ComponentBase
{
    private Guid _userId = Guid.Empty;
    protected string TermSearch = string.Empty;

    protected DateTime? Start = DateTime.Now.GetFirstDayOfMonth();
    protected DateTime? End = DateTime.Now.GetLastDayOfMonth();
    
    protected List<Transaction>? Transactions;

    [Inject]
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    
    [Inject]
    public ITransactionHandler TransactionHandler { get; set; } = null!;

    
    protected override async Task OnInitializedAsync()
    {
        var user = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var userId = Guid.Parse(user.User!.Identity!.Name!);
        
        _userId = userId;
        
        await GetTransactions();
    }
    protected async Task GetTransactions()
    {
        try
        {
            Console.WriteLine("ENTROU");
            if (Start > End)
            {
                Start = DateTime.Now.GetFirstDayOfMonth();
                End = DateTime.Now.GetLastDayOfMonth();
            }
            Console.WriteLine("ENTROU 2");
            var result =await TransactionHandler.GetAllByCreateAt(new GetTransactionsRequest(Start, End)
            {
                UserId = _userId
            });
            Console.WriteLine("ENTROU 3 ");
            if (!result.IsSuccess)
            {
                Snackbar.Add(result.Error?.Message ?? "Error", Severity.Error);
                Snackbar.Add(result.Error?.Title ?? "Error", Severity.Error);
                return;
            }
            Console.WriteLine("ENTROU 4");
            Transactions = result.Data?.ToList();
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message , Severity.Error);
        }
    }
    
    public Func<Transaction, bool> SearchFunc => x =>
    {
        if (string.IsNullOrEmpty(TermSearch))
            return true;

        if (x.Title.Equals(TermSearch, StringComparison.CurrentCultureIgnoreCase))
            return true;

        if (x.Title.Contains(TermSearch))
            return true;

        return false;
    };
    
    private async Task<Response<Transaction>> DeleteAsync(Guid idTransaction)
    {
        var result = await TransactionHandler.Delete(new DeleteTransactionRequest(idTransaction){UserId = _userId});
        
        Transactions?.RemoveAll(x => x.Id == idTransaction);
        
        return result;
    }
}