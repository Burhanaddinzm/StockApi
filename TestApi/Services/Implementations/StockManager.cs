﻿using TestApi.Dtos.Stock;
using TestApi.Helpers.Query;
using TestApi.Mappers;
using TestApi.Models;
using TestApi.Repositories.Interfaces;
using TestApi.Services.Interfaces;

namespace TestApi.Services.Implementations;

public class StockManager : IStockService
{
    private readonly IStockRepository _stockRepository;

    public StockManager(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<List<Stock>> GetAllStocksAsync(QueryObject? query)
    {
        if (query != null)
        {
            if (query.CompanyName != null && query.Symbol != null)
            {
                return await _stockRepository.GetAllWithOrderAsync(
                    query.SortBy,
                    query.IsDescending,
                    x => x.Symbol.ToLower().Contains(query.Symbol.ToLower().Trim()) &&
                    x.CompanyName.ToLower().Contains(query.CompanyName.ToLower().Trim()),
                    "Comments",
                    "Portfolios");
            }
            else if (query.Symbol != null)
            {
                return await _stockRepository.GetAllWithOrderAsync(
                    query.SortBy,
                    query.IsDescending,
                    x => x.Symbol.ToLower().Contains(query.Symbol.ToLower().Trim()),
                    "Comments",
                    "Portfolios");
            }
            else if (query.CompanyName != null)
            {
                return await _stockRepository.GetAllWithOrderAsync(
                    query.SortBy,
                    query.IsDescending,
                    x => x.CompanyName.ToLower().Contains(query.CompanyName.ToLower().Trim()),
                    "Comments",
                    "Portfolios");
            }
            else
            {
                return await _stockRepository.GetAllWithOrderAsync(
                    query.SortBy,
                    query.IsDescending,
                    null,
                    "Comments",
                    "Portfolios");
            }
        }
        return await _stockRepository.GetAllWithOrderAsync(null, false, null, "Comments", "Portfolios");
    }

    public async Task<Stock?> GetStockAsync(int id)
    {
        return await _stockRepository.GetByIdAsync(id, "Comments", "Portfolios");
    }

    public async Task<Stock> CreateStockAsync(CreateStockDto stockDto)
    {
        var stock = stockDto.ToStockFromCreateDto();
        await _stockRepository.CreateAsync(stock);
        return stock;
    }

    public async Task<Stock?> UpdateStockAsync(int id, UpdateStockDto stockDto)
    {
        var stock = await _stockRepository.GetByIdAsync(id);

        if (stock != null)
        {
            stock.Symbol = stockDto.Symbol;
            stock.CompanyName = stockDto.CompanyName;
            stock.Purchase = stockDto.Purchase;
            stock.LastDiv = stockDto.LastDiv;
            stock.Industry = stockDto.Industry;
            stock.MarketCap = stockDto.MarketCap;

            await _stockRepository.UpdateAsync(stock);
        }
        return stock;
    }

    public async Task DeleteStockAsync(int id)
    {
        await _stockRepository.DeleteAsync(id);
    }

    public async Task<bool> StockExistsAsync(int id)
    {
        return await _stockRepository.IsExistsAsync(id);
    }
}
