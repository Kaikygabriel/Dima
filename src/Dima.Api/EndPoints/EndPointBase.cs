using Dima.Api.EndPoints.Categories;
using Dima.Api.Interfaces.Endpoint;

namespace Dima.Api.EndPoints;

public static class EndPointBase
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");
        
        endpoints.MapGroup("Categories")
            .WithTags("Categories")
            .Map<CreateCategoryEndPoint>()
            .Map<GetAllCategoryEndPoint>()
            .Map<GetCategoryByIdEndPoint>()
            .Map<UpdateCategoryEndPoint>()
            .Map<DeleteCategoryEndPoint>();
        
        return app;
    }

    private static IEndpointRouteBuilder Map<T>(this IEndpointRouteBuilder builder) where T : IEndPoint
    {
        T.Map(builder);
        return builder;
    }
}