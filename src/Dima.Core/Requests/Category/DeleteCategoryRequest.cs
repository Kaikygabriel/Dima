namespace Dima.Core.Requests.Category;

public sealed record DeleteCategoryRequest(Guid Id,Guid UserId) : Request(UserId);