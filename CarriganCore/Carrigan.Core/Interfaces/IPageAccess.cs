namespace Carrigan.Core.Interfaces;

/// <summary>
/// Defines methods for accessing paged data in a synchronous manner.
/// </summary>
/// <typeparam name="TModel">The type of the model to be paged.</typeparam>
public interface IPageAccess<TModel>
{
    /// <summary>
    /// Counts all available items.
    /// </summary>
    /// <returns>The total number of items.</returns>
    int CountAll();

    /// <summary>
    /// Retrieves all available items.
    /// </summary>
    /// <returns>An enumerable collection of all items.</returns>
    IEnumerable<TModel> GetAll();

    /// <summary>
    /// Retrieves a specific page of items.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve (1-indexed).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>An enumerable collection of items for the specified page.</returns>
    IEnumerable<TModel> GetPage(uint pageNumber, uint pageSize);

    /// <summary>
    /// Clears any cached data to ensure fresh retrieval on subsequent queries.
    /// </summary>
    void ClearCache();
}
