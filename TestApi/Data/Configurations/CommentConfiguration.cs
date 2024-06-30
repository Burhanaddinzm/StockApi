using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApi.Data.Configurations.Common;
using TestApi.Models;

namespace TestApi.Data.Configurations;

/// <summary>
/// Configures the entity of type <see cref="Comment"/>.
/// </summary>
public class CommentConfiguration : BaseConfiguration<Comment>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        // Configure the title property
        builder.Property(x => x.Title)
               .HasMaxLength(100)
               .HasColumnType("varchar")
               .IsRequired();

        // Configure the content property
        builder.Property(x => x.Content)
               .HasColumnType("varchar(max)")
               .IsRequired();

        // Configure the relation with AppUser
        builder.HasOne(x => x.AppUser)
               .WithMany()
               .HasForeignKey(x => x.AppUserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

        // Configure the relation with Stock
        builder.HasOne(x => x.Stock)
               .WithMany(s => s.Comments)
               .HasForeignKey(x => x.StockId)
               .OnDelete(DeleteBehavior.NoAction);

        // Call the base configuration method
        base.Configure(builder);
    }
}
