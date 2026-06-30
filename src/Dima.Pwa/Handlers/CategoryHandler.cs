using System.Net.Http.Json;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Category;
using Dima.Core.Response;

namespace Dima.Pwa.Handlers;

public class CategoryHandler : ICategoryHandler
{
    private readonly HttpClient _client;
    
    public CategoryHandler(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(Configuration.HttpClientName);
    }

    public async Task<IEnumerable<GetCategoryCreateTransaction>?> GetAllCategoryToCreateTransaction(Guid userId, CancellationToken cancellationToken)
    {
        var endPoint = $"Transaction/v1/All/create/transactions/{userId}";
        var responseRequest = await _client.GetAsync(endPoint,cancellationToken);
        var response = await responseRequest.Content.ReadFromJsonAsync<List<GetCategoryCreateTransaction>>(cancellationToken);
      
        if (!responseRequest.IsSuccessStatusCode && response is null)
            return null;
        
        return response;
    }

    public async Task<Response<Category>> GetById(GetCategoryByIdRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = $"/Categories/v1/{request.Id}/{request.UserId}";
        var responseRequest = await _client.GetAsync(endPoint,cancellationToken);
        var response = await responseRequest.Content.ReadFromJsonAsync<Response<Category>>(cancellationToken);
      
        if (!responseRequest.IsSuccessStatusCode && response is null)
            return new Error("Invalid","Invalid");
        
        return response ?? new Error("Invalid","Invalid");
    }

    public async Task<PagedResponse<List<Category>>> GetAll(GetAllCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = $"/categories/v1/{request.UserId}/{request.Page}/{request.PageSize}";
        
        var responseRequest = await _client.GetAsync(endPoint,cancellationToken);

        var response = await responseRequest.Content.ReadFromJsonAsync<PagedResponse<List<Category>>>(cancellationToken);
        if (!responseRequest.IsSuccessStatusCode && response is null)
            return new Error("Invalid","Invalid");

        return response ?? new Error("Invalid","Invalid");
    }

    public async Task<Response<Category>> Create(CreateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = "/Categories/v1";
        
        Console.WriteLine(request.Description);
        Console.WriteLine(request.Title);
        
        var responseRequest = await _client.PostAsJsonAsync(endPoint, request, cancellationToken);

        var result = await responseRequest.Content.ReadFromJsonAsync<Response<Category>>(cancellationToken);
        if (result is null && !responseRequest.IsSuccessStatusCode)
            return new Error("Invalid", "Invalid");

        return result ?? new Error("Invalid","Invalid");
    }

    public async Task<Response<Category>> Update(UpdateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = "/Categories/v1";
        var responseRequest = await _client.PutAsJsonAsync(endPoint, request, cancellationToken);

        var result = await responseRequest.Content.ReadFromJsonAsync<Response<Category>>(cancellationToken);
        if (result is null && !responseRequest.IsSuccessStatusCode)
            return new Error("Invalid", "Invalid");

        return result ?? new Error("Invalid","Invalid");
    }

    public async Task<Response<Category>> Delete(DeleteCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = $"/Categories/v1/{request.Id}/{request.UserId}";
        var responseRequest = await _client.DeleteAsync(endPoint, cancellationToken);

        var result = await responseRequest.Content.ReadFromJsonAsync<Response<Category>>(cancellationToken);
        if (result is null && !responseRequest.IsSuccessStatusCode)
            return new Error("Invalid", "Invalid");

        return result ?? new Error("Invalid","Invalid");
    }
}