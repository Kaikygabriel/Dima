using Dima.Api.Data.Context;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Category;
using Dima.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

internal sealed class CategoryHandler : ICategoryHandler
{
    private readonly AppDbContext _appDbContext;

    public CategoryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<GetCategoryCreateTransaction>> GetAllCategoryToCreateTransaction(Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext
            .Categories
            .Where(x=>x.UserId == userId)
            .Select(x=> new GetCategoryCreateTransaction(x.Id,x.Title))
            .ToListAsync(cancellationToken);
    }

    public async Task<Response<Category>> GetById(GetCategoryByIdRequest request, CancellationToken cancellationToken = default)
    {
        var category = await _appDbContext.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id&& x.UserId == request.UserId,cancellationToken);
        
        if (category is null)
            return new Error("Category.NotFound", "not found category !");
        
        return category;
    }

    public async Task<PagedResponse<List<Category>>> GetAll(GetAllCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var query =_appDbContext.Categories
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId);
        
        var categories = await query
            .Skip((request.Page  - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        
        if (!categories.Any())
            return new Error("Categories.NotFound", "Categories not found !");

        var count = await query
            .CountAsync(cancellationToken);
        
        return  new(categories,request.Page,count,request.PageSize);
    }

    public async Task<Response<Category>> Create(CreateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        if (await _appDbContext.Categories.AnyAsync(x => x.Title == request.Title,cancellationToken))
            return new Error("Category.Exists", "The title in category already exists !");

        var category = new Category(request.Title, request.Description,request.UserId);
        _appDbContext.Categories.Add(category);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<Response<Category>> Update(UpdateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var category =await _appDbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId, cancellationToken);
        if (category is null)
            return new Error("Category.NotFound", "category not found !");

        category.Description = request.Description;
        category.Title = request.Title;
        
        _appDbContext.Update(category);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<Response<Category>> Delete(DeleteCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var category =await _appDbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id&& x.UserId == request.UserId, cancellationToken);
        if (category is null)
            return new Error("Category.NotFound", "category not found !");
        
        _appDbContext.Remove(category);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return category;
    }
}