using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostal.Infrastructure.Persistence.Configurations.Room;

public class RoomConfiguration : IEntityTypeConfiguration<Domain.Entities.Room>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Room> builder)
    {
        // Configuración de la tabla
        builder.ToTable("Rooms");

        // Clave primaria
        builder.HasKey(r => r.Id);

        // Configuración del Id (Identity)
        builder.Property(r => r.Id)
            .HasColumnName("Id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        // Configuración del Number
        builder.Property(r => r.Number)
            .HasColumnName("Number")
            .IsRequired()
            .HasMaxLength(3)
            .HasColumnType("varchar(3)")
            .HasComment("Room number following format 0XY where X is floor (1-3) and Y is room number (1-5)");

        // Índice único para el número de habitación
        builder.HasIndex(r => r.Number)
            .IsUnique()
            .HasDatabaseName("IX_Rooms_Number");

        // Configuración de Capacity
        builder.Property(r => r.Capacity)
            .HasColumnName("Capacity")
            .IsRequired()
            .HasColumnType("int")
            .HasComment("Maximum number of occupants the room can accommodate");

        // Configuración de IsActive (Soft Delete)
        builder.Property(r => r.IsActive)
            .HasColumnName("IsActive")
            .IsRequired()
            .HasDefaultValue(true)
            .HasColumnType("bit")
            .HasComment("Soft delete flag - true means active, false means deleted");

        // Filtro global para soft delete
        builder.HasQueryFilter(r => r.IsActive);

        // Configuración de relaciones

        // Relación con HeadHousekeeper (Many-to-Many)
        builder.HasMany(x => x.RoomHeadHousekeepers)
            .WithOne(x => x.Room)
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.NoAction);
        
        // Relación con Reservations (One-to-Many)
        builder.HasMany(r => r.Reservations)
            .WithOne(res => res.Room) 
            .HasForeignKey(res => res.RoomId) 
            .OnDelete(DeleteBehavior.Restrict) // Evitar eliminación en cascada para preservar historial
            .HasConstraintName("FK_Reservations_Room");

        
    }
}
        