using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TestApi.Models;
using TestApi.Models.Common;

namespace TestApi.Data;

public class AppDbContext : DbContext
{
    private readonly IHttpContextAccessor _accessor;

    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor accessor)
        : base(options)
    {
        _accessor = accessor;
    }

    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Portfolio> Portfolios => Set<Portfolio>();
    public DbSet<Stock> Stocks => Set<Stock>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseAuditableEntity>();
        var currentUserName = _accessor.HttpContext?.User.Identity?.Name ?? "Anonymous";
        var currentIpAddress = _accessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = currentUserName;
                    entry.Entity.CreatedAt = DateTime.UtcNow.AddHours(4);
                    entry.Entity.IP = currentIpAddress;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = currentUserName;
                    entry.Entity.ModifiedAt = DateTime.UtcNow.AddHours(4);
                    entry.Entity.IP = currentIpAddress;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
