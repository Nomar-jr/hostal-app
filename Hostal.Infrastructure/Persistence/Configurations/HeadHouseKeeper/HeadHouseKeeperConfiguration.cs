using Hostal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostal.Infrastructure.Persistence.Configurations.HeadHouseKeeper;

public class HeadHouseKeeperConfiguration: IEntityTypeConfiguration<HeadHousekeeper>
{
    public void Configure(EntityTypeBuilder<HeadHousekeeper> builder)
    {
        builder.HasMany(x => x.Rooms).WithMany(x => x.HeadHousekeeper);
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(80).IsRequired();
        builder.Property(x => x.CI).HasMaxLength(11).IsRequired();
        builder.Property(x => x.Phone).HasMaxLength(20).IsRequired();
    }
}