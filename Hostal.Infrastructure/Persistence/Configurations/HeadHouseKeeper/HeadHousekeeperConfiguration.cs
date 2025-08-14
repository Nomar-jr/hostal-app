using Hostal.Domain.Entities;
using Hostal.Infrastructure.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostal.Infrastructure.Persistence.Configurations.HeadHouseKeeper;

public class HeadHousekeeperConfiguration : IEntityTypeConfiguration<Domain.Entities.HeadHousekeeper>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.HeadHousekeeper> builder)
    {
        // Configuración de la tabla
        builder.ToTable("HeadHousekeepers");

        // Clave primaria
        builder.HasKey(h => h.Id);

        // Configuración del Id (Identity)
        builder.Property(h => h.Id)
            .HasColumnName("Id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        // Configuración de IsActive (Soft Delete)
        builder.Property(h => h.IsActive)
            .HasColumnName("IsActive")
            .IsRequired()
            .HasDefaultValue(true)
            .HasColumnType("bit")
            .HasComment("Soft delete flag - true means active, false means deleted");

        // Filtro global para soft delete
        builder.HasQueryFilter(h => h.IsActive);

        // Configuraciones heredadas de Person
        builder.ConfigurePersonProperties();
        
        // Configuración de la relación con Rooms (Many-to-Many)
        builder.HasMany(h => h.RoomHeadHousekeepers)
            .WithOne(x => x.HeadHousekeeper)
            .HasForeignKey(x => x.HeadHousekeeperId)
            .OnDelete(DeleteBehavior.NoAction);
        

    }
}