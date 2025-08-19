using Carrigan.Core.Test.DataStructures.TestClass;

namespace Carrigan.Core.Test.DataStructures;

/// <summary>
/// Unit tests for the concrete implementation of PagedContentBaseAsync.
/// </summary>
public class PagedListAsyncUnitTests
{
    [Fact]
    public async Task GetTotalItemCountAsync_ReturnsCorrectCount()
    {
        // Arrange
        int[] numbers = [1, 2, 3, 4, 5];
        PagedListAsyncTestClass<int> pagedList = new(numbers, 2);

        // Act
        uint totalCount = await pagedList.GetTotalItemCountAsync();

        // Assert
        Assert.Equal((uint)numbers.Length, totalCount);
    }

    [Fact]
    public async Task AsEnumerableAsync_ReturnsAllItems()
    {
        // Arrange
        int[] numbers = [10, 20, 30, 40, 50];
        PagedListAsyncTestClass<int> pagedList = new(numbers, 3);

        // Act
        IEnumerable<int> allItems = await pagedList.AsEnumerableAsync();

        // Assert
        Assert.Equal(numbers, allItems);
    }

    [Theory]
    [InlineData(1, 3, new int[] { 1, 2, 3 })]
    [InlineData(2, 3, new int[] { 4, 5, 6 })]
    [InlineData(3, 3, new int[] { 7, 8, 9 })]
    [InlineData(4, 3, new int[] { 10 })]
    public async Task PageAsEnumerableAsync_ReturnsCorrectPage(int pageNumber, int pageSize, int[] expected)
    {
        // Arrange
        int[] numbers = [.. Enumerable.Range(1, 10)];
        PagedListAsyncTestClass<int> pagedList = new(numbers, (uint)pageSize);

        // Instead of setting PageNumber directly (setter is protected), navigate to the desired page.
        await pagedList.GoToPageAsync((uint)pageNumber);

        // Act
        IEnumerable<int> pageItems = await pagedList.PageAsEnumerableAsync();

        // Assert
        Assert.Equal(expected, pageItems);
    }

    [Fact]
    public async Task ChangePageSizeAsync_ResetsPageNumber()
    {
        // Arrange
        int[] numbers = [.. Enumerable.Range(1, 10)];
        PagedListAsyncTestClass<int> pagedList = new(numbers, 2);

        // Navigate to a different page (say page 3)
        await pagedList.GoToPageAsync(3);

        // Act
        await pagedList.ChangePageSizeAsync(5);

        // Assert
        Assert.Equal(1u, pagedList.PageNumber);
        Assert.Equal(5u, pagedList.PageSize);
    }

    [Fact]
    public async Task GetNavigationPagesAsync_ReturnsExpectedPages()
    {
        // Arrange
        // For example, if total pages = 20 and current page = 10, expected navigation might be:
        // If using the same logic as in the base class, with current page = 10:
        // start = 10 - 5 = 5, end = 10 + 5 = 15,
        // then if first page is not 1, prepend 1 and if last page is not pageCount, append pageCount.
        List<uint> expectedPages = [1, .. Enumerable.Range(5, 11).Select(x => (uint)x), 20];

        uint pageNumber = 10;
        uint totalPageCount = 20;

        // Use the non-generic NavigationPagesTestClassAsync
        NavigationPagesTestClassAsync testClass = new (pageNumber, totalPageCount);

        // Act
        uint[] actualPages = await testClass.GetNavigationPagesAsync();

        // Assert
        Assert.Equal(expectedPages, actualPages);
    }
}
