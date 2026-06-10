using Dima.Core.Requests.Accounts;
using Dima.Core.Response;

namespace Dima.Core.Handler;

public interface IUserHandler
{
    Task<Response<string>> LoginAsync(LoginRequest request);
    Task<Response<string>> RegisterAsync(RegisterRequest request);
    Task LogoutAsync();
}