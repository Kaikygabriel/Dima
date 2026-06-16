using System.Net.Http.Json;
using Dima.Core.Handler;
using Dima.Core.Requests.Accounts;
using Dima.Core.Response;

namespace Dima.Pwa.Handlers;

public sealed class UserHandler : IUserHandler
{
    private readonly HttpClient _client;
    
    public UserHandler(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(Configuration.HttpClientName);
    }

    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        var requestClient = await _client.PostAsJsonAsync("v1/Identity/login?useCookies=true",request);
        Console.WriteLine($"STATUS CODE {requestClient.StatusCode}");
        if (!requestClient.IsSuccessStatusCode)
            return new Error($"{(int)requestClient.StatusCode}",$"{requestClient.Content}");
        
        return Response<string>.Success("Login Realizado com Sucesso");
    }

    public async Task<Response<string>> RegisterAsync(RegisterRequest request)
    {
        var requestClient = await _client.PostAsJsonAsync("v1/Identity/register",request);
        if (!requestClient.IsSuccessStatusCode)
            return new Error($"{(int)requestClient.StatusCode}",$"{requestClient.Content}");

        return Response<string>.Success("Cadastro Realizado com Sucesso");
    }

    public async Task LogoutAsync()
    {
        using StringContent content = new StringContent(string.Empty); 
        var requestClient = await _client.PostAsync("v1/Identity/Logout",content);
    }
}