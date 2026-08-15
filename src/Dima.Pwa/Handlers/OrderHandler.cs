using System.Net.Http.Json;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Response;
using Dima.Pwa.Configurations;

namespace Dima.Pwa.Handlers;

internal sealed class OrderHandler : IOrderHandler
{
    private readonly HttpClient _httpClient;

    public OrderHandler(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient(Configuration.HttpClientName);
    }
    
    public async Task<Response<Order>> CreateAsync(CreateOrderRequest request)
    {
        var endPoint = "/v1/Order";
        var resultRequest = await _httpClient.PostAsJsonAsync(endPoint, request);
        var response = await resultRequest.Content.ReadFromJsonAsync<Response<Order>>();
        
        var responseOther = resultRequest.IsSuccessStatusCode 
            ? Response<Order>.Success()
            : new Error("Invalid", "Invalid");
        
        return response ??responseOther;
    }

    public async Task<Response<Order>> PayAsync(PayOrderRequest request)
    {
        var endPoint = "/v1/Order/pay";
        var resultRequest = await _httpClient.PostAsJsonAsync(endPoint, request);
        var response = await resultRequest.Content.ReadFromJsonAsync<Response<Order>>();
        
        return response! ;
    }

    public async Task<Response<Order>> RefundAsync(RefundOrderRequest request)
    {
        var endPoint = "/v1/Order/refund";
        var resultRequest = 
            await _httpClient.PostAsJsonAsync(endPoint, request);
        var response = await resultRequest.Content.ReadFromJsonAsync<Response<Order>>();
        
        var responseOther = resultRequest.IsSuccessStatusCode 
            ? Response<Order>.Success()
            : new Error("Invalid", "Invalid");
        
        return response ??responseOther;
    }

    public async Task<Response<Order>> CancelAsync(CancelOrderRequest request, CancellationToken cancellationToken = default)
    {
        var endPoint = "/v1/Order/cancel";

        var resultRequest = await _httpClient.PutAsJsonAsync(endPoint, request,cancellationToken);
        
        var response = await resultRequest.Content.ReadFromJsonAsync<Response<Order>>(cancellationToken);
        
        var responseOther = resultRequest.IsSuccessStatusCode 
            ? Response<Order>.Success()
            : new Error("Invalid", "Invalid");
        
        return response ??responseOther;

    }

    public async Task<PagedResponse<List<Order>>> GetAllAsync(GetAllOrdersRequest request)
    {
        var endPoint = $"/v1/Order?Page={request.Page}&PageSize{request.PageSize}";
        var result = await _httpClient.GetFromJsonAsync<PagedResponse<List<Order>>>(endPoint);
        return result ?? new Error("Invalid","Invalid");
    }

    public async Task<Response<Order>> GetByIdAsync(GetOrderByIdRequest request)
    {
        var endPoint = $"/v1/Order/{request.Id}";
        var result = await _httpClient.GetFromJsonAsync<Response<Order>>(endPoint);
        return result ?? new Error("Invalid","Invalid");
    }
}