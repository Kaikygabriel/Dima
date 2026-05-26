namespace Dima.Api.Interfaces.Endpoint;

public interface IEndPoint
{
    static abstract void Map(IEndpointRouteBuilder builder);
}