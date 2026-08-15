using System.Net;
using System.Net.Http.Json; 
using Dima.Core.Handler;
using Dima.Core.Models.Reports;
using Dima.Core.Response;
using Dima.Pwa.Configurations;

namespace Dima.Pwa.Handlers;

public class ReportsHandler : IReportHandler
{
    private readonly HttpClient _httpClient;

    public ReportsHandler(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient(Configuration.HttpClientName);
    }

    public async Task<Response<List<IncomesByCategory>>> GetIncomeByCategoryAsync(Guid userId)
    {
        var endPoint = $"/v1/Reports/category/incomes/{userId}";
        var response = await _httpClient.GetAsync(endPoint);
        if (response.StatusCode == HttpStatusCode.InternalServerError)
            return new Error("Error in Server","Error in Server");
        
        return await response.Content.ReadFromJsonAsync<Response<List<IncomesByCategory>>>()
               ?? new Error("Invalid Response","Invalid response");
    }

    public async Task<Response<List<ExpensesByCategory>>> GetExpensesByCategoryAsync(Guid userId)
    {
        var endPoint = $"/v1/Reports/category/expenses/{userId}";
        var response = await _httpClient.GetAsync(endPoint);
        if (response.StatusCode == HttpStatusCode.InternalServerError)
            return new Error("Error in Server","Error in Server");
        
        return await response.Content.ReadFromJsonAsync<Response<List<ExpensesByCategory>>>()
               ?? new Error("Invalid Response","Invalid response");
    }

    public async Task<Response<FinanceSummary>> GetFinanceSummaryAsync(Guid userId)
    {
        var endPoint = $"/v1/Reports/category/finance/{userId}";
        var response = await _httpClient.GetAsync(endPoint);
        if (response.StatusCode == HttpStatusCode.InternalServerError)
            return new Error("Error in Server","Error in Server");
        
        return await response.Content.ReadFromJsonAsync<Response<FinanceSummary>>()
               ?? new Error("Invalid Response","Invalid response");
    }

    public async Task<Response<List<IncomeAndExpenses>>> GetIncomeAndExpensesAsync(Guid userId, int year)
    {
        var endPoint = $"/v1/Reports/category/incomes/expenses/{userId}?year={year}";
        var response = await _httpClient.GetAsync(endPoint);
        if (response.StatusCode == HttpStatusCode.InternalServerError)
            return new Error("Error in Server","Error in Server");
        
        return await response.Content.ReadFromJsonAsync<Response<List<IncomeAndExpenses>>>()
               ?? new Error("Invalid Response","Invalid response");
    }
}