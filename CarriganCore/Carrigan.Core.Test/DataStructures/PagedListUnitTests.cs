using Carrigan.Core.DataStructures;
using Carrigan.Core.Interfaces;

namespace Carrigan.Core.Test.DataStructures;

/// <summary>
/// Unit tests for a concrete implementation of IPagedList&lt;T&gt;.
/// </summary>
public class PagedListUnitTests
{
    private readonly List<int> _data;
    private PagedList<int> _pagedList;

    public PagedListUnitTests()
    {
        // Create sample data from 1 to 10.
        _data = [.. Enumerable.Range(1, 10)];

        // Create an instance with an initial page number of 1 and page size of 3.
        // This means:
        //   Page 1: [1, 2, 3]
        //   Page 2: [4, 5, 6]
        //   Page 3: [7, 8, 9]
        //   Page 4: [10]
        _pagedList = new PagedList<int>(_data, pageSize: 3);
    }

    #region Entire Collection Conversion Tests

    [Fact]
    public void EntireCollectionConversionMethods_ReturnFullData()
    {
        // ToList test
        IList<int> fullList = _pagedList.ToList();
        Assert.Equal(_data, fullList);

        // ToCollection test
        ICollection<int> fullCollection = _pagedList.ToCollection();
        Assert.Equal(_data, fullCollection);

        // AsEnumerable test
        IEnumerable<int> fullEnumerable = _pagedList.AsEnumerable();
        Assert.Equal(_data, [.. fullEnumerable]);

        // ToArray test
        int[] fullArray = _pagedList.ToArray();
        Assert.Equal(_data, fullArray);
    }

    #endregion

    #region Current Page Conversion Tests

    [Fact]
    public void CurrentPageConversionMethods_ReturnCorrectPageData()
    {
        // On page 1 with page size 3, the expected current page is [1, 2, 3].
        List<int> expectedPage = [.. _data.Take(3)];

        IList<int> pageList = _pagedList.PageToList();
        Assert.Equal(expectedPage, pageList);

        ICollection<int> pageCollection = _pagedList.PageToCollection();
        Assert.Equal(expectedPage, pageCollection);

        IEnumerable<int> pageEnumerable = _pagedList.PageAsEnumerable();
        Assert.Equal(expectedPage, [.. pageEnumerable]);

        int[] pageArray = _pagedList.PageToArray();
        Assert.Equal(expectedPage, pageArray);
    }

    #endregion

    #region Navigation Tests

    [Fact]
    public void NextPage_UpdatesPageNumberAndCurrentPageData()
    {
        // Move from page 1 to page 2.
        IPagedContent<int> updatedPagedList = _pagedList.NextPage();
        Assert.Equal(2u, updatedPagedList.PageNumber);

        // Expected page 2: [4, 5, 6]
        List<int> expectedPage = [.. _data.Skip(3).Take(3)];
        Assert.Equal(expectedPage, updatedPagedList.PageToList());
    }

    [Fact]
    public void PreviousPage_UpdatesPageNumberAndCurrentPageData()
    {
        // First move to page 2.
        IPagedContent<int> updatedPagedList = _pagedList.NextPage();
        Assert.Equal(2u, updatedPagedList.PageNumber);

        // Then move back to page 1.
        updatedPagedList = updatedPagedList.PreviousPage();
        Assert.Equal(1u, updatedPagedList.PageNumber);

        List<int> expectedPage = [.. _data.Take(3)];
        Assert.Equal(expectedPage, updatedPagedList.PageToList());
    }

