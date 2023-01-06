using Eparafia.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eparafia.Infrastructure.EntityConfigs;

internal sealed class PostEntityTypeConfig : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Parish)
            .WithMany(c => c.Posts)
            .HasForeignKey(c => c.ParishId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(c => c.Files)
               .WithOne(c => c.Post)
               .HasForeignKey(c => c.PostId);
    }
}