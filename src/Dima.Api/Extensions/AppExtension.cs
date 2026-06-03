namespace Dima.Api.Extensions;

public static class AppExtension
{
    public static WebApplication UseDeveloperEnvironment(this WebApplication app)
    {
        app.MapOpenApi();
        app.UseSwaggerUI(x=>x.SwaggerEndpoint("/openapi/v1.json","Dima v1"));
        app.UseExceptionHandler();
        
        return app;
    }
    
    public static WebApplication UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}