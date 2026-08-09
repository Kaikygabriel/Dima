using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Response;
using Microsoft.AspNetCore.Components;
using MudBlazor;

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

    [Inject] public NavigationManager Nav { get; set; } = null!;

    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    
    protected PatternMask Mask = new("########")
    {
        Placeholder = '_',
    };
    
    protected Voucher? Voucher;
    protected Product? Product;
    protected Error? Error;
    protected CreateOrderRequest Request = new ();
    protected decimal Total;
    protected bool IsBusy;
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

    protected async void InsertVoucher()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(VoucherCode))
                return;
        
            var voucherResult = await VoucherHandler.GetByCodeAsync(new() { Code = VoucherCode });
            if (voucherResult.IsSuccess)
            {
                Voucher = voucherResult.Data;
                Total = Product!.Price - (Voucher?.Amount ?? 0);
                StateHasChanged();
            }
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
    }

    protected async void OnValidSubmit()
    {
        try
        {
            IsBusy = true;
            var request = new CreateOrderRequest()
            {
                ProductId = Product?.Id ?? Guid.Empty,
                VoucherId = Voucher?.Id
            };
            var result = await OrderHandler.CreateAsync(request);
            if (!result.IsSuccess)
            {
                Snackbar.Add(result.Error?.Title ?? "Invalid Order", Severity.Error);
                Snackbar.Add(result.Error?.Message ?? "Invalid Order", Severity.Error);
                return;
            }

            Nav.NavigateTo($"/order/{result.Data!.Id}");
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
            StateHasChanged();
        }
    }
}