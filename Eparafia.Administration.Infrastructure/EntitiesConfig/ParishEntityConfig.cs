using Eparafia.Administration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Administration.Infrastructure.EntitiesConfig;

internal sealed class ParishEntityConfig : IEntityTypeConfiguration<Parish>
{
    public void Configure(EntityTypeBuilder<Parish> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        
        builder.HasMany(c => c.BaptismRegisters)
            .WithOne(c => c.Parish)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);        
        builder.HasMany(c => c.WeddingRegisters)
            .WithOne(c => c.Parish)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);        
        builder.HasMany(c => c.DeadRegisters)
            .WithOne(c => c.Parish)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);        
        builder.HasMany(c => c.HomeRecords)
            .WithOne(c => c.Parish)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(c => c.Priests)
            .WithOne(c => c.Parish)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}