using Dima.Api.Data.Context;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Category;
using Dima.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

internal class CategoryHandler : ICategoryHandler
{
    private readonly AppDbContext _appDbContext;

    public CategoryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Response<Category?>> GetById(GetCategoryByIdRequest request, CancellationToken cancellationToken = default)
    {
        var category = await _appDbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id&& x.UserId == request.UserId,cancellationToken);
        if (category is null)
            return new Error("Category.NotFound", "not found category !");
        
        return category;
    }

    public async Task<Response<List<Category>?>> GetAll(GetAllCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var categories = await _appDbContext.Categories
            .Where(x=>x.UserId == request.UserId)
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        if (!categories.Any())
            return new Error("Categories.NotFound", "Categories not found !");
        return categories;
    }

    public async Task<Response<Category>> Create(CreateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        if (await _appDbContext.Categories.AnyAsync(x => x.Title == request.Title,cancellationToken))
            return new Error("Category.Exists", "The title in category already exists !");
        
        var category = new Category(request.Title, request.Description,request.UserId);
        _appDbContext.Add(category);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<Response<Category>> Update(UpdateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var category =await _appDbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId, cancellationToken);
        if (category is null)
            return new Error("Category.NotFound", "category not found !");
        
        category = new Category(request.Title, request.Description,request.UserId)
        {
            Id = request.Id
        };
        
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