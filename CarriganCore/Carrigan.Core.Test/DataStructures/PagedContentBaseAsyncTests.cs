using Carrigan.Core.Test.DataStructures.TestClass;

namespace Carrigan.Core.Test.DataStructures;

public class PagedContentBaseAsyncTests
{
    [Theory]
    //Special case with less than 11 pages
    [InlineData(1, 1, new uint[] { 1 })]

    //Special case with less than 11 pages
    [InlineData(1, 2, new uint[] { 1, 2 })]
    [InlineData(2, 2, new uint[] { 1, 2 })]



    //Special case with less than 11 pages
    [InlineData(1, 5, new uint[] { 1, 2, 3, 4, 5 })]
    [InlineData(2, 5, new uint[] { 1, 2, 3, 4, 5 })]
    [InlineData(3, 5, new uint[] { 1, 2, 3, 4, 5 })]
    [InlineData(4, 5, new uint[] { 1, 2, 3, 4, 5 })]
    [InlineData(5, 5, new uint[] { 1, 2, 3, 4, 5 })]



    //Special case with less than 11 pages
    [InlineData(1, 6, new uint[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData(2, 6, new uint[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData(3, 6, new uint[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData(4, 6, new uint[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData(5, 6, new uint[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData(6, 6, new uint[] { 1, 2, 3, 4, 5, 6 })]



    //Special case with less than 11 pages
    [InlineData(1, 7, new uint[] { 1, 2, 3, 4, 5, 6, 7 })]
    [InlineData(2, 7, new uint[] { 1, 2, 3, 4, 5, 6, 7 })]
    [InlineData(3, 7, new uint[] { 1, 2, 3, 4, 5, 6, 7 })]
    [InlineData(4, 7, new uint[] { 1, 2, 3, 4, 5, 6, 7 })]
    [InlineData(5, 7, new uint[] { 1, 2, 3, 4, 5, 6, 7 })]
    [InlineData(6, 7, new uint[] { 1, 2, 3, 4, 5, 6, 7 })]
    [InlineData(7, 7, new uint[] { 1, 2, 3, 4, 5, 6, 7 })]



    //Special case with less than 11 pages
    [InlineData(3u, 8u, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8 })]



    //Special case with less than 11 pages
    [InlineData(1, 9, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
    [InlineData(2, 9, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
    [InlineData(4, 9, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
    [InlineData(5, 9, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
    [InlineData(6, 9, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
    [InlineData(8, 9, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
    [InlineData(9, 9, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]



    //Special case with less than 11 pages
    [InlineData(1, 10, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
    [InlineData(2, 10, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
    [InlineData(4, 10, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
    [InlineData(5, 10, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
    [InlineData(6, 10, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
    [InlineData(7, 10, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
    [InlineData(9, 10, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
    [InlineData(10, 10, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]


    //11 or more pages, special case where current page is near the front
    [InlineData(1, 11, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 })] //note: 11 was appended
    [InlineData(2, 11, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 })] //note: 11 was appended
    [InlineData(4, 11, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 })] //note: 11 was appended
    [InlineData(5, 11, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 })] //note: 11 was appended

    //This is the only case where there are exactly +/-5 pages around the current page,
    //and the first and last page are represented with out needing to be prepended or appended.
    [InlineData(6, 11, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 })]

    //Special case where there are at least 11 pages, and near the end.
    [InlineData(7, 11, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 })] //note: 1 was prepended
    [InlineData(10, 11, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 })] //note: 1 was prepended 
    [InlineData(11, 11, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 })] //note: 1 was prepended


    //11 or more pages, special case where current page is near the front
    [InlineData(1, 14, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 14 })] //note: 14 was appended
    [InlineData(2, 14, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 14 })] //note: 14 was appended
    [InlineData(4, 14, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 14 })] //note: 14 was appended
    [InlineData(5, 14, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 14 })] //note: 14 was appended

    [InlineData(6, 14, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 14 })]  //note: 14 was appended
    [InlineData(7, 14, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 14 })] //note: the 1 was prepended, 14 appended
    [InlineData(8, 14, new uint[] { 1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 })] //note: the 1 was prepended, 14 appended
    [InlineData(9, 14, new uint[] { 1, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 })] //note the 1 was prepended

    //Special case where there are at least 11 pages, and near the end.
    [InlineData(10, 14, new uint[] { 1, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 })] //note: the 1 was prepended
    [InlineData(13, 14, new uint[] { 1, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 })] //note: the 1 was prepended
    [InlineData(14, 14, new uint[] { 1, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 })] //note: the 1 was prepended


    //11 or more pages, special case where current page is near the front
    [InlineData(1, 15, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15 })] //note: 15 was appended
    [InlineData(2, 15, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15 })] //note: 15 was appended
    [InlineData(4, 15, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15 })] //note: 15 was appended
    [InlineData(5, 15, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15 })] //note: 15 was appended

    //11 or more pages and +/-5 pages on either side of the current page plus prepended and appended first and last pages.
    [InlineData(6, 15, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 15 })] //note: 15 was appended
    [InlineData(7, 15, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 15 })] //note: 1 was prepended, 15 was appended
    [InlineData(8, 15, new uint[] { 1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 15 })] //note: 1 was prepended, 15 was appended
    [InlineData(9, 15, new uint[] { 1, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 })] //note: 1 was prepended, 15 was appended
    [InlineData(10, 15, new uint[] { 1, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 })] //note: 1 was prepended

    //Special case where there are at least 11 pages, and near the end.
    [InlineData(11, 15, new uint[] { 1, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 })] //note: 1 was prepended
    [InlineData(12, 15, new uint[] { 1, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 })] //note: 1 was prepended
    [InlineData(13, 15, new uint[] { 1, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 })] //note: 1 was prepended
    [InlineData(14, 15, new uint[] { 1, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 })] //note: 1 was prepended
    [InlineData(15, 15, new uint[] { 1, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 })] //note: 1 was prepended


    //11 or more pages, special case where current page is near the front
    [InlineData(1, 18, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 18 })] //note: 16 was appended
    [InlineData(5, 18, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 18 })] //note: 16 was appended

    //11 or more pages and +/-5 pages on either side of the current page plus prepended and appended first and last pages.
    [InlineData(6, 18, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 18 })] //note: 16 was appended
    [InlineData(7, 18, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 18 })] //note: 1 was prepended, 18 was appended
    [InlineData(12, 18, new uint[] { 1, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 })] //note: 1 was prepended, 18 was appended
    [InlineData(13, 18, new uint[] { 1, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 })] //note: 1 was prepended

    //Special case where there are at least 11 pages, and near the end.
    [InlineData(14, 18, new uint[] { 1, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 })] //note: 1 was prepended
    [InlineData(18, 18, new uint[] { 1, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 })] //note: 1 was prepended


    //11 or more pages, special case where current page is near the front
    [InlineData(1, 19, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 19 })] //note: 19 was appended
    [InlineData(5, 19, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 19 })] //note: 19 was appended

    //11 or more pages and +/-5 pages on either side of the current page plus prepended and appended first and last pages.
    [InlineData(6, 19, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 19 })] //note: 19 was appended
    [InlineData(7, 19, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 19 })] //note: 1 was prepended, 19 was appended
    [InlineData(13, 19, new uint[] { 1, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 })] //note: 1 was prepended, 19 was appended
    [InlineData(14, 19, new uint[] { 1, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 })] //note: 1 was prepended

    //Special case where there are at least 11 pages, and near the end.
    [InlineData(15, 19, new uint[] { 1, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 })] //note: 1 was prepended
    [InlineData(18, 19, new uint[] { 1, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 })] //note: 1 was prepended


    //11 or more pages, special case where current page is near the front
    [InlineData(1, 20, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 20 })] //note: 20 was appended
    [InlineData(5, 20, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 20 })] //note: 20 was appended

    //11 or more pages and +/-5 pages on either side of the current page plus prepended and appended first and last pages.
    [InlineData(7, 20, new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 20 })] //note: 1 was prepended, 20 was appended
    [InlineData(8, 20, new uint[] { 1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 20 })] //note: 1 was prepended, 20 was appended
    [InlineData(14, 20, new uint[] { 1, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 })] //note: 1 was prepended, 20 was appended
    [InlineData(15, 20, new uint[] { 1, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 })] //note: 1 was prepended

    //Special case where there are at least 11 pages, and near the end.
    [InlineData(16u, 20u, new uint[] { 1, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 })] //note: 1 was prepended
    [InlineData(17u, 20u, new uint[] { 1, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 })] //note: 1 was prepended
    [InlineData(18u, 20u, new uint[] { 1, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 })] //note: 1 was prepended
    [InlineData(19u, 20u, new uint[] { 1, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 })] //note: 1 was prepended
    [InlineData(20u, 20u, new uint[] { 1, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 })] //note: 1 was prepended

    public async Task PagedContentBaseAsync_NavigationPages_Tests(uint pageNumber, uint pageCount, uint[] expected)
    {
        NavigationPagesTestClassAsync testClass = new(pageNumber, pageCount);
        uint[] actual = await testClass.GetNavigationPagesAsync();
        Assert.Equal(expected, actual);
    }

}
