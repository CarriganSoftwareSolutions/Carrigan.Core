using Carrigan.Core.Interfaces;

namespace Carrigan.Core.DataStructures;

/// <summary>
/// An abstract base class for paged data content. Implements the <see cref="IPagedContent{T}"/> interface.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class PagedContentBase<T> : IPagedContent<T>
{
    /// <summary>
    /// Abstract implementation for the getter for count of the total number of items represented in the data structure.
    /// </summary>
    public abstract uint TotalItemCount { get; }

    /// <summary>
    /// Virtual implementation for the public getter for the current page number and the protected setter.
    /// </summary>
    public virtual uint PageNumber { get; protected set; }

    /// <summary>
    /// Virtual implementation for the public getter for the page size and the protected setter.
    /// </summary>
    public virtual uint PageSize { get; protected set; }

    /// <summary>
    /// Virtual implementation for the public getter for the page count, no setter.
    /// </summary>
    public virtual uint PageCount => (TotalItemCount + PageSize - 1) / PageSize; //math hack to computer page size without using floats or doubles.

    /// <summary>
    /// Public getter for the page count, no setter.
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1u;

    /// <summary>
    /// Public getter to determine if there is a next page.
    /// </summary>
    public bool HasNextPage => PageNumber < PageCount;

    /// <summary>
    /// Public getter to determine if the current page is the first page.
    /// </summary>
    public bool IsFirstPage => PageNumber == 1u;

    /// <summary>
    /// Public getter to determine if the current page is the last page.
    /// </summary>
    public bool IsLastPage => PageNumber == PageCount;

    /// <summary>
    /// Returns a <see cref="IEnumerable{T}"/> that represents the underlying data.
    /// </summary>
    /// <returns>Returns a <see cref="IEnumerable{T}"/> that represents the underlying data.</returns>
    public abstract IEnumerable<T> AsEnumerable();

    /// <summary>
    /// Change the page size and return the current object as an <see cref="IPagedContent{T}"/> interface.
    /// </summary>
    /// <param name="newPageSize">the new size of the page</param>
    /// <returns>Returns the updated instance of the current object as an <see cref="IPagedContent{T}"/> interface.</returns>
    public IPagedContent<T> ChangePageSize(uint newPageSize)
    {
        PageSize = newPageSize;
        PageNumber = 1u;
        return this;
    }

    /// <summary>
    /// Change the page number to the first page and returns the current object as an <see cref="IPagedContent{T}"/> interface.
    /// </summary>
    /// <returns>Returns the updated instance of the current object as an <see cref="IPagedContent{T}"/> interface.</returns>
    public IPagedContent<T> FirstPage()
    {
        PageNumber = 1u;
        return this;
    }

    /// <summary>
    /// Change the page number to the indicated page number and returns the current object as an <see cref="IPagedContent{T}"/> interface.
    /// If the new page number is out of bounds, then the page number will be set to the value of the closet valid page number.
    /// Ex: Page zero and negative page numbers will be set to the first page, and pages numbers exceeding the last page will get set to the last page.
    /// </summary>
    /// <param name="pageNumber">the new current page number</param>
    /// <returns>Returns the updated instance of the current object as an <see cref="IPagedContent{T}"/> interface.</returns>
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

    /// <summary>
    /// Change the page number to the last page and returns the current object as an <see cref="IPagedContent{T}"/> interface.
    /// </summary>
    /// <returns>Returns the updated instance of the current object as an <see cref="IPagedContent{T}"/> interface.</returns>
    public IPagedContent<T> LastPage()
    {
        PageNumber = PageCount;
        return this;
    }

    /// <summary>
    /// Attempts to change the page to the next page and returns the current object as an <see cref="IPagedContent{T}"/> interface.
    /// Pages numbers exceeding the last page will get set to the last page.
    /// </summary>
    /// <returns>Returns the updated instance of the current object as an <see cref="IPagedContent{T}"/> interface.</returns>
    public IPagedContent<T> NextPage() =>
        GoToPage(PageNumber + 1u);

    /// <summary>
    /// Change the page number to the last page and returns the current object as an <see cref="IPagedContent{T}"/> interface.
    /// Page zero and negative page numbers will be set to the first page.
    /// </summary>
    /// <returns>Returns the updated instance of the current object as an <see cref="IPagedContent{T}"/> interface.</returns>
    public IPagedContent<T> PreviousPage() =>
        GoToPage(PageNumber - 1);

    /// <summary>
    /// Returns the current object cast as an <see cref="IList{T}"/>
    /// </summary>
    /// <returns>Returns the current object cast as an <see cref="IList{T}"/></returns>
    public IList<T> ToList() =>
        [.. AsEnumerable()];

    /// <summary>
    /// Returns the current object cast as an <see cref="ICollection{T}"/>
    /// </summary>
    /// <returns>Returns the current object cast as an <see cref="ICollection{T}"/></returns>
    public ICollection<T> ToCollection() =>
        ToList();

    /// <summary>
    /// Returns the current object cast as an array
    /// </summary>
    /// <returns>Returns the current object cast as an array.</returns>
    public T[] ToArray() =>
        [.. AsEnumerable()];


    /// <summary>
    /// Abstract implementation that returns the current page of data represented by an <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <returns>Returns the current page of data represented by an <see cref="IEnumerable{T}"/></returns>
    public abstract IEnumerable<T> PageAsEnumerable();


    /// <summary>
    /// Returns the current page of data represented by an <see cref="IList{T}"/>
    /// </summary>
    /// <returns>Returns the current page of data represented by an <see cref="IList{T}"/></returns>
    public IList<T> PageToList() =>
        [.. PageAsEnumerable()];

    /// <summary>
    /// Returns the current page of data represented by an <see cref="ICollection{T}"/>
    /// </summary>
    /// <returns>Returns the current page of data represented by an <see cref="ICollection{T}"/></returns>
    public ICollection<T> PageToCollection() =>
        [.. PageAsEnumerable()];

    /// <summary>
    /// Returns the current page of data represented by an array
    /// </summary>
    /// <returns>
    /// Returns the current page of data represented by an array
    /// </returns>
    public T[] PageToArray() =>
        [.. PageAsEnumerable()];

    /// <summary>
    /// Public getter that returns an array of page numbers to be displayed in a pagination control based on the current page and total page count. No setter.
    /// </summary>
    /// <remarks>
    /// This property attempts to provide a range of 11 page numbers with the current page centered in the middle.
    /// If the current page is too close to the beginning, it returns the first 10 pages;
    /// if it’s too close to the end, it returns the last 10 pages.
    /// After this logic is accomplished, the very first and very last pages are added to the collection,
    /// this serves as the fast forward to end and rewind to start functionality.    /// 
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

                //perpend the very first element if it is not present
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
