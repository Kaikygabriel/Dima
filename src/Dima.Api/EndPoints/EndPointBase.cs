using Dima.Api.EndPoints.Categories;
using Dima.Api.EndPoints.Identity;
using Dima.Api.EndPoints.Order;
using Dima.Api.EndPoints.Products;
using Dima.Api.EndPoints.Reports;
using Dima.Api.EndPoints.Stripe;
using Dima.Api.EndPoints.Transactions;
using Dima.Api.EndPoints.Voucher;
using Dima.Api.Interfaces.Endpoint;
using Dima.Api.Models;
using Dima.Core.Requests.Orders;

namespace Dima.Api.EndPoints;

public static class EndPointBase
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");
        
        app.MapGet("/", () => Results.Ok());
        
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
            .Map<GetAllTransactionsByCreateAtEndPoint>()
            .Map<GetAllCategoryByCreateTransactionEndPoint>();

        endpoints.MapGroup("v1/Identity")
            .Map<LogoutEndPoint>()
            .Map<GetRolesEndPoint>()
            .Map<GetInfoId>();
        
        endpoints.MapGroup("v1/Identity")
            .MapIdentityApi<User>();

        endpoints.MapGroup("v1/Order")
            .WithTags("Orders")
            .RequireAuthorization()
            .Map<GetAllOrdersEndPoint>()
            .Map<GetByIdOrderEndPoint>()
            .Map<CreateOrderEndPoint>()
            .Map<RefoundOrderEndPoint>()
            .Map<PayOrderEndPoint>()
            .Map<CancelOrderEndPoint>();
        
        endpoints.MapGroup("v1/Product")
            .WithTags("Products")
            .RequireAuthorization()
            .Map<GetAllProductsEndPoint>()
            .Map<GetProductByIdEndPoint>();
        
        endpoints.MapGroup("v1/Voucher")
            .WithTags("Vouchers")
            .RequireAuthorization()
            .Map<GetVoucherByCodeEndPoint>();
            
        endpoints.MapGroup("v1/Reports")
            .WithTags("Reports")
            .RequireAuthorization()
            .Map<GetExpensesAndIncomeEndPoint>()
            .Map<GetExpensesByCategoryEndPoint>()
            .Map<GetIncomeByCategoryEndPoint>()
            .Map<GetFinanceSummaryEndPoint>() ;
        
        endpoints.MapGroup("v1/Stripe")
            .WithTags("Stripe")
            .RequireAuthorization()
            .Map<CreateSessionEndPoint>();

        return app;
    }

    private static IEndpointRouteBuilder Map<T>(this IEndpointRouteBuilder builder) where T : IEndPoint
    {
        T.Map(builder);
        return builder;
    }
}