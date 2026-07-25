using Dima.Core.Abstraction;

namespace Dima.Core.Models;

public class Product : Model
{
    private Product()
    {
         
    }
    public Product(string title, string description, decimal price)
    {
        Title = title;
        Description = description;
        IsActive = false;
        Price = price;
    }

    public string Title { get;private set; } = null!;
    public string Description { get;private  set; } = null!;
    public bool IsActive { get;private  set; }
    public decimal Price { get;private  set; }

    public void Active()
        => IsActive = true;
    
    public void NoActive()
        => IsActive = false;
}