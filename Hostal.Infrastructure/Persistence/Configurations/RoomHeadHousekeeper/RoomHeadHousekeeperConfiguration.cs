using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostal.Infrastructure.Persistence.Configurations.RoomHeadHousekeeper;

public class RoomHeadHousekeeperConfiguration: IEntityTypeConfiguration<Domain.Entities.RoomHeadHousekeeper>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.RoomHeadHousekeeper> builder)
    {
        // Configuración de la tabla intermedia
        builder.ToTable("RoomHeadHousekeepers");

        // Clave compuesta
        builder.HasKey(rhh => new { rhh.RoomId, rhh.HeadHousekeeperId });

        // Configuración de propiedades
        builder.Property(rhh => rhh.RoomId)
            .IsRequired();

        builder.Property(rhh => rhh.HeadHousekeeperId)
            .IsRequired();

        builder.Property(rhh => rhh.AssignedDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        // Índices para mejorar performance
        builder.HasIndex(rhh => rhh.RoomId)
            .HasDatabaseName("IX_RoomHeadHousekeepers_RoomId");

        builder.HasIndex(rhh => rhh.HeadHousekeeperId)
            .HasDatabaseName("IX_RoomHeadHousekeepers_HeadHousekeeperId");

        // Relaciones con las entidades principales
        builder.HasOne(rhh => rhh.Room)
            .WithMany(r => r.RoomHeadHousekeepers)
            .HasForeignKey(rhh => rhh.RoomId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(rhh => rhh.HeadHousekeeper)
            .WithMany(h => h.RoomHeadHousekeepers)
            .HasForeignKey(rhh => rhh.HeadHousekeeperId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}