using System.Net.Http.Json;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Response;
using Dima.Pwa.Configurations;

namespace Dima.Pwa.Handlers;

internal sealed class ProductHandler : IProductHandler
{
    private readonly HttpClient _httpClient;

    public ProductHandler(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient(Configuration.HttpClientName);
    }

    public async Task<PagedResponse<List<Product>>> GetAllAsync(GetAllProductsRequest request)
    {
        var endPoint = $"v1/Product?Page={request.Page}&PageSize={request.PageSize}";
        var response = await _httpClient.GetFromJsonAsync<PagedResponse<List<Product>>>(endPoint);
        return response ?? new Error("Invalid","Invalid");
    }

    public async  Task<Response<Product>> GetByIdAsync(GetProductByIdRequest request)
    {
        var endPoint = $"v1/Product/{request.Id}";
        var response = await _httpClient.GetFromJsonAsync<Response<Product>>(endPoint);
        return response ?? new Error("Invalid","Invalid");
    }
}