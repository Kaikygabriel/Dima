using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Response;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Pwa.Pages.Orders;

public partial class Confirm : ComponentBase
{
    protected Order? Order;
    protected Error? Error;
    
    [Parameter]
    public Guid Id { get; set; }

    [Inject]
    public IOrderHandler OrderHandler { get; set; } = null!;

    [Inject]
    public NavigationManager Nav { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            var request = new PayOrderRequest()
            {
                Id = Id
            };
            var result = await OrderHandler.PayAsync(request);

            if (!result.IsSuccess)
            {
                Error = result.Error;
                return;
            }

            Order = result.Data;
            Nav.NavigateTo($"/order/{Order!.Id}");
        }
        catch (Exception e)
        {
            Error = new Error("Internal Error",e.Message);
        }
    }
}