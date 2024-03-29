using Eparafia.Administration.Domain.Entities.Dead;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Administration.Infrastructure.EntitiesConfig.Dead;

internal sealed class DeadClientEntityConfig : IEntityTypeConfiguration<DeadRegister>
{
    public void Configure(EntityTypeBuilder<DeadRegister> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(c => c.Parish)
            .WithMany(c => c.DeadRegisters)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);        
        
        builder.HasOne(c => c.SacramentalMaker)
            .WithMany(c => c.DeadRegisters)
            .HasForeignKey(c => c.SacramentalMakerId)    
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(c => c.Client)
            .WithOne(c => c.DeadRegister)
            .HasForeignKey<DeadRegister>(c => c.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}