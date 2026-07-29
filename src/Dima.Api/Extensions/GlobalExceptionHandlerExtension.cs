using Microsoft.AspNetCore.Diagnostics;

namespace Dima.Api.Extensions;

public static class GlobalExceptionHandlerExtension
{
    public static void UseGlobalExceptionHandler(this WebApplication app)
        => app.UseExceptionHandler(x=> 
            x.Run(async x =>
            {
                x.Response.StatusCode = StatusCodes.Status500InternalServerError;
                x.Response.ContentType = "application/json";
                var exceptionHandlerFeature = x.Features.Get<IExceptionHandlerFeature>();
                if (exceptionHandlerFeature is not null)
                    await x.Response.WriteAsJsonAsync(new
                    {
                        StackTrace = exceptionHandlerFeature.Error.StackTrace,
                        Message= exceptionHandlerFeature.Error.Message,
                        Source= exceptionHandlerFeature.Error.Source,
                        EndPoint= exceptionHandlerFeature.Endpoint,
                        Path= exceptionHandlerFeature.Path,
                        StatusCode = 500
                    });
            })
        );
}