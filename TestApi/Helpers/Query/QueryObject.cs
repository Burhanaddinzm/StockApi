namespace TestApi.Helpers.Query;

/// <summary>
/// Represents a query object for filtering and sorting stock information.
/// </summary>
public class QueryObject
{
    /// <summary>
    /// The symbol of the stock.
    /// </summary>
    public string? Symbol { get; set; } = null;

    /// <summary>
    /// The name of the company associated with the stock.
    /// </summary>
    public string? CompanyName { get; set; } = null;

    /// <summary>
    /// The field to sort the results by.
    /// </summary>
    public string? SortBy { get; set; } = null;

    /// <summary>
    /// Determines whether to sort the results in descending order.
    /// </summary>
    public bool IsDescending { get; set; } = false;

    /// <summary>
    /// The page number of the results.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// The number of items per page.
    /// </summary>
    public int PageSize { get; set; } = 6;
}
