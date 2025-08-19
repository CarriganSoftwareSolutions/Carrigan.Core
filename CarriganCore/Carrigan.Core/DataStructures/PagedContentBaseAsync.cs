using Carrigan.Core.Interfaces;

namespace Carrigan.Core.DataStructures;

/// <summary>
/// Asynchronous paging base class.
/// Provides a base class for asynchronous paging functionality.
/// Concrete classes supply the actual async data access and enumeration.
/// </summary>
/// <typeparam name="T">The type of items being paged.</typeparam>
public abstract class PagedContentBaseAsync<T> : IAsyncPagedContent<T>
{
    /// <summary>
    /// The total item count is obtained asynchronously in derived classes.
    /// </summary>
    /// <returns>The total item count, as a async Task.</returns>
    public abstract Task<uint> GetTotalItemCountAsync();

    /// <summary>
    /// The current page number. One origin indexed, because that is how books are numbered.
    /// </summary>
    public virtual uint PageNumber { get; protected set; }

    /// <summary>
    /// The page size.
    /// </summary>
    public virtual uint PageSize { get; protected set; }

    /// <summary>
    /// Total number of pages computed from <see cref="GetTotalItemCountAsync"/>.
    /// </summary>
    /// <returns>An integer representing the total page count.</returns>
    public virtual async Task<uint> GetPageCountAsync()
    {
        uint totalCount = await GetTotalItemCountAsync();
        //math hack to computer page size without using floats or doubles.
        return (totalCount + PageSize - 1) / PageSize;
    }

    /// <summary>
    /// Does the current page have a previous page? True if <see cref="PageNumber"/> is greater than 1.
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1u;

    /// <summary>
    /// True if there is a subsequent page
    /// </summary>
    /// <returns></returns>
    public async Task<bool> HasNextPageAsync()
    {
        uint pageCount = await GetPageCountAsync();
        return PageNumber < pageCount;
    }

    /// <summary>
    /// True when the current page, <see cref="PageNumber"/>, is the first page, 1.
    /// </summary>
    public bool IsFirstPage => PageNumber == 1u;

    // For simplicity, we assume IsLastPage is computed synchronously using the current PageNumber and last known PageCount.
    /// <summary>
    /// True when on the last page. 
    /// </summary>
    public bool IsLastPage
    {
        get
        {
            // In a truly asynchronous design, this could be made async.
            uint pageCount = GetPageCountAsync().GetAwaiter().GetResult();
            return PageNumber == pageCount;
        }
    }

    /// <summary>
    /// Returns all items as an <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <returns>Returns all items as an <see cref="IEnumerable{T}"/></returns>
    public abstract Task<IEnumerable<T>> AsEnumerableAsync();

    /// <summary>
    /// Change the new page size.
    /// </summary>
    /// <param name="newPageSize">The new size of the page</param>
    /// <returns>Returns a reference to this class instance.</returns>
    public virtual Task<IAsyncPagedContent<T>> ChangePageSizeAsync(uint newPageSize)
    {
        PageSize = newPageSize;
        PageNumber = 1u;
        return Task.FromResult((IAsyncPagedContent<T>)this);
    }

    /// <summary>
    /// Go to the first page.
    /// </summary>
    /// <returns>Returns a reference to this class instance.</returns>
    public virtual Task<IAsyncPagedContent<T>> FirstPageAsync()
    {
        PageNumber = 1u;
        return Task.FromResult((IAsyncPagedContent<T>)this);
    }

    /// <summary>
    /// Go to the page number indicated. If our of range, correct and go to the first or last page.
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <returns>Returns a reference to this class instance.</returns>
    public virtual async Task<IAsyncPagedContent<T>> GoToPageAsync(uint pageNumber)
    {
        uint pageCount = await GetPageCountAsync();
        if (pageNumber < 1u)
        {
            return await FirstPageAsync();
        }
        else if (pageNumber > pageCount)
        {
            return await LastPageAsync();
        }
        else
        {
            PageNumber = pageNumber;
            return this;
        }
    }

    /// <summary>
    /// Go to the last page.
    /// </summary>
    /// <returns>Returns a reference to this class instance.</returns>
    public virtual async Task<IAsyncPagedContent<T>> LastPageAsync()
    {
        uint pageCount = await GetPageCountAsync();
        PageNumber = pageCount;
        return this;
    }

