using Dima.Core.Handler;
using Dima.Core.Models.Reports;
using Dima.Pwa.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;
using MudBlazor;

namespace Dima.Pwa.Pages;

public partial class HomePage : ComponentBase
{
    protected bool IsVisible = true;
    
    protected FinanceSummary? FinanceSummary;
    
    protected string? Incomes;
    protected string? Expenses;
    protected string? Total;
    
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
        Incomes = FinanceSummary?.Income.ToString("C");
        Expenses = FinanceSummary?.Expense.ToString("C");
        Total = FinanceSummary?.Total.ToString("C");
    }

    public void AlterVisibility()
    {
        if (IsVisible)
        {
            Incomes = string.Join(' ', Incomes?.ToCharArray().Select(x=>'*') ?? []);
            Expenses = string.Join(' ', Expenses?.ToCharArray().Select(x=>'*') ?? []);
            Total = string.Join(' ', Total?.ToCharArray().Select(x=>'*') ?? []);
            IsVisible = false;
            
            StateHasChanged();
            
            return;
        }
        
        Incomes = FinanceSummary?.Income.ToString("C");
        Expenses = FinanceSummary?.Expense.ToString("C");
        Total = FinanceSummary?.Total.ToString("C");
        IsVisible = true;
        StateHasChanged();
    }
}