using Dima.Api.EndPoints.Categories;
using Dima.Api.EndPoints.Identity;
using Dima.Api.EndPoints.Transactions;
using Dima.Api.Interfaces.Endpoint;
using Dima.Api.Models;

namespace Dima.Api.EndPoints;

public static class EndPointBase
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");
        
        app.MapGet("/", () => Results.Ok);
        
        endpoints.MapGroup("Categories")
            .WithTags("Categories")
            .Map<CreateCategoryEndPoint>()
            .Map<GetAllCategoryEndPoint>()
            .Map<GetCategoryByIdEndPoint>()
            .Map<UpdateCategoryEndPoint>()
            .Map<DeleteCategoryEndPoint>();
        
        endpoints.MapGroup("Transaction")
            .WithTags("Transactions")
            .Map<CreateTransactionEndpoint>()
            .Map<UpdateTransactionEndpoint>()
            .Map<DeleteTransactionEndPoint>()
            .Map<GetByIdTransactionEndPoint>()
            .Map<GetAllTransactionsByPaidEndPoint>()
            .Map<GetAllTransactionsByCreateAtEndPoint>();
        
        endpoints.MapGroup("Identity")
            .Map<LogoutEndPoint>()
            .Map<GetRolesEndPoint>();
        
        endpoints.MapGroup("Identity")
            .MapIdentityApi<User>();

        return app;
    }

    private static IEndpointRouteBuilder Map<T>(this IEndpointRouteBuilder builder) where T : IEndPoint
    {
        T.Map(builder);
        return builder;
    }
}