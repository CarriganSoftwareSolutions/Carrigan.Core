namespace Carrigan.Core.Interfaces;

/// <summary>
/// Represents a paginated list with asynchronous navigation and conversion capabilities.
/// </summary>
/// <typeparam name="T">The type of the items in the paginated list.</typeparam>
public interface IAsyncPagedContent<T>
{
    #region Pagination Metadata

    /// <summary>
    /// Asynchronously gets the total number of items available in the full collection.
    /// </summary>
    Task<uint> GetTotalItemCountAsync();

    /// <summary>
    /// Gets the current page number (typically 1-based).
    /// </summary>
    uint PageNumber { get; }

    /// <summary>
    /// Gets the number of items per page.
    /// </summary>
    uint PageSize { get; }

    /// <summary>
    /// Asynchronously gets the total number of pages.
    /// </summary>
    Task<uint> GetPageCountAsync();

    /// <summary>
    /// Gets a value indicating whether there is a previous page.
    /// </summary>
    bool HasPreviousPage { get; }

    /// <summary>
    /// Asynchronously gets a value indicating whether there is a next page.
    /// </summary>
    Task<bool> HasNextPageAsync();

    /// <summary>
    /// Gets a value indicating whether the current page is the first page.
    /// </summary>
    bool IsFirstPage { get; }

    /// <summary>
    /// Gets a value indicating whether the current page is the last page.
    /// </summary>
    bool IsLastPage { get; }

    #endregion

    #region Fluent Navigation Methods

    /// <summary>
    /// Asynchronously changes the page size to the specified value, updates pagination state, and returns the updated paged list.
    /// </summary>
    /// <param name="newPageSize">The new page size.</param>
    Task<IAsyncPagedContent<T>> ChangePageSizeAsync(uint newPageSize);

    /// <summary>
    /// Asynchronously advances to the next page (if available) and returns the updated paged list.
    /// </summary>
    Task<IAsyncPagedContent<T>> NextPageAsync();

    /// <summary>
    /// Asynchronously moves to the previous page (if available) and returns the updated paged list.
    /// </summary>
    Task<IAsyncPagedContent<T>> PreviousPageAsync();

    /// <summary>
    /// Asynchronously navigates to the first page and returns the updated paged list.
    /// </summary>
    Task<IAsyncPagedContent<T>> FirstPageAsync();

    /// <summary>
    /// Asynchronously navigates to the last page and returns the updated paged list.
    /// </summary>
    Task<IAsyncPagedContent<T>> LastPageAsync();

    /// <summary>
    /// Asynchronously navigates to the specified page number and returns the updated paged list.
    /// </summary>
    /// <param name="pageNumber">The page number to navigate to.</param>
    Task<IAsyncPagedContent<T>> GoToPageAsync(uint pageNumber);

    #endregion

    #region Entire Collection Conversion Methods

    /// <summary>
    /// Asynchronously returns the entire collection as an IEnumerable.
    /// </summary>
    Task<IEnumerable<T>> AsEnumerableAsync();

    /// <summary>
    /// Asynchronously returns the entire collection as an IList.
    /// </summary>
    Task<IList<T>> ToListAsync();

    /// <summary>
    /// Asynchronously returns the entire collection as an ICollection.
    /// </summary>
    Task<ICollection<T>> ToCollectionAsync();

    /// <summary>
    /// Asynchronously returns the entire collection as an array.
    /// </summary>
    Task<T[]> ToArrayAsync();

    #endregion

    #region Current Page Conversion Methods

    /// <summary>
    /// Asynchronously returns the current page as an IEnumerable.
    /// </summary>
    Task<IEnumerable<T>> PageAsEnumerableAsync();

    /// <summary>
    /// Asynchronously returns the current page as an IList.
    /// </summary>
    Task<IList<T>> PageToListAsync();

    /// <summary>
    /// Asynchronously returns the current page as an ICollection.
    /// </summary>
    Task<ICollection<T>> PageToCollectionAsync();

    /// <summary>
    /// Asynchronously returns the current page as an array.
    /// </summary>
    Task<T[]> PageToArrayAsync();

    #endregion

    /// <summary>
    /// Asynchronously gets an array of page numbers to be displayed in a pagination control based on the current page and total page count.
    /// </summary>
    Task<uint[]> GetNavigationPagesAsync();
}
