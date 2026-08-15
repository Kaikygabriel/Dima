using Dima.Core.Handler;
using Dima.Core.Requests.Orders;
using Dima.Core.Requests.Stripe;
using Dima.Pwa.Configurations;
using Dima.Pwa.Pages.Orders;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Dima.Pwa.Components.Order;

public partial class OrderActionComponent : ComponentBase
{
    [CascadingParameter(Name = "DetailsPage")] public Details DetailsPage { get; set; }
    [Parameter,EditorRequired] 
    public Core.Models.Order Order { get; set; }

    // [Parameter]
    // public EventCallback<Core.Models.Order> Event  { get; set; }
    [Inject]
    public IStripeHandler StripeHandler { get;private set; } = null!;
    
    [Inject]
        public IJSRuntime JsRuntime { get;private set; } = null!;
    
    [Inject]
    public IOrderHandler OrderHandler { get;private set; } = null!;
    
    [Inject]
    public IDialogService DialogService  { get;private set; } = null!;
    
    [Inject]
    public ISnackbar Snackbar { get;private set; } = null!;

    public async Task OnCancelClicked()
    {
        try
        {
            var result= await DialogService
                .ShowMessageBoxAsync("Deseja Cancelar esse pedido",$"Pedido : {Order.Id}","Sim","Não") ?? false;
            if (!result)
                return;
            var request = new CancelOrderRequest()
            {
                Id = Order.Id
            };
            var resultCancelOrder = await OrderHandler.CancelAsync(request);

            if (!resultCancelOrder.IsSuccess)
                Snackbar.Add(resultCancelOrder.Error?.Title??"Error" , Severity.Error);
            
            DetailsPage.HasChangeScreen(resultCancelOrder.Data!);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message , Severity.Error);
        }
    }
    
    public async Task OnRefoundClicked()
    {
        try
        {
            var result= await DialogService
                .ShowMessageBoxAsync("Deseja Estornar esse pedido",$"Pedido : {Order.Id}","Sim","Não") ?? false;
            if (!result)
                return;
            var request = new RefundOrderRequest()
            {
                Id = Order.Id
            };
            var resultCancelOrder = await OrderHandler.RefundAsync(request);

            if (!resultCancelOrder.IsSuccess)
                Snackbar.Add(resultCancelOrder.Error?.Title??"Error" , Severity.Error);
            
            DetailsPage.HasChangeScreen(resultCancelOrder.Data!);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message , Severity.Error);
        }
    }
    public async Task OnPaidClicked()
    {
        try
        {
            var request = new CreateSessionRequest()
            {
                Id = Order.Id
            };
            var result = await StripeHandler.CreateSession(request);
            if (!result.IsSuccess || result.Data is null)
            {
                Snackbar.Add(result.Error?.Title ?? "Error" , Severity.Error);
                return;
            }

            await JsRuntime.InvokeVoidAsync("checkout", StripeConfiguration.PublicKey, result.Data);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message , Severity.Error);
        }
    }
}