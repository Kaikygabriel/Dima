using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Response;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Pwa.Pages.Orders;

public partial class Details : ComponentBase
{
    [Parameter]
    public Guid OrderId { get; set; }
    
    protected Error? Error;
    protected Order? Order;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    
    [Inject]
    public IOrderHandler OrderHandler { get; set; } = null!;
    
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var request = new GetOrderByIdRequest()
        {
            Id = OrderId
        };
        var result = await OrderHandler.GetByIdAsync(request);
        if (!result.IsSuccess)
        {
            Error = result.Error;
            return;
        }

        Order = result.Data;
    }
}