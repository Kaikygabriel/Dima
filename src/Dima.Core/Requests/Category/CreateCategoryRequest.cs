using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Category;

public sealed record CreateCategoryRequest(
    [Required]
    [Length(minimumLength:3,80)]
    string Title,
    [Required]
    string Description) : Request;