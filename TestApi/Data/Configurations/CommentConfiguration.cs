using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApi.Data.Configurations.Common;
using TestApi.Models;

namespace TestApi.Data.Configurations;

public class CommentConfiguration : BaseConfiguration<Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(x => x.Title)
               .HasMaxLength(100)
               .HasColumnType("varchar")
               .IsRequired();

        builder.Property(x => x.Content)
               .HasColumnType("varchar")
               .IsRequired();

        builder.HasOne(x => x.AppUser)
               .WithMany()
               .HasForeignKey(x => x.AppUserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Stock)
               .WithMany(s => s.Comments)
               .HasForeignKey(x => x.StockId)
               .OnDelete(DeleteBehavior.NoAction);

        base.Configure(builder);
    }
}
