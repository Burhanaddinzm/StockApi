using TestApi.Models.Common;

namespace TestApi.Models;
public class Stock : BaseAuditableEntity
{
    public string Symbol { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    public string Industry { get; set; } = null!;
    public long MarketCap { get; set; }

    public List<Comment> Comments { get; set; }
    public List<Portfolio> Portfolios { get; set; }
    public Stock()
    {
        Comments = new List<Comment>();
        Portfolios = new List<Portfolio>();
    }
}
