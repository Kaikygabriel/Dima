using System.Text.RegularExpressions;
using Azure.Storage.Blobs;
using Dima.Api.Configurations;
using Dima.Api.Interfaces.Endpoint;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Storage;

public class ImageStorageEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/image", async ([FromBody] ImageFile file,[FromServices]StorageConfiguration configuration) =>
        {
            if (string.IsNullOrEmpty(file.Base64))
                return Results.BadRequest("Invalid base64");

            var base64Clean = Regex.Replace(file.Base64, @"^data:[^;]+;base64,", "");

            var bytes = Convert.FromBase64String(base64Clean);

            var name = Guid.NewGuid() + file.Extensions;
            Console.WriteLine($"BLOB NAME {name}");
            var client = new BlobClient(configuration.ConnectionString, configuration.ContainerImage,name);
            
            using var stream = new MemoryStream(bytes);
            await client.UploadAsync(stream);

            return Results.Ok(client.Uri.AbsoluteUri);
        });
    }
}

public record ImageFile(string Base64,string Extensions);