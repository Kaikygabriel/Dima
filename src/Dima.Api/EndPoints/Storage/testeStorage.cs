using System.Text.RegularExpressions;
using Azure.Storage.Blobs;
using Dima.Api.Configurations;
using Dima.Api.Interfaces.Endpoint;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Storage;

public class testeStorage : IEndPoint  
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/create/image",async ([FromBody] ImageFile file,[FromServices] StorageConfiguration config) =>
        {
            if(file is null || string.IsNullOrWhiteSpace(file.Base64))
                return Results.BadRequest("File invalid");
            
            var base64Clean = Regex.Replace(file.Base64, @"^data:[^;]+;base64,", "");
            var bytes = Convert.FromBase64String(base64Clean);
            
            var client = new BlobClient(config.ConnectionString, config.ContainerImage,
                Guid.NewGuid() + file.Extensions);
            
            using var stream = new MemoryStream(bytes);
            await client.UploadAsync(stream);

            return Results.Ok(client.Uri.AbsoluteUri);
        });
    }
}