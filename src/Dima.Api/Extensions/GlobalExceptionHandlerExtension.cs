using Microsoft.AspNetCore.Diagnostics;

namespace Dima.Api.Extensions;

public static class GlobalExceptionHandlerExtension
{
    public static WebApplication UseGlobalExceptionHandlerDevelopment(this WebApplication app)
    {
        app.UseExceptionHandler(x =>
            x.Run(async x =>
            {
                x.Response.StatusCode = StatusCodes.Status500InternalServerError;
                x.Response.ContentType = "application/json";
                var errorContent = x.Features.Get<IExceptionHandlerFeature>();
                if (errorContent is not null)
                    await x.Response.WriteAsJsonAsync(new
                    {
                        errorContent.Error.StackTrace,
                        errorContent.Error.Message,
                        errorContent.Error.Source,
                        errorContent.Error.Data,
                    });
            }));
        return app;
    }
}