using Hostal.Domain.Interfaces;
using Hostal.Infrastructure.Persistence;
using Hostal.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hostal.Infrastructure.ServicesExtensions;

public static class ServicesCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Sqlite");
        services.AddDbContext<HostalDbContext>(opt => opt.UseSqlite(connectionString)
            .EnableSensitiveDataLogging());
        services.AddDbContext<HostalDbContext>(option => option.UseSqlite(connectionString));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}