using System.Net.Http.Json;
using Dima.Core.Handler;
using Dima.Core.Requests.Stripe;
using Dima.Core.Response;
using Dima.Pwa.Configurations;

namespace Dima.Pwa.Handlers;

public class StripeHandler : IStripeHandler
{
    private readonly HttpClient _httpClient;

    public StripeHandler(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient(Configuration.HttpClientName);
    }

    public async Task<Response<string>> CreateSession(CreateSessionRequest request)
    {
        var endPoint = "/v1/Stripe";
        var result = await _httpClient.PostAsJsonAsync(endPoint, request);
        var content = await result.Content.ReadFromJsonAsync<Response<string>>();

        return content ?? new Error("Error ao gerar O codigo", "Error ao gerar O codigo");
    }

    public async Task<Response<List<StripeTransactionReponse>>> GetTransactionsByOrder(GetTransactionsByOrderRequest request)
    {
        var endPoint = "/v1/Stripe";
                var result = await _httpClient.PostAsJsonAsync(endPoint, request);
                var content = await result.Content.ReadFromJsonAsync<Response<List<StripeTransactionReponse>>>();
        
                return content ?? new Error("Error ao gerar O codigo", "Error ao gerar O codigo");
    }
}