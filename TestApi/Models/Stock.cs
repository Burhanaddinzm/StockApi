using TestApi.Models.Common;

namespace TestApi.Models;
/// <summary>
/// Represents a stock.
/// </summary>
/// <remarks>This class inherits from <see cref="BaseAuditableEntity"/>.</remarks>
public class Stock : BaseAuditableEntity
{
    /// <summary>
    /// The symbol of the stock.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public string Symbol { get; set; } = null!;

    /// <summary>
    /// The name of the company that issued the stock.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public string CompanyName { get; set; } = null!;

    /// <summary>
    /// The price that the stock was purchased at.
    /// </summary>
    public decimal Purchase { get; set; }

    /// <summary>
    /// The last dividend that was paid out on the stock.
    /// </summary>
    public decimal LastDiv { get; set; }

    /// <summary>
    /// The industry that the company that issued the stock is in.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public string Industry { get; set; } = null!;

    /// <summary>
    /// The market capitalization of the stock.
    /// </summary>
    public long MarketCap { get; set; }

    /// <summary>
    /// The list of comments on the stock.
    /// </summary>
    /// <remarks>This property is not required.</remarks>
    public List<Comment> Comments { get; set; }

    /// <summary>
    /// The list of portfolios that the stock is in.
    /// </summary>
    /// <remarks>This property is not required.</remarks>
    public List<Portfolio> Portfolios { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Stock"/> class.
    /// </summary>
    public Stock()
    {
        Comments = new List<Comment>();
        Portfolios = new List<Portfolio>();
    }
}


