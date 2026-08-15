using Dima.Api.Configurations;
using Dima.Api.Data.Context;
using Dima.Core.Handler;
using Dima.Core.Requests.Stripe;
using Dima.Core.Response;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;
using SessionService = Stripe.Checkout.SessionService;

namespace Dima.Api.Handlers;

internal sealed class StripeHandler : IStripeHandler
{
    private readonly AppDbContext _appDbContext;

    public StripeHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Response<string>> CreateSession(CreateSessionRequest request)
    {
        var order = await _appDbContext.Orders
            .Include(x => x.Product)
            .Include(x => x.Voucher)
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        if (order is null)
            return new Error("Order not found", "Order not found");
        
        var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == order.UserId);
        
        var options = new SessionCreateOptions()
        {
            CustomerEmail =request.UserEmail,
            PaymentIntentData = new SessionPaymentIntentDataOptions
            {
                Metadata = new Dictionary<string, string>
                {
                    {"order",request.Id.ToString()}
                }
            },
            PaymentMethodTypes = ["card","boleto"],
            LineItems = 
            [
                new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        Currency = "BRL",
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = order.Product.Title,
                            Description = order.Product.Description 
                        },
                        UnitAmount = (long)Math.Round(order.Total *100m,2)
                    },
                    Quantity = 1
                }
            ],
            Mode= "payment",
            SuccessUrl = $"{ApiConfiguration.FrontEndUrl}/Order/{order.Id}/confirm",
            CancelUrl = $"{ApiConfiguration.FrontEndUrl}/Order/{order.Id}/cancel",
        };
        var services = new SessionService();
        var result=  await services.CreateAsync(options);
        
        
        return result.Id ;
    }

    public Task<Response<List<StripeTransactionReponse>>> GetTransactionsByOrder(GetTransactionsByOrderRequest request)
    {
        throw new NotImplementedException();
    }
}