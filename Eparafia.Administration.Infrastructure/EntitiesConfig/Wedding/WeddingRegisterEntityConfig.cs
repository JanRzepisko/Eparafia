using Eparafia.Administration.Domain.Entities.WeddingEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Administration.Infrastructure.EntitiesConfig.Wedding;

internal sealed class WeddingRegisterEntityConfig : IEntityTypeConfiguration<WeddingRegister>
{
    public void Configure(EntityTypeBuilder<WeddingRegister> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(c => c.Parish)
            .WithMany(c => c.WeddingRegisters)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);        
        
        builder.HasOne(c => c.SacramentalMaker)
            .WithMany(c => c.WeddingRegisters)
            .HasForeignKey(c => c.SacramentalMakerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(c => c.Men)
            .WithOne(c => c.WeddingRegister)
            .HasForeignKey<Men>(c => c.WeddingRegisterId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(c => c.Women)
            .WithOne(c => c.WeddingRegister)
            .HasForeignKey<Men>(c => c.WeddingRegisterId)
            .OnDelete(DeleteBehavior.Cascade);        
        
                
        builder.HasMany(c => c.Witnesses)
            .WithOne(c => c.WeddingRegister)
            .HasForeignKey(c => c.WeddingRegisterId)
            .OnDelete(DeleteBehavior.Cascade);        
    }
}