using Dima.Core.Abstraction;

namespace Dima.Core.Models;

public class Category : Model
{
    private Category()
    {
        
    }
    public Category(string title, string? description ,Guid userId)
    {
        Title = title;
        Description = description;
        UserId = userId;
    }

    public string Title { get; set; } = null!; 
    public string? Description { get; set; }
    public Guid UserId { get; set; }
}