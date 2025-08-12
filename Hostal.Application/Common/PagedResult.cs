namespace Hostal.Application.Common;

/// <summary>
/// Generic class that represents a paged result of a query.
/// </summary>
/// <typeparam name="T">Type of the items in the paged collection.</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// Initializes a new instance of the PagedResult class.
    /// </summary>
    /// <param name="items">Collection of items in the current page.</param>
    /// <param name="totalCount">Total number of items without pagination.</param>
    /// <param name="pageNumber">Current page number.</param>
    /// <param name="pageSize">Number of items per page.</param>
    public PagedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalItemsCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        ItemsFrom = pageSize * (pageNumber - 1) + 1;
        ItemsTo = ItemsFrom + pageSize -1;
    }
    
    /// <summary>Gets or sets the items in the current page.</summary>
    public IEnumerable<T> Items { get; set; }
    
    /// <summary>Gets or sets the total number of pages available.</summary>
    public int TotalPages { get; set; }
    
    /// <summary>Gets or sets the total number of items across all results.</summary>
    public int TotalItemsCount { get; set; }
    
    /// <summary>Gets or sets the index of the first item in the current page within the complete result set.</summary>
    public int ItemsFrom { get; set; }
    
    /// <summary>Gets or sets the index of the last item in the current page within the complete result set.</summary>
    public int ItemsTo { get; set; } 
}