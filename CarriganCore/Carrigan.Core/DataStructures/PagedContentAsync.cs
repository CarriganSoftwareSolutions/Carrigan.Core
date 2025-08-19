using Carrigan.Core.Interfaces;

namespace Carrigan.Core.DataStructures;

/// <summary>
/// Provides a concrete asynchronous paging implementation.
/// Inherits from <see cref="PagedContentBaseAsync{T}"/> and uses an asynchronous page access service.
/// </summary>
/// <typeparam name="T">The type of the model to be paged.</typeparam>
public class PagedContentAsync<T> : PagedContentBaseAsync<T>
{
    private IAsyncPageAccess<T> _access;
    private uint _PageSize;
    private uint _PageNumber;

    /// <summary>
    /// Concrete constructor for asynchronous paging implementation.
    /// </summary>
    /// <param name="access">Interface for asynchronous access to page-based data retrieval</param>
    /// <param name="pageNumber">Initial page number</param>
    /// <param name="pageSize">Page size</param>
    /// <exception cref="ArgumentNullException">Throws an <see cref="ArgumentNullException"> access is null.</exception>
    public PagedContentAsync(IAsyncPageAccess<T> access, uint pageNumber, uint pageSize)
    {
        _access = access ?? throw new ArgumentNullException(nameof(access));
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    /// <summary>
    /// Public getter for page number. Protected setter
    /// </summary>
    public override uint PageNumber
    {
        get => _PageNumber;
        protected set
        {
            // Preserve cache clearing logic from the synchronous version.
            _access.ClearCacheAsync().GetAwaiter().GetResult();
            _PageNumber = value;
        }
    }

    /// <summary>
    /// Public get for PageSize, protected set
    /// </summary>
    public override uint PageSize
    {
        get => _PageSize;
        protected set
        {
            _access.ClearCacheAsync().GetAwaiter().GetResult();
            _PageSize = value;
        }
    }

    /// <summary>
    /// Returns the total item count as <see cref="Task{uint}"/>
    /// </summary>
    /// <returns>Returns the total item count as <see cref="Task{uint}"/>.</returns>
    public override async Task<uint> GetTotalItemCountAsync() =>
        (uint)await _access.CountAllAsync();

    /// <summary>
    /// Returns all items as an <see cref="Task{IEnumerable{T}}"/>
    /// </summary>
    /// <returns>Returns all items as an <see cref="Task{IEnumerable{T}}"/></returns>
    public override async Task<IEnumerable<T>> AsEnumerableAsync() =>
        await _access.GetAllAsync();

    /// <summary>
    /// Returns all items on the current page as an <see cref="Task{IEnumerable{T}}"/>
    /// </summary>
    /// <returns>Returns all items on the current page as an <see cref="Task{IEnumerable{T}}"/></returns>
    public override async Task<IEnumerable<T>> PageAsEnumerableAsync() =>
        await _access.GetPageAsync(PageNumber, PageSize);
}
