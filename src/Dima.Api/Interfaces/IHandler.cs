namespace Dima.Api.Interfaces;

public interface IHandler<TRequest,TResponse>
{
     Task<TResponse> Handle(TRequest response,CancellationToken cancellationToken=  default);
}