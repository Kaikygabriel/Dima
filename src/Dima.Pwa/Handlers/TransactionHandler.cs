using System.Net.Http.Json;
using Dima.Core.Commum.Extensions;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Transaction;
using Dima.Core.Response;

namespace Dima.Pwa.Handlers;

public class TransactionHandler : ITransactionHandler
{
    private readonly HttpClient _client;
    private const string FormatDate = "yyyy-MM-dd";
    
    public TransactionHandler(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(Configuration.HttpClientName);
    }
    
    public async Task<PagedResponse<IEnumerable<Transaction>>> GetAllByCreateAt(GetTransactionsRequest request, CancellationToken cancellationToken = default)
    {
        var start = (request.Start ?? DateTime.UtcNow.GetFirstDayOfMonth()).ToString(FormatDate);
        var end = (request.End ?? DateTime.UtcNow.GetLastDayOfMonth()).ToString(FormatDate);
        
         var endPoint = $"/Transaction/v1/ByCreate/{request.UserId}?start={start}&end={end}";
        
        var responseApi = await _client.GetAsync(endPoint, cancellationToken);

        var response = await responseApi.Content.ReadFromJsonAsync<PagedResponse<IEnumerable<Transaction>>>(cancellationToken);
        if(response is null && !responseApi.IsSuccessStatusCode)
            return new Error("Invalid", "Invalid");
        
        return response ?? new Error("Invalid","Invalid");
    }

    public async Task<PagedResponse<IEnumerable<Transaction>>> GetAllByPaidOrReceivedAt(GetTransactionsRequest request, CancellationToken cancellationToken = default)
    {
        var start = (request.Start ?? DateTime.UtcNow.GetFirstDayOfMonth()).ToString(FormatDate);
        var end = (request.Start ?? DateTime.UtcNow.GetLastDayOfMonth()).ToString(FormatDate);
        
        var endPoint = $"/Transaction/v1/ByPaid/{request.UserId}/{request.Page}/{request.PageSize}?Start={start}&End={end}";
        
        var responseApi = await _client.GetAsync(endPoint, cancellationToken);

        var response = await responseApi.Content.ReadFromJsonAsync<PagedResponse<IEnumerable<Transaction>>>(cancellationToken);
        if(response is null && !responseApi.IsSuccessStatusCode)
            return new Error("Invalid", "Invalid");
        
        return response ?? new Error("Invalid","Invalid");
    }

    public async Task<Response<Transaction>> GetById(GetTransactionsByIdRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = $"/Transaction/v1/{request.Id}/{request.UserId}";
        var responseApi = await _client.GetAsync(endPoint, cancellationToken);

        var response = await responseApi.Content.ReadFromJsonAsync<Response<Transaction>>(cancellationToken);
        if(response is null && !responseApi.IsSuccessStatusCode)
            return new Error("Invalid", "Invalid");
        
        return response ?? new Error("Invalid","Invalid");
    }

    public async Task<Response<Transaction>> Create(CreateTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = "/Transaction/v1";
        var responseApi = await _client.PostAsJsonAsync(endPoint, request, cancellationToken);
        
        if (!responseApi.IsSuccessStatusCode)
            return new Error("Invalid", "Invalid");

        return Response<Transaction>.Success();
    }

    public async Task<Response<Transaction>> Update(UpdateTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = "Transaction/v1";
        var responseApi = await _client.PutAsJsonAsync(endPoint, request, cancellationToken);

        var response = await responseApi.Content.ReadFromJsonAsync<Response<Transaction>>(cancellationToken);
        if (response is null && !responseApi.IsSuccessStatusCode)
            return new Error("Invalid", "Invalid");

        return response ?? new Error("Invalid","Invalid");
    }

    public async Task<Response<Transaction>> Delete(DeleteTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = $"Transaction/v1/{request.Id}/{request.UserId}";
        var responseApi = await _client.DeleteAsync(endPoint, cancellationToken);

        var response = await responseApi.Content.ReadFromJsonAsync<Response<Transaction>>(cancellationToken);
        if (response is null && !responseApi.IsSuccessStatusCode)
            return new Error("Invalid", "Invalid");

        return response ?? new Error("Invalid","Invalid");
    }
}