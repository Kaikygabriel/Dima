using Dima.Api.Category.Request;
using Dima.Api.Category.Response;
using Dima.Api.Data.Context;
using Dima.Api.Interfaces;

namespace Dima.Api.Category.Handler;

internal class CreateCategoryHandler : IHandler<CreateCategoryRequest,CreateCategoryResponse>
{
    private readonly AppDbContext _context;

    public CreateCategoryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CreateCategoryResponse> Handle
        (CreateCategoryRequest request,CancellationToken cancellationToken = default)
    {
        var category = new Core.Models.Category(request.Title, request.Description, request.UserId);
        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);
        
        return new CreateCategoryResponse(request.Title, request.Description ?? "", DateTime.Now);
    }
}