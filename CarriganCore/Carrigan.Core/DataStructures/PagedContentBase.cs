using Carrigan.Core.Interfaces;

namespace Carrigan.Core.DataStructures;

public abstract class PagedContentBase<T> : IPagedContent<T>
{
    public abstract uint TotalItemCount { get; }

    public virtual uint PageNumber { get; protected set; }

    public virtual uint PageSize { get; protected set; }

    public virtual uint PageCount => (TotalItemCount + PageSize - 1) / PageSize; //math hack to computer page size without using floats or doubles.

    public bool HasPreviousPage => PageNumber > 1u;

    public bool HasNextPage => PageNumber < PageCount;

    public bool IsFirstPage => PageNumber == 1u;

    public bool IsLastPage => PageNumber == PageCount;

    public abstract IEnumerable<T> AsEnumerable();

    public IPagedContent<T> ChangePageSize(uint newPageSize)
    {
        PageSize = newPageSize;
        PageNumber = 1u;
        return this;
    }

    public IPagedContent<T> FirstPage()
    {
        PageNumber = 1u;
        return this;
    }

    public IPagedContent<T> GoToPage(uint pageNumber)
    {
        if (pageNumber < 1u)
        {
            return FirstPage();
        }
        else if (pageNumber > PageCount)
        {
            return LastPage();
        }
        else
        {
            PageNumber = pageNumber;
            return this;
        }
    }

    public IPagedContent<T> LastPage()
    {
        PageNumber = PageCount;
        return this;
    }

    public IPagedContent<T> NextPage() =>
        GoToPage(PageNumber + 1u);
    public IPagedContent<T> PreviousPage() =>
        GoToPage(PageNumber - 1);

    public IList<T> ToList() =>
        [.. AsEnumerable()];
    public ICollection<T> ToCollection() =>
        ToList();

    public T[] ToArray() =>
        [.. AsEnumerable()];

    public abstract IEnumerable<T> PageAsEnumerable();
    public IList<T> PageToList() =>
        [.. PageAsEnumerable()];

    public ICollection<T> PageToCollection() =>
        [.. PageAsEnumerable()];
    public T[] PageToArray() =>
        [.. PageAsEnumerable()];

    /// <summary>
    /// Gets an array of page numbers to be displayed in a pagination control based on the current page and total page count.
    /// </summary>
    /// <remarks>
    /// This property attempts to provide a range of 11 page numbers with the current page centered in the middle.
    /// If the current page is too close to the beginning, it returns the first 10 pages;
    /// if it’s too close to the end, it returns the last 10 pages.
    /// After this logic is accomplished, the very first and very last pages are added to the collection,
    /// this serves as the fast forward to end and rewind to start functionality.
    /// 
    /// </remarks>
    public uint[] NavigationPages
    {
        get
        {
            List<uint> pages = [];
            uint start;
            uint end;

            //gracefully handles special case of 0 pages
            if (PageCount != 0u || PageNumber == 0)
            {
                //special case when less than 10 pages exists

                if (PageCount <= 10u)
                {
                    start = 1u;
                    end = PageCount;
                }
                //special case when the current page is near the beginning, and there are at least 11 pages
                else if (PageNumber < 6u)
                {
                    start = 1u;
                    end = 10u;
                }
                //special case when the current page is near the end, and there are at least 11 pages
                else if (PageCount - PageNumber < 5u)
                {
                    start = PageCount - 9u;
                    end = PageCount;
                }
                //normal logic when their are at least 5 pages on either side of the current page
                else
                {
                    start = PageNumber - 5u;
                    end = PageNumber + 5u;
                }


                for (uint i = start; i <= end; i++)
                    pages.Add(i);

                //prepend the very first element if it is not present
                if (PageCount > 0u && pages.First() != 1u)
                    pages.Insert(0, 1u);

                //append the very last element if it is not present
                if (PageCount > 0 && pages.Last() != PageCount)
                    pages.Add(PageCount);

                return [.. pages];
            }
            else
            {
                return [];
            }
        }
    }
}
