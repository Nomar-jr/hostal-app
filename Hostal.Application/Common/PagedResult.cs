namespace Hostal.Application.Common;

/// <summary>
/// Generic class that represents a paged result of a query.
/// </summary>
/// <typeparam name="T">Type of the items in the paged collection.</typeparam>
public class PagedResult<T>
{
    public PagedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalItemsCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        ItemsFrom = totalCount == 0 ? 0 : pageSize * (pageNumber - 1) + 1;
        
        var calculatedItemsTo = ItemsFrom + pageSize - 1;
        ItemsTo = calculatedItemsTo > totalCount ? totalCount : calculatedItemsTo;
        
        // Si no hay items, ItemsTo debe ser 0
        if (totalCount == 0) ItemsTo = 0;
    }
    
    public IEnumerable<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int TotalItemsCount { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
    
    // Propiedades adicionales útiles
    public bool HasPreviousPage => TotalPages > 1 && ItemsFrom > 1;
    public bool HasNextPage => TotalPages > 1 && ItemsTo < TotalItemsCount;
}