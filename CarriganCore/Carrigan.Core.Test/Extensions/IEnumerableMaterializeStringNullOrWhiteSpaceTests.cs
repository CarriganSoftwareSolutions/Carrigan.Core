using Carrigan.Core.Enums;
using Carrigan.Core.Exceptions;
using Carrigan.Core.Extensions;

namespace Carrigan.Core.Test.Extensions;

public class IEnumerableMaterializeStringNullOrWhiteSpaceTests
{
    [Theory]
    [InlineData(NullOrWhiteSpaceOptionsEnum.Allowed)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.FilteredOut)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.NullOrWhiteSpaceException)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.ArgumentNullOrWhiteSpaceException)]
    public void Materialize_EmptyCollection_ReturnsEmptyCollection(NullOrWhiteSpaceOptionsEnum option)
    {
        int enumerationCount = 0;
        IEnumerable<string> Enumerate()
        {
            enumerationCount++;
            yield break;
        }

        IEnumerable<string> result = Enumerate().Materialize(option);

        Assert.Equal(1, enumerationCount);
        Assert.Empty(result);
        Assert.Equal(1, enumerationCount);
    }

    [Theory]
    [InlineData(NullOrWhiteSpaceOptionsEnum.Allowed)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.FilteredOut)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.NullOrWhiteSpaceException)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.ArgumentNullOrWhiteSpaceException)]
    public void Materialize_ValidElements_PreservesOrder(NullOrWhiteSpaceOptionsEnum option)
    {
        IEnumerable<string> source = ["first", "last"];

        IEnumerable<string> result = source.Materialize(option);

        Assert.NotSame(source, result);
        Assert.Equal(["first", "last"], result);
    }

    [Fact]
    public void Materialize_Allowed_PreservesNullEmptyAndWhitespace()
    {
        IEnumerable<string> source = ["first", null!, "", " ", "\t", "last"];

        IEnumerable<string> result = source.Materialize(NullOrWhiteSpaceOptionsEnum.Allowed);

        Assert.Equal(source, result);
    }

    [Fact]
    public void Materialize_FilteredOut_RemovesInvalidElements()
    {
        IEnumerable<string> source = [null!, "first", "", " ", "\t", null!, "last"];

        IEnumerable<string> result = source.Materialize(NullOrWhiteSpaceOptionsEnum.FilteredOut);

        Assert.Equal(["first", "last"], result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\r\n")]
    public void Materialize_NullOrWhiteSpaceException_InvalidElement_ThrowsExpectedException(string? invalid)
    {
        IEnumerable<string> source = ["first", invalid!, "last"];

        NullOrWhiteSpaceException exception = Assert.Throws<NullOrWhiteSpaceException>(
            () => source.Materialize(NullOrWhiteSpaceOptionsEnum.NullOrWhiteSpaceException, "items").ToArray());

        Assert.Equal("'items' contains disallowed null or white space elements.", exception.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\r\n")]
    public void Materialize_ArgumentNullOrWhiteSpaceException_InvalidElement_ThrowsExpectedException(string? invalid)
    {
        IEnumerable<string> source = ["first", invalid!, "last"];

        ArgumentNullOrWhiteSpaceException exception = Assert.Throws<ArgumentNullOrWhiteSpaceException>(
            () => source.Materialize(NullOrWhiteSpaceOptionsEnum.ArgumentNullOrWhiteSpaceException, "items").ToArray());

        Assert.Equal("items", exception.ParamName);
        Assert.Contains("Collection contains disallowed null or white space elements.", exception.Message);
    }

    [Fact]
    public void Materialize_ArgumentNullOrWhiteSpaceException_CapturesCallerExpression()
    {
        IEnumerable<string> items = [""];

        ArgumentNullOrWhiteSpaceException exception = Assert.Throws<ArgumentNullOrWhiteSpaceException>(
            () => items.Materialize(NullOrWhiteSpaceOptionsEnum.ArgumentNullOrWhiteSpaceException).ToArray());

        Assert.Equal("items", exception.ParamName);
        Assert.Contains("Collection contains disallowed null or white space elements.", exception.Message);
    }

    [Theory]
    [InlineData(NullOrWhiteSpaceOptionsEnum.Allowed)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.FilteredOut)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.NullOrWhiteSpaceException)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.ArgumentNullOrWhiteSpaceException)]
    public void Materialize_NullSource_ThrowsArgumentNullException(NullOrWhiteSpaceOptionsEnum option)
    {
        IEnumerable<string>? source = null;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => source!.Materialize(option).ToArray());

        Assert.Equal("enumerable", exception.ParamName);
    }

    [Fact]
    public void Materialize_InvalidOption_ThrowsInvalidOperationException()
    {
        IEnumerable<string> source = ["first"];
        NullOrWhiteSpaceOptionsEnum option = (NullOrWhiteSpaceOptionsEnum)int.MaxValue;

        Assert.Throws<InvalidOperationException>(() => source.Materialize(option).ToArray());
    }

    [Theory]
    [InlineData(NullOrWhiteSpaceOptionsEnum.Allowed)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.FilteredOut)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.NullOrWhiteSpaceException)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.ArgumentNullOrWhiteSpaceException)]
    public void Materialize_ValidElements_EnumeratesSourceOnce(NullOrWhiteSpaceOptionsEnum option)
    {
        int enumerationCount = 0;
        IEnumerable<string> Enumerate()
        {
            enumerationCount++;
            yield return "first";
            yield return "last";
        }

        IEnumerable<string> result = Enumerate().Materialize(option);

        Assert.Equal(1, enumerationCount);
        Assert.Equal(["first", "last"], result);
        Assert.Equal(1, enumerationCount);
    }
}
