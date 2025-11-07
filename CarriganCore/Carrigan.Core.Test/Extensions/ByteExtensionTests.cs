using Carrigan.Core.Extensions;

namespace Carrigan.Core.Test.Extensions;

public class ByteExtensionsTests
{
    [Fact]
    public void FormattedSize_NullByteArray_ReturnsZeroBytes()
    {
        byte[]? byteArray = null;
        string result = byteArray.FormattedSize();
        Assert.Equal("0 bytes", result);
    }

    [Fact]
    public void FormattedSize_EmptyByteArray_ReturnsZeroBytes()
    {
        byte[] byteArray = [];
        string result = byteArray.FormattedSize();
        Assert.Equal("0 bytes", result);
    }

    [Theory]
    [InlineData(1, "1 bytes")]
    [InlineData(500, "500 bytes")]
    [InlineData(1023, "1023 bytes")]
    public void FormattedSize_LessThanKB_ReturnsBytes(long size, string expected)
    {
        byte[] byteArray = new byte[size];
        string result = byteArray.FormattedSize();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1024, "1 KB")]
    [InlineData(1536, "1.5 KB")]
    [InlineData(1048576, "1 MB")]
    [InlineData(1572864, "1.5 MB")]
    //the following would be great tests if the files sizes were feasible.
    //support beyond MB will just have to be theoretical for now.
    //it is possible
    //[InlineData(1073741824, "1 GB")]
    //[InlineData(1610612736, "1.5 GB")]
    //[InlineData(1099511627776, "1 TB")]
    //[InlineData(1649267441664, "1.5 TB")]
    //[InlineData(1125899906842624, "1 PB")]
    //[InlineData(1688849860263936, "1.5 PB")]
    //[InlineData(1152921504606846976, "1 EB")]
    //[InlineData(1729382256910270464, "1.5 EB")]
    public void FormattedSize_MultipleUnits_ReturnsCorrectFormattedSize(long size, string expected)
    {
        byte[] byteArray = new byte[size];
        string result = byteArray.FormattedSize();
        Assert.Equal(expected, result);
    }

    //Just because the extension method supports byte array's of exabyte length, doesn't mean I can create the array to test it.
    //Maybe one day?
    //[Fact]
    //public void FormattedSize_LargeSizeBeyondEB_ReturnsEB()
    //{
    //    // Assuming the method caps at EB
    //    byte[] byteArray = new byte[long.MaxValue];
    //    string result = byteArray.FormattedSize();
    //    // Calculate expected value
    //    double size = (double)long.MaxValue;
    //    string[] sizeUnits = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB" };
    //    int unitIndex = 0;
    //    while (size >= 1024 && unitIndex < sizeUnits.Length - 1)
    //    {
    //        size /= 1024;
    //        unitIndex++;
    //    }
    //    string expected = $"{size:0.##} {sizeUnits[unitIndex]}";
    //    Assert.Equal(expected, result);
    //}

    [Fact]
    public void FormattedSize_SizeExactlyAtUnitThresholds_ReturnsExactUnit()
    {
        string[] sizeUnits = ["bytes", "KB", "MB"/*, "GB", "TB", "PB", "EB"*/]; //again there are practical limits at play here.
        for (int i = 0; i < sizeUnits.Length; i++)
        {
            long size = (long)Math.Pow(1024, i);
            byte[] byteArray = new byte[size];
            string result = byteArray.FormattedSize();
            string expectedUnit = sizeUnits[i];
            string expected = i == 0 ? $"{size} {expectedUnit}" : $"1 {expectedUnit}";
            Assert.Equal(expected, result);
        }
    }

    [Fact]
    public void FormattedSize_SizeWithDecimals_ReturnsFormattedWithTwoDecimals()
    {
        byte[] byteArray = new byte[1500]; // 1500 bytes = ~1.46 KB
        string result = byteArray.FormattedSize();
        Assert.Equal("1.46 KB", result);
    }


    [Fact]
    public void BlockAppend_BothArraysNonNullAndNonEmpty_ShouldConcatenateCorrectly()
    {
        byte[] first = [1, 2, 3];
        byte[] second = [4, 5, 6];
        byte[] expected = [1, 2, 3, 4, 5, 6];

        byte[] result = first.BlockAppend(second);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void BlockAppend_FirstArrayNull_ShouldReturnSecondArray()
    {
        byte[]? first = null;
        byte[] second = [4, 5, 6];
        byte[] expected = [4, 5, 6];

        byte[] result = first.BlockAppend(second);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void BlockAppend_SecondArrayNull_ShouldReturnFirstArray()
    {
        byte[] first = [1, 2, 3];
        byte[]? second = null;
        byte[] expected = [1, 2, 3];

        byte[] result = first.BlockAppend(second);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void BlockAppend_BothArraysNull_ShouldReturnEmptyArray()
    {
        byte[]? first = null;
        byte[]? second = null;
        byte[] expected = [];

        byte[] result = first.BlockAppend(second);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void BlockAppend_FirstArrayEmpty_ShouldReturnSecondArray()
    {
        byte[] first = [];
        byte[] second = [4, 5, 6];
        byte[] expected = [4, 5, 6];

        byte[] result = first.BlockAppend(second);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void BlockAppend_SecondArrayEmpty_ShouldReturnFirstArray()
    {
        byte[] first = [1, 2, 3];
        byte[] second = [];
        byte[] expected = [1, 2, 3];

        byte[] result = first.BlockAppend(second);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void BlockAppend_BothArraysEmpty_ShouldReturnEmptyArray()
    {
        byte[] first = [];
        byte[] second = [];
        byte[] expected = [];

        byte[] result = first.BlockAppend(second);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void BlockAppend_LargeArrays_ShouldConcatenateCorrectly()
    {
        byte[] first = new byte[1000];
        byte[] second = new byte[2000];
        for (int i = 0; i < 1000; i++) first[i] = (byte)(i % 256);
        for (int i = 0; i < 2000; i++) second[i] = (byte)((i + 1000) % 256);

        byte[] expected = new byte[3000];
        Array.Copy(first, 0, expected, 0, 1000);
        Array.Copy(second, 0, expected, 1000, 2000);

        byte[] result = first.BlockAppend(second);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void BlockAppend_UnicodeByteArrays_ShouldConcatenateCorrectly()
    {
        byte[] first = System.Text.Encoding.UTF8.GetBytes("Hello, ");
        byte[] second = System.Text.Encoding.UTF8.GetBytes("World!");
        byte[] expected = System.Text.Encoding.UTF8.GetBytes("Hello, World!");

        byte[] result = first.BlockAppend(second);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void BlockAppend_SameArrayInstance_ShouldDuplicateArray()
    {
        byte[] first = [1, 2, 3];
        byte[] second = first;
        byte[] expected = [1, 2, 3, 1, 2, 3];

        byte[] result = first.BlockAppend(second);

        Assert.Equal(expected, result);
    }
}