using Dima.Api.Configurations;
using Dima.Core.Handler;
using Dima.Core.Requests.Stripe;
using Dima.Core.Response;
using Stripe.Checkout;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;
using SessionService = Stripe.Checkout.SessionService;

namespace Dima.Api.Handlers;

internal sealed class StripeHandler : IStripeHandler
{
    public async Task<Response<string>> CreateSession(CreateSessionRequest request)
    {
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
                            Name = request.ProductTitle,
                            Description = request.ProductSummary 
                        },
                        UnitAmount = request.Total,
                    },
                    Quantity = 1
                }
            ],
            Mode= "payment",
            SuccessUrl = $"{ApiConfiguration.FrontEndUrl}/Order/{request.Id}/confirm",
            CancelUrl = $"{ApiConfiguration.FrontEndUrl}/Order/{request.Id}/cancel",
        };
        var services = new SessionService();
        var result=  await services.CreateAsync(options);
        
        
        return result.Url ;
    }

    public Task<Response<List<StripeTransactionReponse>>> GetTransactionsByOrder(GetTransactionsByOrderRequest request)
    {
        throw new NotImplementedException();
    }
}