using Carrigan.Core.Enums;
using Carrigan.Core.Exceptions;
using Carrigan.Core.Extensions;
using Carrigan.Core.Test.Extensions.ObjectExtensionTestsSupport;

namespace Carrigan.Core.Test.Extensions;

public class IEnumerableMaterializeNullOrWhiteSpaceTests
{
    [Fact]
    public void Materialize_ArgumentNullOrWhiteSpaceException_CapturesCallerExpression()
    {
        IEnumerable<WhiteSpaceTestClass> items = [new("")];

        ArgumentNullOrWhiteSpaceException exception = Assert.Throws<ArgumentNullOrWhiteSpaceException>(
            () => items.Materialize(NullOrWhiteSpaceOptionsEnum.ArgumentException).ToArray());

        Assert.Equal("items", exception.ParamName);
        Assert.Contains("Collection contains disallowed null or white space elements.", exception.Message);
    }

    [Theory]
    [InlineData(NullOrWhiteSpaceOptionsEnum.Allowed)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.FilteredOut)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.Exception)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.ArgumentException)]
    public void Materialize_EmptyCollection_ReturnsEmptyList(NullOrWhiteSpaceOptionsEnum option)
    {
        IEnumerable<WhiteSpaceTestClass> source = [];

        IEnumerable<WhiteSpaceTestClass> result = source.Materialize(option);

        Assert.IsType<IEnumerable<WhiteSpaceTestClass>>(result, false);
        Assert.Empty(result);
    }

    [Theory]
    [InlineData(NullOrWhiteSpaceOptionsEnum.Allowed)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.FilteredOut)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.Exception)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.ArgumentException)]
    public void Materialize_ValidElements_PreservesValuesAndOrder(NullOrWhiteSpaceOptionsEnum option)
    {
        WhiteSpaceTestClass first = new("first");
        WhiteSpaceTestClass second = new("second");
        IEnumerable<WhiteSpaceTestClass> source = [first, second];

        IEnumerable<WhiteSpaceTestClass> result = source.Materialize(option);

        Assert.IsType<IEnumerable<WhiteSpaceTestClass>>(result, false);
        Assert.NotSame(source, result);
        Assert.Equal([first, second], result);
    }

    [Fact]
    public void Materialize_Allowed_PreservesNullEmptyAndWhitespace()
    {
        WhiteSpaceTestClass first = new("first");
        WhiteSpaceTestClass empty = new("");
        WhiteSpaceTestClass whitespace = new(" \t ");
        IEnumerable<WhiteSpaceTestClass> source = [first, null!, empty, whitespace];

        IEnumerable<WhiteSpaceTestClass> result = source.Materialize(NullOrWhiteSpaceOptionsEnum.Allowed);

        Assert.Equal([first, null!, empty, whitespace], result);
    }

    [Fact]
    public void Materialize_FilteredOut_RemovesNullEmptyAndWhitespace()
    {
        WhiteSpaceTestClass first = new("first");
        WhiteSpaceTestClass last = new("last");
        IEnumerable<WhiteSpaceTestClass> source = [null!, first, new(""), new(" "), new("\t"), null!, last];

        IEnumerable<WhiteSpaceTestClass> result = source.Materialize(NullOrWhiteSpaceOptionsEnum.FilteredOut);

        Assert.Equal([first, last], result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\r\n")]
    public void Materialize_NullOrWhiteSpaceException_InvalidElement_ThrowsNullOrWhiteSpaceException(string? invalidValue)
    {
        IEnumerable<WhiteSpaceTestClass> source = [new("valid"), invalidValue is null ? null! : new(invalidValue), new("later")];

        NullOrWhiteSpaceException exception = Assert.Throws<NullOrWhiteSpaceException>(() => source.Materialize(NullOrWhiteSpaceOptionsEnum.Exception, "items").ToArray());

        Assert.Equal("'items' contains disallowed null or white space elements.", exception.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\r\n")]
    public void Materialize_ArgumentNullOrWhiteSpaceException_InvalidElement_ThrowsArgumentNullOrWhiteSpaceException(string? invalidValue)
    {
        IEnumerable<WhiteSpaceTestClass> source = [new("valid"), invalidValue is null ? null! : new(invalidValue), new("later")];

        ArgumentNullOrWhiteSpaceException exception = Assert.Throws<ArgumentNullOrWhiteSpaceException>(() => source.Materialize(NullOrWhiteSpaceOptionsEnum.ArgumentException, "items").ToArray());

        Assert.Equal("items", exception.ParamName);
        Assert.Contains("Collection contains disallowed null or white space elements.", exception.Message);
    }

    [Theory]
    [InlineData(NullOrWhiteSpaceOptionsEnum.Allowed)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.FilteredOut)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.Exception)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.ArgumentException)]
    public void Materialize_NullSource_ThrowsArgumentNullException(NullOrWhiteSpaceOptionsEnum option)
    {
        IEnumerable<WhiteSpaceTestClass>? source = null;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => source!.Materialize(option).ToArray());

        Assert.Equal("enumerable", exception.ParamName);
    }

    [Fact]
    public void Materialize_InvalidOption_ThrowsInvalidOperationException()
    {
        IEnumerable<WhiteSpaceTestClass> source = [new("valid")];
        NullOrWhiteSpaceOptionsEnum option = (NullOrWhiteSpaceOptionsEnum)int.MaxValue;

        Assert.Throws<InvalidOperationException>(() => source.Materialize(option).ToArray());
    }

    [Theory]
    [InlineData(NullOrWhiteSpaceOptionsEnum.Allowed)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.FilteredOut)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.Exception)]
    [InlineData(NullOrWhiteSpaceOptionsEnum.ArgumentException)]
    public void Materialize_ValidElements_EnumeratesSourceOnce(NullOrWhiteSpaceOptionsEnum option)
    {
        int enumerationCount = 0;
        IEnumerable<WhiteSpaceTestClass> Enumerate()
        {
            enumerationCount++;
            yield return new WhiteSpaceTestClass("first");
            yield return new WhiteSpaceTestClass("second");
        }

        IEnumerable<WhiteSpaceTestClass> result = Enumerate().Materialize(option);

        Assert.Equal(1, enumerationCount);
        Assert.Equal(2, result.Count());
        Assert.Equal(1, enumerationCount);
    }
}
