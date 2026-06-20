using System.Security.Claims;
using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Category;
using Dima.Core.Response;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Categories;

public class CreateCategoryEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/v1",async
                ([FromBody]CreateCategoryRequest request,ClaimsPrincipal claims,ICategoryHandler handler) =>
            {
                request.UserId = Guid.Parse(claims.Identity!.Name!);
                
                var result = await handler.Create(request);
                return
                    result.IsSuccess ? 
                        Results.Created() :
                        Results.BadRequest(result.Error);
            })
            .WithName("Categories : create")
            .WithSummary("Create new Category")
            .Produces<Response<Category>>();
    }
}