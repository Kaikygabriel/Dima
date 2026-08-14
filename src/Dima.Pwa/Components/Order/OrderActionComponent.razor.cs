using Dima.Core.Handler;
using Dima.Core.Requests.Orders;
using Dima.Pwa.Pages.Orders;
using Microsoft.AspNetCore.Components;
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
}