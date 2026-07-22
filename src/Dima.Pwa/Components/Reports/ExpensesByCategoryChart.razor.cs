using Dima.Core.Handler;
using Dima.Core.Response;
using Dima.Pwa.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Pwa.Components.Reports;

public partial class ExpensesByCategoryChartComponent : ComponentBase
{
    [Inject]
    public IReportHandler ReportHandler { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public ICookieAuthenticationStateProvider CookieAuthenticationStateProvider { get; set; } = null!;

    protected Error? Error;
    protected decimal[]? Data ;
    protected string[]? Labels ;
    
    protected override async Task OnInitializedAsync()
    {
        var user = await CookieAuthenticationStateProvider.GetAuthenticationStateAsync();
        var id = Guid.Parse(user.User.Identity?.Name ?? Guid.Empty.ToString());
        
        var result = await ReportHandler.GetExpensesByCategoryAsync(id);
        if (!result.IsSuccess)
        {
            Error = result.Error;
            return; 
        }

        var expenses = result.Data;
        Data = expenses!.Select(x => Math.Abs(x.Expenses)).ToArray();
        Labels = expenses!.Select(x => $"{x.Category} {x.Expenses:C}").ToArray();
    }
}