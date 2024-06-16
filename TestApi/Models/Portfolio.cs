using TestApi.Models.Common;

namespace TestApi.Models;

public class Portfolio : BaseAuditableEntity
{
    public int StockId { get; set; }
    public Stock Stock { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
}