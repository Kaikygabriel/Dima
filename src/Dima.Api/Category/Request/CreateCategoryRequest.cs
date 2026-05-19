namespace Dima.Api.Category.Request;

public sealed record CreateCategoryRequest(string Title,string? Description,Guid UserId);