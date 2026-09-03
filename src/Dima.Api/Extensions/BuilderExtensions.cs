using Dima.Api.Configurations;
using Dima.Api.Data.Context;
using Dima.Api.Handlers;
using Dima.Api.Models;
using Dima.Core.Handler;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Dima.Api.Extensions;

public  static class BuilderExtensions
{
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        var connection = builder.Configuration["ConnectionStrings_DefaultConnection"]
                         ?? throw new Exception("Connection String Not Found !");
        
        builder.Services.AddDbContext<AppDbContext>(x => 
            x.UseSqlServer(connection));

        StripeConfiguration.ApiKey = builder.Configuration["ApiConfiguration_StripeKey"]
                                     ?? throw new Exception("Key Stripe Not Found !");

        ApiConfiguration.SecretKeyWebHook = builder.Configuration["Stripe_WebHookKey"]
                                         ?? throw new Exception("Key Stripe WEb HOOK Not Found !");
        
        builder.Services.AddOptions<ApiConfiguration>()
            .BindConfiguration("ApiConfiguration")
            .ValidateOnStart()
            .ValidateDataAnnotations();
        
        builder.Services.AddProblemDetails();
        return builder;
    }

    public static WebApplicationBuilder AddDependency(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IStripeHandler,StripeHandler>();
        builder.Services.AddTransient<IVoucherHandler , VoucherHandler>();
        builder.Services.AddTransient<IProductHandler, ProductHandler>();
        builder.Services.AddTransient<IOrderHandler,OrderHandler>();
        
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
                x.WithOrigins(builder.Configuration["Url_FrontEnd_Http"]!,builder.Configuration["Url_FrontEnd_Https"]!)
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