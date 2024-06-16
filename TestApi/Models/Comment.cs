using TestApi.Models.Common;

namespace TestApi.Models;

public class Comment : BaseAuditableEntity
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int? StockId { get; set; }
    public Stock? Stock { get; set; }
    public string AppUserId { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
}
