using Microsoft.AspNetCore.Diagnostics;

namespace Dima.Api.Handlers;

public class ExceptionGlobalHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionGlobalHandler> _logger;

    public ExceptionGlobalHandler(ILogger<ExceptionGlobalHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/json";
        var contentError = httpContext.Features.Get<IExceptionHandlerFeature>();

        if (contentError is not null)
        {
            var errorMessage = new
            {
                
                contentError.Error.Message,
                contentError.Error.StackTrace,
                contentError.Error.Source,
                contentError.Error.Data,
                Date = DateTime.UtcNow
            };
            var errorId = Guid.NewGuid();
            _logger.LogCritical(exception,$"{errorId} id of error",errorId);
            
            await httpContext.Response.WriteAsJsonAsync(errorMessage, cancellationToken: cancellationToken);
        }

        return true;
    }
}