using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApi.Data.Configurations.Common;
using TestApi.Models;

namespace TestApi.Data.Configurations;

/// <summary>
/// Represents the configuration for the Portfolio entity.
/// </summary>
public class PortfolioConfiguration : BaseConfiguration<Portfolio>
{
    /// <summary>
    /// Configures the entity of type <see cref="Portfolio"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public override void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.HasOne(x => x.AppUser)
               .WithMany(u => u.Portfolios)
               .HasForeignKey(x => x.AppUserId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Stock)
               .WithMany(s => s.Portfolios)
               .HasForeignKey(x => x.StockId)
               .OnDelete(DeleteBehavior.NoAction);

        base.Configure(builder);
    }
}
