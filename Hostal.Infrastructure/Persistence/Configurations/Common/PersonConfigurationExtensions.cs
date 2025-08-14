using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostal.Infrastructure.Persistence.Configurations.Common;

// Actualización del método de extensión PersonConfigurationExtensions
public static class PersonConfigurationExtensions
{
    /// <summary>
    /// Aplica configuraciones comunes para entidades que heredan de Person
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que hereda de Person</typeparam>
    /// <param name="builder">EntityTypeBuilder</param>
    public static void ConfigurePersonProperties<T>(this EntityTypeBuilder<T> builder) 
        where T : Domain.Entities.AbstractClass.Person
    {
        // CI Configuration
        builder.Property(p => p.CI)
            .HasColumnName("CI")
            .IsRequired()
            .HasMaxLength(11)
            .HasColumnType("varchar(11)")
            .HasComment("Carnet de Identidad - 11 digit national identification number");

        // Name Configuration  
        builder.Property(p => p.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("nvarchar(100)")
            .HasComment("First name of the person");

        // LastName Configuration
        builder.Property(p => p.LastName)
            .HasColumnName("LastName")
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("nvarchar(100)")
            .HasComment("Last name of the person");

        // Phone Configuration
        builder.Property(p => p.Phone)
            .HasColumnName("Phone")
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnType("varchar(20)")
            .HasComment("Contact phone number");

        // Common indexes (sin unique constraint aquí, se maneja en cada entidad específica)
        builder.HasIndex(p => new { p.Name, p.LastName })
            .HasDatabaseName($"IX_{typeof(T).Name}_FullName");

        builder.HasIndex(p => p.Phone)
            .HasDatabaseName($"IX_{typeof(T).Name}_Phone");
    }
}