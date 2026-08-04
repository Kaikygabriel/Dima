using System.Net.Http.Json;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Response;

namespace Dima.Pwa.Handlers;

internal sealed  class VoucherHandler : IVoucherHandler
{
    private readonly HttpClient _httpClient;

    public VoucherHandler(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient(Configuration.HttpClientName);
    }

    public async Task<Response<Voucher>> GetByCodeAsync(GetVoucherByCodeRequest request)
    {
        var endPoint = $"v1/Voucher/{request.Code}";

        var result = await _httpClient.GetFromJsonAsync<Response<Voucher>>(endPoint);

        return result ?? new Error("Result Invalid","Result Invalid");
    }
}