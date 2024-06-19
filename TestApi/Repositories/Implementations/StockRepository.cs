using TestApi.Data;
using TestApi.Models;
using TestApi.Repositories.Interfaces;

namespace TestApi.Repositories.Implementations;

public class StockRepository : Repository<Stock>, IStockRepository
{
    public StockRepository(AppDbContext context) : base(context)
    {
    }
}
