using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Response;
using Microsoft.AspNetCore.Components;

namespace Dima.Pwa.Pages.Orders;

public partial  class CheckoutPage : ComponentBase
{
    [SupplyParameterFromQuery] 
    public string? VoucherCode { get; set; }
    [Parameter]
    public Guid IdProduct { get; set; }
    
    [Inject] 
    public IOrderHandler OrderHandler { get; set; } = null!;
    [Inject]
    public IProductHandler ProductHandler { get; set; } = null!;
    [Inject]
    public IVoucherHandler VoucherHandler { get; set; } = null!;
    [Inject] 
    public NavigationManager Nav { get; set; }


    protected Voucher? Voucher;
    protected Product? Product;
    protected Error? Error;
    protected CreateOrderRequest Request = new ();
    protected decimal Total;
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            var request = new GetProductByIdRequest()
            {
                Id = IdProduct
            };
            var resultGetProductById = await ProductHandler.GetByIdAsync(request);

            if (!resultGetProductById.IsSuccess)
            {
                Error = resultGetProductById.Error;
                return;
            }

            Product = resultGetProductById.Data;


            if (VoucherCode is not null)
            {
                var voucherResult = await VoucherHandler.GetByCodeAsync(new() { Code = VoucherCode });
                if (voucherResult.IsSuccess)
                    Voucher = voucherResult.Data;
            }

            Total = Product!.Price - (Voucher?.Amount ?? 0);
        }
        catch (Exception e)
        {
            Error = new Error("Internal Error", "Not Found Product");
        }
    }
}