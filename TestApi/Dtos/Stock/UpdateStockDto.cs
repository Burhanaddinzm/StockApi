using System.ComponentModel.DataAnnotations;

namespace TestApi.Dtos.Stock;

/// <summary>
/// A data transfer object for updating a stock.
/// </summary>
public class UpdateStockDto
{
    /// <summary>
    /// The symbol of the stock to update.
    /// </summary>
    [Required]
    [MaxLength(10, ErrorMessage = "Symbol can't be over 10 characters long.")]
    public string Symbol { get; set; } = null!;

    /// <summary>
    /// The name of the company associated with the stock.
    /// </summary>
    [Required]
    [MaxLength(10, ErrorMessage = "Company Name can't be over 10 characters long.")]
    public string CompanyName { get; set; } = null!;

    /// <summary>
    /// The purchase price of the stock.
    /// </summary>
    [Required]
    [Range(1, 1000000000000)]
    public decimal Purchase { get; set; }

    /// <summary>
    /// The last dividend paid by the company.
    /// </summary>
    [Required]
    [Range(0.001, 100)]
    public decimal LastDiv { get; set; }

    /// <summary>
    /// The industry the company belongs to.
    /// </summary>
    [Required]
    [MaxLength(10, ErrorMessage = "Industry can't be over 10 characters long.")]
    public string Industry { get; set; } = null!;

    /// <summary>
    /// The market capitalization of the company.
    /// </summary>
    [Required]
    [Range(1, 5000000000000)]
    public long MarketCap { get; set; }
}
