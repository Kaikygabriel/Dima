namespace Dima.Api.Interfaces.Handler;

public interface IHandler<TRequest,TResponse>
{
     Task<TResponse> Handle(TRequest response,CancellationToken cancellationToken=  default);
}