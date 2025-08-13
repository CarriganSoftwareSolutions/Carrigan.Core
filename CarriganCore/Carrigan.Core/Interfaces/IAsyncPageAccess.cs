namespace Carrigan.Core.Interfaces;

/// <summary>
/// Provides asynchronous access to page-based data retrieval.
/// </summary>
/// <typeparam name="TModel">The type of the model to page.</typeparam>
public interface IAsyncPageAccess<TModel>
{
    /// <summary>
    /// Asynchronously counts all available items.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the total item count.</returns>
    Task<int> CountAllAsync();

    /// <summary>
    /// Asynchronously retrieves all available items.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains all items.</returns>
    Task<IEnumerable<TModel>> GetAllAsync();

    /// <summary>
    /// Asynchronously retrieves a page of items based on the specified page number and page size.
    /// </summary>
    /// <param name="pageNumber">The page number (1-indexed).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the items for the specified page.</returns>
    Task<IEnumerable<TModel>> GetPageAsync(uint pageNumber, uint pageSize);

    /// <summary>
    /// Asynchronously clears any cached data.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ClearCacheAsync();
}
