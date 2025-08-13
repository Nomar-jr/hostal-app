using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostal.Infrastructure.Persistence.Configurations.Client;

public class ClientConfiguration: IEntityTypeConfiguration<Domain.Entities.Client>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Client> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(80).IsRequired();
        builder.Property(x => x.CI).HasMaxLength(11).IsRequired();
        builder.Property(x => x.Phone).HasMaxLength(20).IsRequired();
        builder.HasMany(x => x.Reservations).WithOne(x => x.Client)
            .HasForeignKey(x => x.ClientId);
    }
}