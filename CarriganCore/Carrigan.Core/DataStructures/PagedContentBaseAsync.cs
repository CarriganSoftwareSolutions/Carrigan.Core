using Carrigan.Core.Interfaces;

namespace Carrigan.Core.DataStructures;

/// <summary>
/// Provides a base class for asynchronous paging functionality.
/// Implements common navigation and conversion methods while leaving data retrieval abstract.
/// </summary>
/// <typeparam name="T">The type of items being paged.</typeparam>
public abstract class PagedContentBaseAsync<T> : IAsyncPagedContent<T>
{
    // The total item count is obtained asynchronously in derived classes.
    public abstract Task<uint> GetTotalItemCountAsync();

    public virtual uint PageNumber { get; protected set; }

    public virtual uint PageSize { get; protected set; }

    public virtual async Task<uint> GetPageCountAsync()
    {
        uint totalCount = await GetTotalItemCountAsync();
        //math hack to computer page size without using floats or doubles.
        return (totalCount + PageSize - 1) / PageSize;
    }

    public bool HasPreviousPage => PageNumber > 1u;

    public async Task<bool> HasNextPageAsync()
    {
        uint pageCount = await GetPageCountAsync();
        return PageNumber < pageCount;
    }

    public bool IsFirstPage => PageNumber == 1u;

    // For simplicity, we assume IsLastPage is computed synchronously using the current PageNumber and last known PageCount.
    public bool IsLastPage
    {
        get
        {
            // In a truly asynchronous design, this could be made async.
            uint pageCount = GetPageCountAsync().GetAwaiter().GetResult();
            return PageNumber == pageCount;
        }
    }

    public abstract Task<IEnumerable<T>> AsEnumerableAsync();

    // Fluent Navigation Methods
    public virtual Task<IAsyncPagedContent<T>> ChangePageSizeAsync(uint newPageSize)
    {
        PageSize = newPageSize;
        PageNumber = 1u;
        return Task.FromResult((IAsyncPagedContent<T>)this);
    }

    public virtual Task<IAsyncPagedContent<T>> FirstPageAsync()
    {
        PageNumber = 1u;
        return Task.FromResult((IAsyncPagedContent<T>)this);
    }

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

    public virtual async Task<IAsyncPagedContent<T>> LastPageAsync()
    {
        uint pageCount = await GetPageCountAsync();
        PageNumber = pageCount;
        return this;
    }

    public virtual async Task<IAsyncPagedContent<T>> NextPageAsync() =>
        await GoToPageAsync(PageNumber + 1u);

    public virtual async Task<IAsyncPagedContent<T>> PreviousPageAsync() =>
        await GoToPageAsync(PageNumber - 1u);

    public virtual async Task<IList<T>> ToListAsync() =>
        [.. await AsEnumerableAsync()];

    public virtual async Task<ICollection<T>> ToCollectionAsync() =>
        [.. await AsEnumerableAsync()];

    public virtual async Task<T[]> ToArrayAsync() =>
        [.. await AsEnumerableAsync()];

    // Current Page Conversion Methods
    public abstract Task<IEnumerable<T>> PageAsEnumerableAsync();

    public virtual async Task<IList<T>> PageToListAsync() =>
        [.. await PageAsEnumerableAsync()];

    public virtual async Task<ICollection<T>> PageToCollectionAsync() =>
        [.. await PageAsEnumerableAsync()];

    public virtual async Task<T[]> PageToArrayAsync() =>
        [.. await PageAsEnumerableAsync()];

    // Navigation Pages method – logic preserved from the synchronous version.
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
