using Hostal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hostal.Infrastructure.Persistence;

public class HostalDbContext(DbContextOptions<HostalDbContext> options): DbContext(options)
{
    public DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HostalDbContext).Assembly);
    }
}