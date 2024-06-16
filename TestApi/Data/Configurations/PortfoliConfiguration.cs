using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApi.Data.Configurations.Common;
using TestApi.Models;

namespace TestApi.Data.Configurations;

public class PortfoliConfiguration : BaseConfiguration<Portfolio>
{
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