    [Fact]
    public void FirstAndLastPage_ReturnCorrectPageData()
    {
        // Move to the last page.
        IPagedContent<int> updatedPagedList = _pagedList.LastPage();
        Assert.Equal(_pagedList.PageCount, updatedPagedList.PageNumber);

        // On our dataset, page 4 should contain only [10].
        List<int> expectedLastPage = [.. _data.Skip(9).Take(3)];
        Assert.Equal(expectedLastPage, updatedPagedList.PageToList());

        // Now move back to the first page.
        updatedPagedList = updatedPagedList.FirstPage();
        Assert.Equal(1u, updatedPagedList.PageNumber);
        List<int> expectedFirstPage = [.. _data.Take(3)];
        Assert.Equal(expectedFirstPage, updatedPagedList.PageToList());
    }

    [Fact]
    public void GoToPage_SetsCorrectPage()
    {
        // Navigate to page 3.
        IPagedContent<int> updatedPagedList = _pagedList.GoToPage(3);
        Assert.Equal(3u, updatedPagedList.PageNumber);

        // Expected page 3: [7, 8, 9]
        List<int> expectedPage = [.. _data.Skip(6).Take(3)];
        Assert.Equal(expectedPage, updatedPagedList.PageToList());
    }

    #endregion

    #region Change Page Size Tests
    [Fact]
    public void ChangePageSize_UpdatesPaginationState()
    {
        // Change page size to 4.
        IPagedContent<int> updatedPagedList = _pagedList.ChangePageSize(4);
        Assert.Equal(4u, updatedPagedList.PageSize);

        // With 10 items and a page size of 4, there should be 3 pages.
        Assert.Equal(3u, updatedPagedList.PageCount);

        // Test page 1.
        List<int> expectedPage1 = [.. _data.Take(4)];
        Assert.Equal(expectedPage1, updatedPagedList.PageToList());

        // Test page 2.
        // Assuming ChangePageNumber returns a new instance with the updated current page.
        IPagedContent<int> page2PagedList = updatedPagedList.GoToPage(2);
        List<int> expectedPage2 = [.. _data.Skip(4).Take(4)];
        Assert.Equal(expectedPage2, page2PagedList.PageToList());

        // Test page 3.
        IPagedContent<int> page3PagedList = updatedPagedList.GoToPage(3);
        List<int> expectedPage3 = [.. _data.Skip(8).Take(4)];
        Assert.Equal(expectedPage3, page3PagedList.PageToList());
    }

    #endregion

    [Fact]
    public void GoToPage_PageNumberTooHigh_GoesToLastPageAndReturnsSameInstance()
    {
        // Act: Attempt to go to a page number that exceeds the available pages.
        IPagedContent<int> result = _pagedList.GoToPage(100);

        // Assert: The current page should be set to the last page.
        Assert.Equal(_pagedList.PageCount, _pagedList.PageNumber);
        Assert.Same(_pagedList, result);
    }

    [Fact]
    public void NextPage_OnLastPage_DoesNothingAndReturnsSameInstance()
    {
        // Arrange: Navigate to the last page.
        _pagedList.LastPage();

        // Act: Call NextPage while on the last page.
        IPagedContent<int> result = _pagedList.NextPage();

        // Assert: The current page remains unchanged and the same instance is returned.
        Assert.Equal(_pagedList.PageCount, _pagedList.PageNumber);
        Assert.Same(_pagedList, result);
    }

    [Fact]
    public void PreviousPage_OnFirstPage_DoesNothingAndReturnsSameInstance()
    {
        // Arrange: Ensure we are on the first page.
        _pagedList.FirstPage();

        // Act: Call PreviousPage while on the first page.
        IPagedContent<int> result = _pagedList.PreviousPage();

        // Assert: The current page remains the first page and the same instance is returned.
        Assert.Equal(1u, _pagedList.PageNumber);
        Assert.Same(_pagedList, result);
    }

    [Theory]
    [InlineData(0)]
    public void ChangePageSize_NegativeOrZero_SetsPageSizeToOne(uint invalidPageSize)
    {
        // Act: Change the page size using an invalid (zero or negative) value.
        _pagedList.ChangePageSize(invalidPageSize);

        // Assert: The page size should be set to 1.
        Assert.Equal(1u, _pagedList.PageNumber);
    }

}
