using Carrigan.Core.Interfaces;

namespace Carrigan.Core.DataStructures;

/// <summary>
/// Provides a concrete asynchronous paging implementation.
/// Inherits from <see cref="PagedContentBaseAsync{TModel}"/> and uses an asynchronous page access service.
/// </summary>
/// <typeparam name="TModel">The type of the model to be paged.</typeparam>
public class PagedContentAsync<TModel> : PagedContentBaseAsync<TModel>
{
    private IAsyncPageAccess<TModel> _access;
    private uint _PageSize;
    private uint _PageNumber;

    public PagedContentAsync(IAsyncPageAccess<TModel> access, uint pageNumber, uint pageSize)
    {
        _access = access ?? throw new ArgumentNullException(nameof(access));
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

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

    public override uint PageSize
    {
        get => _PageSize;
        protected set
        {
            _access.ClearCacheAsync().GetAwaiter().GetResult();
            _PageSize = value;
        }
    }

    public override async Task<uint> GetTotalItemCountAsync() =>
        (uint)await _access.CountAllAsync();

    public override async Task<IEnumerable<TModel>> AsEnumerableAsync() =>
        await _access.GetAllAsync();

    public override async Task<IEnumerable<TModel>> PageAsEnumerableAsync() =>
        await _access.GetPageAsync(PageNumber, PageSize);
}
