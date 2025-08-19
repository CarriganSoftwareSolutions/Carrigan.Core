namespace Carrigan.Core.Test.DataStructures.TestClass;

/// <summary>
/// A test class to simulate navigation pages without a generic parameter.
/// Inherits from PagedListAsyncTestClass of type object and exposes only the constructor 
/// (uint pageNumber, uint pageCount) for testing navigation page logic.
/// </summary>
public class NavigationPagesTestClassAsync : PagedListAsyncTestClass<object>
{
    private readonly uint _pageCount;

    /// <summary>
    /// Initializes a new instance of the NavigationPagesTestClassAsync class.
    /// </summary>
    /// <param name="pageNumber">The current page number.</param>
    /// <param name="pageCount">The total number of pages.</param>
    public NavigationPagesTestClassAsync(uint pageNumber, uint pageCount)
        : base(new List<object>(), 1)
    {
        PageNumber = pageNumber;
        _pageCount = pageCount;
    }

    /// <summary>
    /// Returns 0 for total item count for testing purposes.
    /// </summary>
    public override Task<uint> GetTotalItemCountAsync() =>
        Task.FromResult(0u);

    /// <summary>
    /// Returns the page count provided in the constructor.
    /// </summary>
    public override Task<uint> GetPageCountAsync() =>
        Task.FromResult(_pageCount);

    /// <summary>
    /// Returns an empty collection.
    /// </summary>
    public override Task<IEnumerable<object>> AsEnumerableAsync() =>
        Task.FromResult(Enumerable.Empty<object>());

    /// <summary>
    /// Returns an empty collection for the current page.
    /// </summary>
    public override Task<IEnumerable<object>> PageAsEnumerableAsync() =>
        Task.FromResult(Enumerable.Empty<object>());
}
