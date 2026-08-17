using System.IO;
using Dima.Api.Configurations;
using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Orders;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Dima.Api.EndPoints.Stripe;

public class WebHookEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        // 1. Removeu-se o ClaimsPrincipal da assinatura
        builder.MapPost("/api/webhook", async (HttpContext context, [FromServices] IOrderHandler orderHandler) =>
        {
            try
            {
                var json = await new StreamReader(context.Request.Body).ReadToEndAsync();
                var stripeSignature = context.Request.Headers["Stripe-Signature"];

                var stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, ApiConfiguration.SecretKeyWebHook);
                
                if (stripeEvent.Type is EventTypes.PaymentIntentSucceeded)
                {
                    if (stripeEvent.Data.Object is PaymentIntent paymentIntent)
                    {
                        // 2. Recupera os dados originais que você salvou no Metadata da Stripe
                        // Garanta que ao criar o PaymentIntent você enviou essas chaves!
                        if (paymentIntent.Metadata.TryGetValue("OrderId", out var orderIdStr) &&
                            paymentIntent.Metadata.TryGetValue("UserId", out var userIdStr))
                        {
                            await orderHandler.PayAsync(new PayOrderRequest()
                            {
                                Id = Guid.Parse(orderIdStr), // ID do Pedido do seu banco
                                UserId = Guid.Parse(userIdStr) // ID do Usuário do seu banco
                            });
                        }
                    }
                }

                return Results.Ok();
            }
            catch (StripeException e)
            {
                return Results.BadRequest($"Webhook Error: {e.Message}");
            }
        })
        .AllowAnonymous(); // 3. CRÍTICO: Ignora regras globais de autenticação/redirecionamento
    }
}
