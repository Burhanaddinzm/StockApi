using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApi.Data.Configurations.Common;
using TestApi.Models;

namespace TestApi.Data.Configurations;

public class StockConfiguration : BaseConfiguration<Stock>
{
    public override void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.Property(x => x.Symbol)
               .HasColumnType("varchar")
               .IsRequired();

        builder.Property(x => x.CompanyName)
               .HasColumnType("varchar")
               .IsRequired();

        builder.Property(x => x.Purchase)
               .HasPrecision(18, 2)
               .HasColumnType("decimal")
               .IsRequired();

        builder.Property(x => x.LastDiv)
               .HasPrecision(18, 2)
               .HasColumnType("decimal")
               .IsRequired();

        builder.Property(x => x.Industry)
               .HasColumnType("varchar")
               .IsRequired();

        builder.Property(x => x.MarketCap)
               .HasColumnType("bigint")
               .IsRequired();

        builder.HasMany(x => x.Comments)
                .WithOne(c => c.Stock)
                .HasForeignKey(c => c.StockId)
                .OnDelete(DeleteBehavior.NoAction); 

        builder.HasMany(x => x.Portfolios)
               .WithOne(p => p.Stock)
               .HasForeignKey(p => p.StockId)
               .OnDelete(DeleteBehavior.NoAction);

        base.Configure(builder);
    }
}
