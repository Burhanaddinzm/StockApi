using TestApi.Dtos.Comment;
using TestApi.Models;

namespace TestApi.Dtos.Stock;

/// <summary>
/// Represents a data transfer object (DTO) for a Stock.
/// </summary>
public class StockDto
{
    /// <summary>
    /// Gets or sets the identifier of the stock.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the symbol of the stock.
    /// </summary>
    public string Symbol { get; set; } = null!;

    /// <summary>
    /// Gets or sets the company name of the stock.
    /// </summary>
    public string CompanyName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the purchase price of the stock.
    /// </summary>
    public decimal Purchase { get; set; }

    /// <summary>
    /// Gets or sets the last dividend of the stock.
    /// </summary>
    public decimal LastDiv { get; set; }

    /// <summary>
    /// Gets or sets the industry of the stock.
    /// </summary>
    public string Industry { get; set; } = null!;

    /// <summary>
    /// Gets or sets the market capitalization of the stock.
    /// </summary>
    public long MarketCap { get; set; }

    /// <summary>
    /// Gets or sets the comments associated with the stock.
    /// </summary>
    public List<CommentDto> Comments { get; set; } = null!;

    /// <summary>
    /// Gets or sets the portfolios associated with the stock.
    /// </summary>
    public List<Portfolio> Portfolios { get; set; } = null!;
}
