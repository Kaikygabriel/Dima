using Microsoft.OpenApi;

namespace Dima.Api.Extensions;

public static class DocumentationExtension
{
    public static IServiceCollection AddDocumentation(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddOpenApi("v1",x =>
        {
            x.AddDocumentTransformer((document, context, ct) =>
            {
                document.Servers =
                [
                    new OpenApiServer { Url = configuration["Url:BackEndHttps"], Description = "Server in Https" },
                    new OpenApiServer { Url =configuration["Url:BackEndHttp"], Description = "Server in Http" }
                ];
                document.Info = new OpenApiInfo()
                {
                    Description = "Api For control of Money",
                    Version = "V1",
                    Title = "Dima"
                };
                return  Task.CompletedTask;
            });
        });

        return services;
    }
}