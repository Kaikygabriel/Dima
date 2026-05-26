using Dima.Core.Models;
using Dima.Core.Requests.Category;
using Dima.Core.Response;

namespace Dima.Core.Handler;

public interface ICategoryHandler
{
    Task<Response<Category>> GetById(GetCategoryByIdRequest request, CancellationToken cancellationToken = default);
    Task<PagedResponse<List<Category>>> GetAll(GetAllCategoryRequest request, CancellationToken cancellationToken = default);
    
    Task<Response<Category>> Create(CreateCategoryRequest request, CancellationToken cancellationToken = default);
    Task<Response<Category>> Update(UpdateCategoryRequest request, CancellationToken cancellationToken = default);
    Task<Response<Category>> Delete(DeleteCategoryRequest request, CancellationToken cancellationToken = default);
}