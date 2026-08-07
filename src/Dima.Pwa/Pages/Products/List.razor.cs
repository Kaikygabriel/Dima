using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Response;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Pwa.Pages.Products;

public partial class ListProductPage : ComponentBase
{
    private int _page =1  ;
    public int Page
    {
        get => _page;
        set 
        {
            if(value > TotalPage)
                return;
            
            _page = value;
            TryGetProducts();
        }
    }

    private const int PageSize = 25;
    protected int TotalPage;
    
    protected Error? Error;
    protected List<Product>? Products;
    
    [Inject]
    public IProductHandler ProductHandler { get; set; } = null!;

    [Inject]
    public NavigationManager Nav { get; set; } = null!;
    

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(0);
        TryGetProducts();
    }

    protected async void TryGetProducts()
    {
        try
        {
            var request = new GetAllProductsRequest()
            {
                Page = _page,
                PageSize = PageSize
            };         
            var result = await ProductHandler.GetAllAsync(request);

            if (!result.IsSuccess)
            {
                Error = result.Error;
                return;
            }
            
            Products = result.Data;
            TotalPage = result.PageTotal; 
            
            StateHasChanged();
        }
        catch (Exception e)
        {
            Error = new Error("Internal Error",e.Message);
        }
    }
}