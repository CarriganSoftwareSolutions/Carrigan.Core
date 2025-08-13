namespace Carrigan.Core.DataStructures;

/// <summary>
/// This implementation of PagedContentBase mostly exists to unit test the base class.
/// However it does work as genuine page content class with a list as the underlying data structure.
/// The PageContent can be used with things like database access, using the paged access interface.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedList<T> : PagedContentBase<T>
{
    private IEnumerable<T> _enumerable;

    public override uint TotalItemCount => 
        (uint)_enumerable.Count();

    public PagedList(T[] values, uint pageSize)
    {
        _enumerable = [.. values];
        PageNumber = 1u;
        PageSize = pageSize;
    }
    public PagedList(IEnumerable<T> values, uint pageSize)
    {
        _enumerable = values;
        PageNumber = 1u;
        PageSize = pageSize;
    }
    public PagedList(IList<T> values, uint pageSize)
    {
        _enumerable = values;
        PageNumber = 1u;
        PageSize = pageSize;
    }
    public PagedList(ICollection<T> values, uint pageSize)
    {
        _enumerable = values;
        PageNumber = 1u;
        PageSize = pageSize;
    }

    public override IEnumerable<T> AsEnumerable() =>
        _enumerable;

    public override IEnumerable<T> PageAsEnumerable() =>
        _enumerable
            .Skip((int)((PageNumber - 1u) * PageSize))
            .Take((int)PageSize);
}
