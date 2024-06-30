using TestApi.Models.Common;

namespace TestApi.Models;

/// <summary>
/// Represents a user's portfolio of stocks.
/// </summary>
/// <remarks>This class inherits from <see cref="BaseAuditableEntity"/>.</remarks>
public class Portfolio : BaseAuditableEntity
{
    /// <summary>
    /// Gets or sets the ID of the stock in the portfolio.
    /// </summary>
    public int StockId { get; set; }

    /// <summary>
    /// Gets or sets the stock in the portfolio.
    /// </summary>
    public Stock Stock { get; set; } = null!;

    /// <summary>
    /// Gets or sets the ID of the user who owns the portfolio.
    /// </summary>
    public string AppUserId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who owns the portfolio.
    /// </summary>
    public AppUser AppUser { get; set; } = null!;
}
