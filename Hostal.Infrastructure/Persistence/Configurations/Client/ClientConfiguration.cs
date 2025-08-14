using Hostal.Infrastructure.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostal.Infrastructure.Persistence.Configurations.Client;

public class ClientConfiguration : IEntityTypeConfiguration<Domain.Entities.Client>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Client> builder)
    {
        // Configuración de la tabla
        builder.ToTable("Clients");

        // Clave primaria
        builder.HasKey(c => c.Id);

        // Configuración del Id (Identity)
        builder.Property(c => c.Id)
            .HasColumnName("Id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        // Configuración de IsVip
        builder.Property(c => c.IsVip)
            .HasColumnName("IsVip")
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnType("bit")
            .HasComment("Indicates if the client has VIP status for special benefits");

        // Configuración del Email (nullable)
        builder.Property(c => c.Email)
            .HasColumnName("Email")
            .IsRequired(false) // Nullable
            .HasMaxLength(320) // RFC 5321 standard for email length
            .HasColumnType("varchar(320)")
            .HasComment("Client's email address for communication and reservations");

        // Índice único para el email (solo si no es null)
        builder.HasIndex(c => c.Email)
            .IsUnique()
            .HasDatabaseName("IX_Clients_Email")
            .HasFilter("[Email] IS NOT NULL"); // Solo valores no nulos deben ser únicos

        // Configuración de IsActive (Soft Delete)
        builder.Property(c => c.IsActive)
            .HasColumnName("IsActive")
            .IsRequired()
            .HasDefaultValue(true)
            .HasColumnType("bit")
            .HasComment("Soft delete flag - true means active, false means deleted");

        // Filtro global para soft delete
        builder.HasQueryFilter(c => c.IsActive);

        // Configuraciones heredadas de Person usando el método de extensión
        builder.ConfigurePersonProperties();

        // Configuración específica de CI para clientes (índice único con filtro de soft delete)
        builder.HasIndex(c => c.CI)
            .IsUnique()
            .HasDatabaseName("IX_Clients_CI")
            .HasFilter("[IsActive] = 1"); // Solo CIs activos deben ser únicos

        // Configuración de la relación con Reservations (One-to-Many)
        builder.HasMany(c => c.Reservations)
            .WithOne(r => r.Client) // Asumiendo que Reservation tiene una propiedad Client
            .HasForeignKey(r => r.ClientId) // Asumiendo que Reservation tiene ClientId
            .OnDelete(DeleteBehavior.Restrict) // Evitar eliminación en cascada para preservar historial
            .HasConstraintName("FK_Reservations_Client");

        // Configuraciones de validación a nivel de base de datos

        /*
        // Check constraint para validar formato del email
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_Client_Email_Format", 
            "[Email] IS NULL OR ([Email] LIKE '%_@_%.__%' AND [Email] NOT LIKE '% %' AND LEN([Email]) >= 5)"
        ));

        // Check constraint para validar formato del CI (11 dígitos exactos)
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_Client_CI_Format", 
            "LEN([CI]) = 11 AND [CI] NOT LIKE '%[^0-9]%'"
        ));

        // Check constraint para validar formato del teléfono
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_Client_Phone_Format", 
            "LEN([Phone]) >= 7 AND [Phone] NOT LIKE '%[^0-9+-() ]%'"
        ));

        // Check constraint para validar que Name no esté vacío después de trim
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_Client_Name_NotEmpty", 
            "LEN(TRIM([Name])) > 0"
        ));

        // Check constraint para validar que LastName no esté vacío después de trim
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_Client_LastName_NotEmpty", 
            "LEN(TRIM([LastName])) > 0"
        ));

        // Índice compuesto para búsquedas comunes
        builder.HasIndex(c => new { c.Name, c.LastName, c.IsActive })
            .HasDatabaseName("IX_Clients_FullName_Active");

        // Índice para clientes VIP activos (consultas frecuentes)
        builder.HasIndex(c => new { c.IsVip, c.IsActive })
            .HasDatabaseName("IX_Clients_Vip_Active")
            .HasFilter("[IsVip] = 1 AND [IsActive] = 1");*/
    }
}