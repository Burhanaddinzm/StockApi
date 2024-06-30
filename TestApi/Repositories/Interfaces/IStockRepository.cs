using System.Linq.Expressions;
using TestApi.Models;

namespace TestApi.Repositories.Interfaces;

/// <summary>
/// Represents a repository for <see cref="Stock"/> entities that provides methods for querying and filtering the data.
/// </summary>
public interface IStockRepository : IRepository<Stock>
{
    /// <summary>
    /// Retrieves a list of <see cref="Stock"/> entities based on the provided query parameters.
    /// </summary>
    /// <param name="sortBy">The property to sort the results by.</param>
    /// <param name="isDescending">Indicates whether the sorting should be done in descending order.</param>
    /// <param name="page">The page number of the results.</param>
    /// <param name="pageSize">The number of results per page.</param>
    /// <param name="expression">An optional expression to filter the results by.</param>
    /// <param name="includes">An optional array of navigation properties to include in the results.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of <see cref="Stock"/> entities.</returns>
    Task<List<Stock>> GetAllWithQueryParamsAsync(
        string? sortBy,
        bool isDescending,
        int page,
        int pageSize,
        Expression<Func<Stock, bool>>? expression = null,
        params string[] includes);
}
