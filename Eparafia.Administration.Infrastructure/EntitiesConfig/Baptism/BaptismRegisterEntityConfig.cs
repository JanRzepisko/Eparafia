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
            .OnDelete(DeleteBehavior.SetNull);        
        
        builder.HasOne(c => c.SacramentalMaker)
            .WithMany(c => c.BaptismRegister)
            .HasForeignKey(c => c.SacramentalMakerId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(c => c.Parents)
            .WithOne(c => c.BaptismRegister)
            .HasForeignKey<BaptismRegister>(c => c.ParentsId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Godfather)
            .WithOne(c => c.BaptismRegister)
            .HasForeignKey<BaptismRegister>(c => c.GodfatherId)
            .OnDelete(DeleteBehavior.Cascade);        
        
        builder.HasOne(c => c.Godmother)
            .WithOne(c => c.BaptismRegister)
            .HasForeignKey<BaptismRegister>(c => c.GodmotherId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(c => c.Client)
            .WithOne(c => c.BaptismRegister)
            .HasForeignKey<BaptismRegister>(c => c.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}