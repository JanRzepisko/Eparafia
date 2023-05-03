using Eparafia.Administration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Administration.Infrastructure.EntitiesConfig;

internal sealed class SacramentalMakerEntityConfig : IEntityTypeConfiguration<SacramentalMaker>
{
    public void Configure(EntityTypeBuilder<SacramentalMaker> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        
        builder.HasMany(c => c.BaptismRegisters)
            .WithOne(c => c.SacramentalMaker)
            .HasForeignKey(c => c.SacramentalMakerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.WeddingRegisters)
            .WithOne(c => c.SacramentalMaker)
            .HasForeignKey(c => c.SacramentalMakerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(c => c.DeadRegisters)
            .WithOne(c => c.SacramentalMaker)
            .HasForeignKey(c => c.SacramentalMakerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}