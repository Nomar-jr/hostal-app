using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostal.Infrastructure.Persistence.Configurations.Reservation;

public class ReservationConfiguration: IEntityTypeConfiguration<Domain.Entities.Reservation>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Reservation> builder)
    {
        // Configuración de la tabla
        builder.ToTable("Reservations");

        // Clave primaria
        builder.HasKey(r => r.Id);

        // Configuración del Id (Identity)
        builder.Property(r => r.Id)
            .HasColumnName("Id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        // Fecha de la reservación
        builder.Property(r => r.ReservationDate)
            .HasColumnName("FechaReservacion")
            .IsRequired()
            .HasColumnType("smalldatetime")
            .HasDefaultValueSql("GETDATE()") // Asigna la fecha actual por defecto
            .HasComment("Fecha en que se realizó la reserva.");

        // Fecha de entrada al Hostal
        builder.Property(r => r.StartDateReservation)
            .HasColumnName("FechaEntrada")
            .IsRequired()
            .HasColumnType("smalldatetime")
            .HasComment("Fecha de inicio de la estancia del cliente.");

        // Fecha de salida del Hostal
        builder.Property(r => r.EndDateReservation)
            .HasColumnName("FechaSalida")
            .IsRequired()
            .HasColumnType("smalldatetime")
            .HasComment("Fecha de fin de la estancia del cliente.");

        // Importe de la Renta de la Habitación
        builder.Property(r => r.TotalAmount)
            .HasColumnName("ImporteRenta")
            .IsRequired()
            .HasColumnType("decimal(10, 2)") // Precisión para valores monetarios
            .HasComment("Costo total de la reserva.");

        // EstaElClienteEnHostal
        builder.Property(r => r.IsClientInHostel)
            .HasColumnName("EstaElClienteEnHostal")
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnType("bit")
            .HasComment("Indica si el cliente ha realizado el check-in (true) o no (false).");

        // EstaCancelada
        builder.Property(r => r.IsCanceled)
            .HasColumnName("EstaCancelada")
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnType("bit")
            .HasComment("Indica si la reserva fue cancelada (true) o no (false).");

        // FechaCancelacion
        builder.Property(r => r.CancellationDate)
            .HasColumnName("FechaCancelacion")
            .IsRequired(false) // Nulable
            .HasColumnType("smalldatetime")
            .HasComment("Fecha en que se canceló la reserva. Nulo si no está cancelada.");

        // MotivoCancelacion
        builder.Property(r => r.CancellationReason)
            .HasColumnName("MotivoCancelacion")
            .IsRequired(false) // Nulable
            .HasMaxLength(500)
            .HasColumnType("varchar(500)")
            .HasComment("Motivo especificado por el cual se canceló la reserva.");

        // ---- Configuración de relaciones (Foreign Keys) ----

        // Relación con Cliente (One-to-Many)
        builder.HasOne(r => r.Client)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.Restrict) // Evita eliminar un cliente si tiene reservas
            .HasConstraintName("FK_Reservations_Client");

        // Relación con Habitación (One-to-Many)
        builder.HasOne(r => r.Room)
            .WithMany(room => room.Reservations)
            .HasForeignKey(r => r.RoomId)
            .OnDelete(DeleteBehavior.Restrict) // Evita eliminar una habitación si tiene reservas
            .HasConstraintName("FK_Reservations_Room");

        // ---- Índices para mejorar el rendimiento de las consultas ----
        builder.HasIndex(r => r.ClientId).HasDatabaseName("IX_Reservations_ClientId");
        builder.HasIndex(r => r.RoomId).HasDatabaseName("IX_Reservations_RoomId");
        builder.HasIndex(r => new { r.StartDateReservation, r.EndDateReservation })
               .HasDatabaseName("IX_Reservations_DateRange");

        // ---- Restricciones a nivel de base de datos (Check Constraints) ----

        // Asegura que la fecha de salida sea posterior a la fecha de entrada
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_Reservation_DateOrder",
            "[FechaSalida] > [FechaEntrada]"
        ));
        
        // Asegura que el importe sea un valor positivo
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_Reservation_AmountPositive",
            "[ImporteRenta] >= 0"
        ));
    }
}