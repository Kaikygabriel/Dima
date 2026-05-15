namespace Dima.Api.Category.Request;

public sealed record CreateCategoryRequest(string Title,string Summary,Guid UserId);