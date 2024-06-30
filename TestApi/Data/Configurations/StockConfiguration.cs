using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApi.Data.Configurations.Common;
using TestApi.Models;

namespace TestApi.Data.Configurations;

/// <summary>
/// Represents the configuration for the Stock entity.
/// </summary>
public class StockConfiguration : BaseConfiguration<Stock>
{
    /// <summary>
    /// Configures the entity of type <see cref="Stock"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public override void Configure(EntityTypeBuilder<Stock> builder)
    {
        // Configure the symbol property
        builder.Property(x => x.Symbol)
               .HasColumnType("varchar(max)")
               .IsRequired();

        // Configure the company name property
        builder.Property(x => x.CompanyName)
               .HasColumnType("varchar(max)")
               .IsRequired();

        // Configure the purchase property
        builder.Property(x => x.Purchase)
               .HasPrecision(18, 2)
               .HasColumnType("decimal")
               .IsRequired();

        // Configure the last dividend property
        builder.Property(x => x.LastDiv)
               .HasPrecision(18, 2)
               .HasColumnType("decimal")
               .IsRequired();

        // Configure the industry property
        builder.Property(x => x.Industry)
               .HasColumnType("varchar(max)")
               .IsRequired();

        // Configure the market capitalization property
        builder.Property(x => x.MarketCap)
               .HasColumnType("bigint")
               .IsRequired();

        // Configure the relation with Comment
        builder.HasMany(x => x.Comments)
                .WithOne(c => c.Stock)
                .HasForeignKey(c => c.StockId)
                .OnDelete(DeleteBehavior.NoAction);

        // Configure the relation with Portfolio
        builder.HasMany(x => x.Portfolios)
               .WithOne(p => p.Stock)
               .HasForeignKey(p => p.StockId)
               .OnDelete(DeleteBehavior.NoAction);

        // Call the base configuration method
        base.Configure(builder);
    }
}
