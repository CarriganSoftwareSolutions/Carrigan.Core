namespace Carrigan.Core.DataStructures;

/// <summary>
/// This is a simple in-memory implementation of paging over an <see cref="IEnumerable{T}"/>.
/// This implementation of PagedContentBase mostly exists to unit test the base class.
/// However it does work as genuine page content class with support for various collection types.
/// </summary>
/// <typeparam name="T">The item type.</typeparam>
public class PagedList<T> : PagedContentBase<T>
{
    private IEnumerable<T> _enumerable;

    /// <summary>
    /// Total number of items, computed from the underlying <see cref="IEnumerable{T}"/>.
    /// </summary>
    public override uint TotalItemCount => 
        (uint)_enumerable.Count();

    /// <summary>
    /// Create an instance of the class using an array.
    /// </summary>
    /// <param name="values">Values represented by an array.</param>
    /// <param name="pageSize">Page size</param>
    public PagedList(T[] values, uint pageSize)
    {
        _enumerable = [.. values];
        PageNumber = 1u;
        PageSize = pageSize;
    }

    /// <summary>
    /// Create an instance of the class using an <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="values">Values represented by an <see cref="IEnumerable{T}"/></param>
    /// <param name="pageSize">Page size</param>
    public PagedList(IEnumerable<T> values, uint pageSize)
    {
        _enumerable = values;
        PageNumber = 1u;
        PageSize = pageSize;
    }

    /// <summary>
    /// Create an instance of the class using an <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="values">Values represented by an <see cref="IList{T}"/></param>
    /// <param name="pageSize">Page size</param>
    public PagedList(IList<T> values, uint pageSize)
    {
        _enumerable = values;
        PageNumber = 1u;
        PageSize = pageSize;
    }

    /// <summary>
    /// Create an instance of the class using an <see cref="ICollection{T}"/>.
    /// </summary>
    /// <param name="values">Values represented by an <see cref="ICollection{T}"/>.</param>
    /// <param name="pageSize">Page size</param>
    public PagedList(ICollection<T> values, uint pageSize)
    {
        _enumerable = values;
        PageNumber = 1u;
        PageSize = pageSize;
    }

    /// <summary>
    /// Returns an <see cref="IEnumerable{T}"/> that represents the underlying data.
    /// </summary>
    /// <returns>Returns the underlying data as an <see cref="IEnumerable{T}"/>.</returns>
    public override IEnumerable<T> AsEnumerable() =>
        _enumerable;

    /// <summary>
    /// Returns an <see cref="IEnumerable{T}"/> that represents the current <see cref="PageNumber"/> and <see cref="PageSize"/>
    /// </summary>
    /// <returns>Returns an <see cref="IEnumerable{T}"/> that represents the current <see cref="PageNumber"/> and <see cref="PageSize"/></returns>
    public override IEnumerable<T> PageAsEnumerable() =>
        _enumerable
            .Skip((int)((PageNumber - 1u) * PageSize))
            .Take((int)PageSize);
}