    /// <summary>
    /// Go to the next page, or last page if already on the last page.
    /// </summary>
    /// <returns>Returns a reference to this class instance.</returns>
    public virtual async Task<IAsyncPagedContent<T>> NextPageAsync() =>
        await GoToPageAsync(PageNumber + 1u);

    /// <summary>
    /// Goto the previous page, or first page if already on the first page.
    /// </summary>
    /// <returns>Returns a reference to this class instance.</returns>
    public virtual async Task<IAsyncPagedContent<T>> PreviousPageAsync() =>
        await GoToPageAsync(PageNumber - 1u);

    /// <summary>
    /// Return a <see cref="IList{T}"/> representing the all items in the underlying data structure.
    /// </summary>
    /// <returns>Return a <see cref="IList{T}"/> representing the all items in the underlying data structure.</returns>
    public virtual async Task<IList<T>> ToListAsync() =>
        [.. await AsEnumerableAsync()];


    /// <summary>
    /// Return a <see cref="ICollection{T}"/> representing the all items in the underlying data structure.
    /// </summary>
    /// <returns>Return a <see cref="ILICollectionist{T}"/> representing the all items in the underlying data structure.</returns>
    public virtual async Task<ICollection<T>> ToCollectionAsync() =>
        [.. await AsEnumerableAsync()];

    /// <summary>
    /// Return an array representing the all items in the underlying data structure.
    /// </summary>
    /// <returns>Return an array representing the all items in the underlying data structure.</returns>
    public virtual async Task<T[]> ToArrayAsync() =>
        [.. await AsEnumerableAsync()];

    /// <summary>
    /// Return a <see cref="IEnumerable{T}"/> representing the current page of items in the underlying data structure.
    /// </summary>
    /// <returns>Return a <see cref="IEnumerable{T}"/> representing the current page of items in the underlying data structure.</returns>
    public abstract Task<IEnumerable<T>> PageAsEnumerableAsync();

    /// <summary>
    /// Return a <see cref="IList{T}"/> representing the current page of items in the underlying data structure.
    /// </summary>
    /// <returns>Return a <see cref="IList{T}"/> representing the current page of items in the underlying data structure.</returns>
    public virtual async Task<IList<T>> PageToListAsync() =>
        [.. await PageAsEnumerableAsync()];

    /// <summary>
    /// Return a <see cref="ICollection{T}"/> representing the current page of items in the underlying data structure.
    /// </summary>
    /// <returns>Return a <see cref="ICollection{T}"/> representing the current page of items in the underlying data structure.</returns>
    public virtual async Task<ICollection<T>> PageToCollectionAsync() =>
        [.. await PageAsEnumerableAsync()];

    /// <summary>
    /// Return an array representing the current page of items in the underlying data structure.
    /// </summary>
    /// <returns>Return an array representing the current page of items in the underlying data structure.</returns>
    public virtual async Task<T[]> PageToArrayAsync() =>
        [.. await PageAsEnumerableAsync()];

    // Navigation Pages method – logic preserved from the synchronous version.

    /// <summary>
    /// Return an array of integers representing every page.
    /// </summary>
    /// <returns></returns>
    public virtual async Task<uint[]> GetNavigationPagesAsync()
    {
        uint pageCount = await GetPageCountAsync();
        List<uint> pages = [];
        uint start, end;

        // Gracefully handles the special case of 0 pages.
        if (pageCount != 0u || PageNumber == 0)
        {
            //special case when less than 10 pages exists

            if (pageCount <= 10u)
            {
                start = 1u;
                end = pageCount;
            }
            //special case when the current page is near the beginning, and there are at least 11 pages

            else if (PageNumber < 6u)
            {
                start = 1u;
                end = 10u;
            }
            //special case when the current page is near the end, and there are at least 11 pages

            else if (pageCount - PageNumber < 5u)
            {
                start = pageCount - 9u;
                end = pageCount;
            }
            //normal logic when their are at least 5 pages on either side of the current page

            else
            {
                start = PageNumber - 5u;
                end = PageNumber + 5u;
            }
            for (uint i = start; i <= end; i++)
                pages.Add(i);

            //prepends the very first element if it is not present
            if (pageCount > 0u && pages.First() != 1u)
                pages.Insert(0, 1u);

            //append the very last element if it is not present
            if (pageCount > 0u && pages.Last() != pageCount)
                pages.Add(pageCount);

            return [.. pages];
        }
        else
        {
            return [];
        }
    }
}
