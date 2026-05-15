using Dima.Api.Category.Request;
using Dima.Api.Category.Response;
using Dima.Api.Interfaces;

namespace Dima.Api.Category.Handler;

internal class CreateCategoryHandler : IHandler<CreateCategoryRequest,CreateCategoryResponse>
{
    public async Task<CreateCategoryResponse> Handle
        (CreateCategoryRequest request)
    {
        await Task.Delay(200);
        Console.WriteLine("criou a categoria : " + request.Title);
        return new CreateCategoryResponse(request.Title, request.Summary, DateTime.Now);
    }
}