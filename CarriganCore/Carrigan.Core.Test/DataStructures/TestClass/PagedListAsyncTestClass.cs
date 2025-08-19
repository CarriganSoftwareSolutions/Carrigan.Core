
using Carrigan.Core.DataStructures;


namespace Carrigan.Core.Test.DataStructures.TestClass;

/// <summary>
/// This implementation of PagedContentBaseAsync mostly exists to unit test the base class.
/// However it does work as a genuine page content class with a list as the underlying data structure.
/// The PagedListAsync can be used with things like database access, using the paged access interface.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedListAsyncTestClass<T> : PagedContentBaseAsync<T>
{
    private IEnumerable<T> _enumerable;

    public override Task<uint> GetTotalItemCountAsync() =>
        Task.FromResult((uint)_enumerable.Count());

    public PagedListAsyncTestClass(T[] values, uint pageSize)
    {
        _enumerable = [.. values];
        PageNumber = 1u;
        PageSize = pageSize;
    }

    public PagedListAsyncTestClass(IEnumerable<T> values, uint pageSize)
    {
        _enumerable = values;
        PageNumber = 1u;
        PageSize = pageSize;
    }

    public PagedListAsyncTestClass(IList<T> values, uint pageSize)
    {
        _enumerable = values;
        PageNumber = 1u;
        PageSize = pageSize;
    }

    public PagedListAsyncTestClass(ICollection<T> values, uint pageSize)
    {
        _enumerable = values;
        PageNumber = 1u;
        PageSize = pageSize;
    }

    public override Task<IEnumerable<T>> AsEnumerableAsync() =>
        Task.FromResult(_enumerable);

    public override Task<IEnumerable<T>> PageAsEnumerableAsync()
    {
        IEnumerable<T> page = _enumerable
            .Skip((int)((PageNumber - 1u) * PageSize))
            .Take((int)PageSize);
        return Task.FromResult(page);
    }
}
