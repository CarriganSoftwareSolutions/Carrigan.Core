namespace Carrigan.Core.Interfaces;

/// <summary>
/// Represents a paginated list with navigation and conversion capabilities.
/// </summary>
public interface IPagedContent<T>
{
    #region Pagination Metadata

    /// <summary>
    /// Gets the total number of items available in the full collection.
    /// </summary>
    uint TotalItemCount { get; }

    /// <summary>
    /// Gets the current page number (typically 1-based).
    /// </summary>
    uint PageNumber { get; }

    /// <summary>
    /// Gets the number of items per page.
    /// </summary>
    uint PageSize { get; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    uint PageCount { get; }

    /// <summary>
    /// Gets a value indicating whether there is a previous page.
    /// </summary>
    bool HasPreviousPage { get; }

    /// <summary>
    /// Gets a value indicating whether there is a next page.
    /// </summary>
    bool HasNextPage { get; }

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
    /// Changes the page size to the specified value, updates pagination state, and returns the updated paged list.
    /// </summary>
    /// <param name="newPageSize">The new page size.</param>
    /// <returns>The updated <see cref="IPagedContent{T}"/> instance.</returns>
    IPagedContent<T> ChangePageSize(uint newPageSize);

    /// <summary>
    /// Advances to the next page (if available) and returns the updated paged list.
    /// </summary>
    /// <returns>The updated <see cref="IPagedContent{T}"/> instance.</returns>
    IPagedContent<T> NextPage();

    /// <summary>
    /// Moves to the previous page (if available) and returns the updated paged list.
    /// </summary>
    /// <returns>The updated <see cref="IPagedContent{T}"/> instance.</returns>
    IPagedContent<T> PreviousPage();

    /// <summary>
    /// Navigates to the first page and returns the updated paged list.
    /// </summary>
    /// <returns>The updated <see cref="IPagedContent{T}"/> instance.</returns>
    IPagedContent<T> FirstPage();

    /// <summary>
    /// Navigates to the last page and returns the updated paged list.
    /// </summary>
    /// <returns>The updated <see cref="IPagedContent{T}"/> instance.</returns>
    IPagedContent<T> LastPage();

    /// <summary>
    /// Navigates to the specified page number and returns the updated paged list.
    /// </summary>
    /// <param name="pageNumber">The page number to navigate to.</param>
    /// <returns>The updated <see cref="IPagedContent{T}"/> instance.</returns>
    IPagedContent<T> GoToPage(uint pageNumber);

    #endregion

    #region Entire Collection Conversion Methods

    /// <summary>
    /// Returns the entire collection as an <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all items.</returns>
    IEnumerable<T> AsEnumerable();

    /// <summary>
    /// Returns the entire collection as an <see cref="IList{T}"/>.
    /// </summary>
    /// <returns>An <see cref="IList{T}"/> containing all items.</returns>
    IList<T> ToList();

    /// <summary>
    /// Returns the entire collection as an <see cref="ICollection{T}"/>.
    /// </summary>
    /// <returns>An <see cref="ICollection{T}"/> containing all items.</returns>
    ICollection<T> ToCollection();

    /// <summary>
    /// Returns the entire collection as an array.
    /// </summary>
    /// <returns>An array containing all items.</returns>
    T[] ToArray();

    #endregion

    #region Current Page Conversion Methods

    /// <summary>
    /// Returns the current page as an <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}"/> containing the items of the current page.</returns>
    IEnumerable<T> PageAsEnumerable();

    /// <summary>
    /// Returns the current page as an <see cref="IList{T}"/>.
    /// </summary>
    /// <returns>An <see cref="IList{T}"/> containing the items of the current page.</returns>
    IList<T> PageToList();

    /// <summary>
    /// Returns the current page as an <see cref="ICollection{T}"/>.
    /// </summary>
    /// <returns>An <see cref="ICollection{T}"/> containing the items of the current page.</returns>
    ICollection<T> PageToCollection();

    /// <summary>
    /// Returns the current page as an array.
    /// </summary>
    /// <returns>An array containing the items of the current page.</returns>
    T[] PageToArray();

    #endregion
    public uint[] NavigationPages { get; }
}
