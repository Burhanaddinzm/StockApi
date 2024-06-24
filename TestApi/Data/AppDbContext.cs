using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TestApi.Models;
using TestApi.Models.Common;

namespace TestApi.Data;

public class AppDbContext : IdentityDbContext<AppUser>
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
        // Add configurations from assembly.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Seed roles.
        List<IdentityRole> roles = new List<IdentityRole>()
        {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            }
        };
        modelBuilder.Entity<IdentityRole>().HasData(roles);

        base.OnModelCreating(modelBuilder);
    }
}
