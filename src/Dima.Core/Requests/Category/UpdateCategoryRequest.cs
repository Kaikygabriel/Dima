using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Category;

public record UpdateCategoryRequest : Request
{
    [Required]
    public Guid Id { get; set; }
    [Length(minimumLength:3,80)]
    public string Title { get; set; } 
    [Required]
    public string Description { get; set; }
}