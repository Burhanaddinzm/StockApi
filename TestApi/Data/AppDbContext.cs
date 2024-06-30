using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;
using TestApi.Models;
using TestApi.Models.Common;

namespace TestApi.Data;


/// <summary>
/// The main database context for the application.
/// Handles the creation and configuration of the database schema.
/// </summary>
public class AppDbContext : IdentityDbContext<AppUser>
{
    private readonly IHttpContextAccessor _accessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    /// <param name="accessor">The HTTP context accessor.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor accessor)
        : base(options)
    {
        _accessor = accessor;
    }

    /// <summary>
    /// Gets the comments database set.
    /// </summary>
    public DbSet<Comment> Comments => Set<Comment>();

    /// <summary>
    /// Gets the portfolios database set.
    /// </summary>
    public DbSet<Portfolio> Portfolios => Set<Portfolio>();

    /// <summary>
    /// Gets the stocks database set.
    /// </summary>
    public DbSet<Stock> Stocks => Set<Stock>();

    /// <summary>
    /// Overrides the SaveChangesAsync method to set audit fields.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The task representing the asynchronous operation.</returns>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Set audit fields.
        var entries = ChangeTracker.Entries<BaseAuditableEntity>();
        var currentUserName = _accessor.HttpContext?.User.FindFirst(ClaimTypes.GivenName)?.Value ?? "Anonymous";
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

    /// <summary>
    /// Overrides the OnModelCreating method to configure the database schema.
    /// Adds configurations from the executing assembly and seeds the roles.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
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
