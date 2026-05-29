using Dima.Api.EndPoints.Categories;
using Dima.Api.EndPoints.Transactions;
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
        
        endpoints.MapGroup("Transaction")
            .WithTags("Transactions")
            .Map<CreateTransactionEndpoint>()
            .Map<UpdateTransactionEndpoint>()
            .Map<DeleteTransactionEndPoint>()
            .Map<GetByIdTransactionEndPoint>()
            .Map<GetAllTransactionsByPaidEndPoint>()
            .Map<GetAllTransactionsByCreateAtEndPoint>();
        
        return app;
    }

    private static IEndpointRouteBuilder Map<T>(this IEndpointRouteBuilder builder) where T : IEndPoint
    {
        T.Map(builder);
        return builder;
    }
}