using Hostal.Application.ServicesExtensions;
using Hostal.Infrastructure.ServicesExtensions;

namespace Hostal.Api.ServicesExtensions;

public static class ServicesCollectionExtensions
{
    public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddInfrastructureServices(configuration);
        services.AddApplicationServices();
    }
}