using Dima.Core.Handler;
using Dima.Core.Response;
using Dima.Pwa.Security;
using Microsoft.AspNetCore.Components;

namespace Dima.Pwa.Components.Reports;

public partial class IncomesByCategoryChartPage : ComponentBase
{
    protected decimal[]? Data ;
    protected string[]? Labels ;
    protected Error? Error;
        
    [Inject]
    public ICookieAuthenticationStateProvider CookieAuthenticationStateProvider { get; set; } = null!;
    
    [Inject]
    public IReportHandler ReportHandler { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var claims = await CookieAuthenticationStateProvider.GetAuthenticationStateAsync();
        var userId = Guid.Parse(claims.User.Identity?.Name ?? Guid.Empty.ToString());
        var result = await ReportHandler.GetIncomeByCategoryAsync(userId);
        if(!result.IsSuccess)
        {
            Error = result.Error;
            return;
        }

        Data = result.Data!.Select(x => x.Incomes).ToArray();
        Labels = result.Data!.Select(x => x.Category + " " + x.Year).ToArray();
    }
}