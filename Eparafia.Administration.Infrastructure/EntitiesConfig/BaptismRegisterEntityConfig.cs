using Eparafia.Administration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Administration.Infrastructure.EntitiesConfig;

internal sealed class BaptismRegisterEntityConfig : IEntityTypeConfiguration<BaptismRegister>
{
    public void Configure(EntityTypeBuilder<BaptismRegister> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(c => c.Parish)
            .WithMany(c => c.BaptismRegisters)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}