using Dima.Core.Handler;
using Dima.Core.Models.Reports;
using Dima.Pwa.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;
using MudBlazor;

namespace Dima.Pwa.Pages;

public partial class HomePage : ComponentBase
{
    protected FinanceSummary? FinanceSummary;
    
    [Inject]
    public ISnackbar Snackbar { get;private set; } = null!;
    
    [Inject]
    public IReportHandler ReportHandler { get;private set; } = null!;
    
    [Inject]
    public ICookieAuthenticationStateProvider CookieAuthenticationStateProvider { get;private set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var user = await CookieAuthenticationStateProvider.GetAuthenticationStateAsync();
        var id = Guid.Parse(user.User.Identity?.Name ?? Guid.Empty.ToString());
        var result = await ReportHandler.GetFinanceSummaryAsync(id);
        if (!result.IsSuccess)
        {
            Snackbar.Add(result.Error?.Title ?? "Error", Severity.Error);
            Snackbar.Add(result.Error?.Message??"Error", Severity.Error);
            return;
        }

        FinanceSummary = result.Data;
    }
}