using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dima.Core.Requests.Category;


public sealed class CreateCategoryRequest : Abstraction.Request
{
    [JsonConstructor]
    public CreateCategoryRequest()
    {
        
    }
    public CreateCategoryRequest(string title,string description)
    {
        Title = title;
        Description = description;
    }
    
    [Required]
    [Length(minimumLength:3,80)]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; } 
    
}