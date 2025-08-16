using System.Text.Json.Serialization;
using Hostal.Api.Middlewares.ErrorHandlingMiddleware;
using Hostal.Api.Middlewares.RequestLoggingMiddleware;
using Hostal.Application.ServicesExtensions;
using Hostal.Infrastructure.ServicesExtensions;

namespace Hostal.Api.ServicesExtensions;

public static class ServicesCollectionExtensions
{
    public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddInfrastructureServices(configuration);
        services.AddApplicationServices();
        services.AddScoped<ErrorHandlingMiddleware>();
        services.AddScoped<RequestLoggingMiddleware>();
    }
}