using System.Net.Http.Json;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Transaction;
using Dima.Core.Response;

namespace Dima.Pwa.Handlers;

public class TransactionHandler : ITransactionHandler
{
    private readonly HttpClient _client;
    
    public TransactionHandler(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(Configuration.HttpClientName);
    }
    
    public async Task<PagedResponse<IEnumerable<Transaction>>> GetAllByCreateAt(GetTransactionsRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = $"/Transaction/v1/ByCreate/{request.UserId}/{request.Page}/{request.PageSize}";
        if (request.Start is not null && request.End is not null)
            endPoint += $"?Start={request.Start}&End={request.End}";
        
        var responseApi = await _client.GetAsync(endPoint, cancellationToken);

        var response = await responseApi.Content.ReadFromJsonAsync<PagedResponse<IEnumerable<Transaction>>>(cancellationToken);
        if(response is null && !responseApi.IsSuccessStatusCode)
            return new Error("Invalid", "Invalid");
        
        if(!response!.IsSuccess)
            return response.Error ??new Error("Invalid", "Invalid");
        
        return response;
    }

    public async Task<PagedResponse<IEnumerable<Transaction>>> GetAllByPaidOrReceivedAt(GetTransactionsRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = $"/Transaction/v1/ByPaid/{request.UserId}/{request.Page}/{request.PageSize}";
        if (request.Start is not null && request.End is not null)
            endPoint += $"?Start={request.Start}&End={request.End}";
        
        var responseApi = await _client.GetAsync(endPoint, cancellationToken);

        var response = await responseApi.Content.ReadFromJsonAsync<PagedResponse<IEnumerable<Transaction>>>(cancellationToken);
        if(response is null && !responseApi.IsSuccessStatusCode)
            return new Error("Invalid", "Invalid");
        
        if(!response!.IsSuccess)
            return response.Error ??new Error("Invalid", "Invalid");
        
        return response;
    }

    public async Task<Response<Transaction>> GetById(GetTransactionsByIdRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = $"/Transaction/v1/{request.Id}/{request.UserId}";
        var responseApi = await _client.GetAsync(endPoint, cancellationToken);

        var response = await responseApi.Content.ReadFromJsonAsync<Response<Transaction>>(cancellationToken);
        if(response is null && !responseApi.IsSuccessStatusCode)
            return new Error("Invalid", "Invalid");
        
        if(!response!.IsSuccess)
            return response.Error ??new Error("Invalid", "Invalid");
        
        return response.Data;
    }

    public async Task<Response<Transaction>> Create(CreateTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = "Transaction/v1";
        var responseApi = await _client.PostAsJsonAsync(endPoint, request, cancellationToken);

        var response = await responseApi.Content.ReadFromJsonAsync<Response<Transaction>>(cancellationToken);
        if (response is null && !responseApi.IsSuccessStatusCode)
            return new Error("Invalid", "Invalid");
        
        if (!response!.IsSuccess)
            return response.Error ?? new Error("Invalid", "Invalid");;

        return response.Data;
    }

    public async Task<Response<Transaction>> Update(UpdateTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = "Transaction/v1";
        var responseApi = await _client.PutAsJsonAsync(endPoint, request, cancellationToken);

        var response = await responseApi.Content.ReadFromJsonAsync<Response<Transaction>>(cancellationToken);
        if (response is null && !responseApi.IsSuccessStatusCode)
            return new Error("Invalid", "Invalid");
        
        if (!response!.IsSuccess)
            return response.Error ?? new Error("Invalid", "Invalid");;

        return response.Data;
    }

    public async Task<Response<Transaction>> Delete(DeleteTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = $"Transaction/v1/{request.Id}/{request.UserId}";
        var responseApi = await _client.DeleteAsync(endPoint, cancellationToken);

        var response = await responseApi.Content.ReadFromJsonAsync<Response<Transaction>>(cancellationToken);
        if (response is null && !responseApi.IsSuccessStatusCode)
            return new Error("Invalid", "Invalid");
        
        if (!response!.IsSuccess)
            return response.Error ?? new Error("Invalid", "Invalid");;

        return response.Data;
    }
}