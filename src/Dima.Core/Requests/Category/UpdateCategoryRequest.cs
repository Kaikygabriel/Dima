using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Category;

public record UpdateCategoryRequest([Required]
    Guid Id,
    [Length(minimumLength:3,80)]
    string Title,
    [Required]
    string Description) : Request;