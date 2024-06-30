using TestApi.Models.Common;

namespace TestApi.Models;

/// <summary>
/// Represents a comment on a stock.
/// </summary>
/// <remarks>This class inherits from <see cref="BaseAuditableEntity"/>.</remarks>
public class Comment : BaseAuditableEntity
{
    /// <summary>
    /// Gets or sets the title of the comment.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the content of the comment.
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Gets or sets the ID of the associated stock.
    /// </summary>
    public int? StockId { get; set; }

    /// <summary>
    /// Gets or sets the associated stock.
    /// </summary>
    public Stock? Stock { get; set; }

    /// <summary>
    /// Gets or sets the ID of the associated user.
    /// </summary>
    public string AppUserId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the associated user.
    /// </summary>
    public AppUser AppUser { get; set; } = null!;
}
