using Eparafia.Administration.Domain.Entities.BaptismEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Administration.Infrastructure.EntitiesConfig.Baptism;

internal sealed class BaptismClientEntityConfig : IEntityTypeConfiguration<BaptismClient>
{
    public void Configure(EntityTypeBuilder<BaptismClient> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.HasOne(c => c.BaptismRegister)
            .WithOne(c => c.Client)
            .HasForeignKey<BaptismClient>(c => c.BaptismRegisterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}