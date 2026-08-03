using Dima.Core.Abstraction;
using Dima.Core.Enum;

namespace Dima.Core.Models;

public class Order : Model
{
    private Order()
    {
        
    }
    public Order( Product product, Guid userId, Voucher? voucher)
    {
        Product = product;
        ProductId = product.Id;
        UserId = userId;
        Voucher = voucher;
        VoucherId = voucher?.Id;
        PaymentGateway = EPaymentGateway.Stripe;
        StatePayment = EStatePayment.AwaitingPay;
    }

    public string? ExternalReference { get;private set; }

    public DateTime CreateAt { get; private init; } = DateTime.Now;
    public DateTime PaytAt { get; private set; } 
    public DateTime UpdateAt { get; private set; }

    public EPaymentGateway PaymentGateway { get; private set; } 
    public EStatePayment StatePayment { get;private set; }
    public Product Product { get; private set; } = null!;

    public Guid ProductId { get;private set; }
    public Guid UserId { get;private set; }

    public Voucher? Voucher { get;private set; }
    public Guid? VoucherId { get;private set; }

    public void AlterState(EStatePayment newStatePayment)
    {
        if (newStatePayment is EStatePayment.Paid)
            PaytAt = DateTime.Now;
        StatePayment = newStatePayment;
        UpdateAt = DateTime.Now;
    }

    public void AlterExternalCode(string newExternalCode)
    {
        ExternalReference = newExternalCode;
        UpdateAt = DateTime.Now;
    }
    public decimal Total => Product.Price - (Voucher?.Amount ?? 0);
}