using TestApi.Dtos.Stock;
using TestApi.Models;

namespace TestApi.Mappers;


    /// <summary>
    /// Extension methods to map between Stock and StockDto objects.
    /// </summary>
    public static class StockMappers
    {
        /// <summary>
        /// Maps a Stock object to a StockDto object.
        /// </summary>
        /// <param name="stock">The Stock object to map from.</param>
        /// <returns>The mapped StockDto object.</returns>
        public static StockDto ToStockDto(this Stock stock)
        {
            return new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Comments = stock.Comments.Select(x => x.ToCommentDto()).ToList(),
                Portfolios = stock.Portfolios,
            };
        }

        /// <summary>
        /// Maps a CreateStockDto object to a Stock object.
        /// </summary>
        /// <param name="stockDto">The CreateStockDto object to map from.</param>
        /// <returns>The mapped Stock object.</returns>
        public static Stock ToStockFromCreateDto(this CreateStockDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
            };
        }
    }
