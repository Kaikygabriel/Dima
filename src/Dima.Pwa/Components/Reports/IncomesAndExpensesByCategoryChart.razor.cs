using System.Globalization;
using Dima.Core.Handler;
using Dima.Core.Response;
using Dima.Pwa.Security;
using Dima.Pwa.Security.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Pwa.Components.Reports;

public partial class IncomesAndExpensesByCategoryChartComponent : ComponentBase
{
    
    protected readonly Dictionary<ChartType, string> MenuIcons = new()
    {
        { ChartType.Bar, Icons.Material.Filled.BarChart },
        { ChartType.StackedBar, Icons.Material.Filled.StackedBarChart },
        { ChartType.Line, Icons.Material.Filled.ShowChart },
        { ChartType.Pie, Icons.Material.Filled.PieChart },
        { ChartType.Donut, Icons.Material.Filled.DonutLarge },
        { ChartType.Rose, Icons.Material.Filled.DonutSmall },
        { ChartType.Radar, Icons.Material.Filled.Hub }
    };
    protected ChartType ChartType = ChartType.Bar;

    protected List<ChartSeries<double>>? Series;
    protected string[]? AxisLabels;

    protected Error? Error;
    
    [Inject]
    public IReportHandler ReportHandler { get; set; } = null!; 
    
    [Inject]
    public ICookieAuthenticationStateProvider CookieAuthenticationStateProvider { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var claimsPrincipal = await CookieAuthenticationStateProvider.GetAuthenticationStateAsync();
        var id = Guid.Parse(claimsPrincipal.User.Identity?.Name ?? Guid.Empty.ToString());
        var result = await ReportHandler.GetIncomeAndExpensesAsync(id,DateTime.Now.Year);
        if (!result.IsSuccess)
        {
            Error = result.Error;
            return;
        }
        
        Series = new List<ChartSeries<double>>()
        {
            new ChartSeries<double>()
                { Name = "Incomes", Data = result.Data?.OrderBy(x=>x.Month).Select(x => (double)x.Incomes).ToArray() ?? [] },
            new ChartSeries<double>()
                { Name = "Expenses", Data = result.Data?.OrderBy(x=>x.Month).Select(x => (double)Math.Abs(x.Expenses)).ToArray() ?? [] }
        };
        
        var months = result.Data?.OrderBy(x=>x.Month).Select(x=>x.Month) ?? [];
        AxisLabels = months.Select(m => CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat.GetMonthName(m))
            .ToArray();
    }
} 