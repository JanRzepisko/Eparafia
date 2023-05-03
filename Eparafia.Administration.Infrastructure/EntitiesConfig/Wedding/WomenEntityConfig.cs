using Eparafia.Administration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Administration.Infrastructure.EntitiesConfig;

internal sealed class WomenEntityConfig : IEntityTypeConfiguration<Women>
{
    public void Configure(EntityTypeBuilder<Women> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(c => c.WeddingRegister)
            .WithOne(c => c.Women)
            .HasForeignKey<WeddingRegister>(c => c.WomenId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}