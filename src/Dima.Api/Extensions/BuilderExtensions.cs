using Dima.Api.Data.Context;
using Dima.Api.Handlers;
using Dima.Api.Models;
using Dima.Core.Handler;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Extensions;

public  static class BuilderExtensions
{
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        var connection = builder.Configuration.GetConnectionString("DefaultConnection")
                         ?? throw new Exception("Connection String Not Found !");
        
        builder.Services.AddDbContext<AppDbContext>(x => 
            x.UseSqlServer(connection));

        builder.Services.AddProblemDetails();
        return builder;
    }

    public static WebApplicationBuilder AddDependency(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IReportHandler , ReportHandler>();
        builder.Services.AddTransient<ICategoryHandler,CategoryHandler>();
        builder.Services.AddTransient<ITransactionHandler,TransactionHandler>();
        builder.Services.AddExceptionHandler<ExceptionGlobalHandler>();

        return builder;
    }

    public static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(x =>
            x.AddDefaultPolicy( x =>
                x.WithOrigins(builder.Configuration["Url:FrontEndHttp"]!,builder.Configuration["Url:FrontEndHttps"]!)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials())
            );
        return builder;
    }
    
    public static WebApplicationBuilder AddConfigurationLogging(this WebApplicationBuilder builder)
    {
        builder.Services.AddLogging();
        builder.WebHost.ConfigureKestrel(x => x.AddServerHeader = false);
        
        return builder;
    }

    public static WebApplicationBuilder AddSecurity(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentityCore<User>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();

        builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();
        builder.Services.AddAuthorization();
        return builder;
    }
}