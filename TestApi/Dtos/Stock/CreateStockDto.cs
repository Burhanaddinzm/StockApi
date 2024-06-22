using System.ComponentModel.DataAnnotations;

namespace TestApi.Dtos.Stock;

public class CreateStockDto
{
    [Required]
    [MaxLength(10, ErrorMessage = "Symbol can't be over 10 characters long.")]
    public string Symbol { get; set; } = null!;
    [Required]
    [MaxLength(10, ErrorMessage = "Company Name can't be over 10 characters long.")]
    public string CompanyName { get; set; } = null!;
    [Required]
    [Range(1, 1000000000000)]
    public decimal Purchase { get; set; }
    [Required]
    [Range(0.001, 100)]
    public decimal LastDiv { get; set; }
    [Required]
    [MaxLength(10, ErrorMessage = "Industry can't be over 10 characters long.")]
    public string Industry { get; set; } = null!;
    [Required]
    [Range(1, 5000000000000)]
    public long MarketCap { get; set; }
}
