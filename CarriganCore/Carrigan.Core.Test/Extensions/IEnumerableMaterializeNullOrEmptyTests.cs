using Carrigan.Core.Enums;
using Carrigan.Core.Exceptions;
using Carrigan.Core.Extensions;
using Carrigan.Core.Test.Extensions.ObjectExtensionTestsSupport;

namespace Carrigan.Core.Test.Extensions;

public class IEnumerableMaterializeNullOrEmptyTests
{
    [Theory]
    [InlineData(NullOrEmptyOptionsEnum.Allowed)]
    [InlineData(NullOrEmptyOptionsEnum.FilteredOut)]
    [InlineData(NullOrEmptyOptionsEnum.NullOrEmptyReferenceException)]
    [InlineData(NullOrEmptyOptionsEnum.ArgumentNullOrEmptyException)]
    public void Materialize_EmptyCollection_ReturnsEmptyList(NullOrEmptyOptionsEnum option)
    {
        IEnumerable<WhiteSpaceTestClass> source = [];

        IEnumerable<WhiteSpaceTestClass> result = source.Materialize(option);

        Assert.IsType<IEnumerable<WhiteSpaceTestClass>>(result, false);
        Assert.Empty(result);
    }

    [Theory]
    [InlineData(NullOrEmptyOptionsEnum.Allowed)]
    [InlineData(NullOrEmptyOptionsEnum.FilteredOut)]
    [InlineData(NullOrEmptyOptionsEnum.NullOrEmptyReferenceException)]
    [InlineData(NullOrEmptyOptionsEnum.ArgumentNullOrEmptyException)]
    public void Materialize_ValidElements_PreservesValuesAndOrder(NullOrEmptyOptionsEnum option)
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

        IEnumerable<WhiteSpaceTestClass> result = source.Materialize(NullOrEmptyOptionsEnum.Allowed);

        Assert.Equal([first, null!, empty, whitespace], result);
    }

    [Fact]
    public void Materialize_FilteredOut_RemovesNullAndEmptyButKeepsWhitespace()
    {
        WhiteSpaceTestClass first = new("first");
        WhiteSpaceTestClass last = new("last");
        WhiteSpaceTestClass whitespace = new(" ");
        IEnumerable<WhiteSpaceTestClass> source = [null!, first, new(""), whitespace, null!, last];

        IEnumerable<WhiteSpaceTestClass> result = source.Materialize(NullOrEmptyOptionsEnum.FilteredOut);

        Assert.Equal([first, whitespace, last], result);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Materialize_NullOrEmptyReferenceException_InvalidElement_ThrowsNullOrEmptyException(bool isNull)
    {
        IEnumerable<WhiteSpaceTestClass> source = [new("valid"), isNull ? null! : new(""), new("later")];

        NullOrEmptyException exception = Assert.Throws<NullOrEmptyException>(() => source.Materialize(NullOrEmptyOptionsEnum.NullOrEmptyReferenceException, "items").ToArray());

        Assert.Contains("items contains disallowed null or empty", exception.Message);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Materialize_ArgumentNullOrEmptyException_InvalidElement_ThrowsArgumentNullOrEmptyException(bool isNull)
    {
        IEnumerable<WhiteSpaceTestClass> source = [new("valid"), isNull ? null! : new(""), new("later")];

        ArgumentNullOrEmptyException exception = Assert.Throws<ArgumentNullOrEmptyException>(() => source.Materialize(NullOrEmptyOptionsEnum.ArgumentNullOrEmptyException, "items").ToArray());

        Assert.Equal("items contains disallowed null or empty", exception.ParamName);
    }

    [Theory]
    [InlineData(NullOrEmptyOptionsEnum.Allowed)]
    [InlineData(NullOrEmptyOptionsEnum.FilteredOut)]
    [InlineData(NullOrEmptyOptionsEnum.NullOrEmptyReferenceException)]
    [InlineData(NullOrEmptyOptionsEnum.ArgumentNullOrEmptyException)]
    public void Materialize_NullSource_ThrowsArgumentNullException(NullOrEmptyOptionsEnum option)
    {
        IEnumerable<WhiteSpaceTestClass>? source = null;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => source!.Materialize(option).ToArray());

        Assert.Equal("enumerable", exception.ParamName);
    }

    [Fact]
    public void Materialize_InvalidOption_ThrowsInvalidOperationException()
    {
        IEnumerable<WhiteSpaceTestClass> source = [new("valid")];
        NullOrEmptyOptionsEnum option = (NullOrEmptyOptionsEnum)int.MaxValue;

        Assert.Throws<InvalidOperationException>(() => source.Materialize(option).ToArray());
    }

    [Theory]
    [InlineData(NullOrEmptyOptionsEnum.Allowed)]
    [InlineData(NullOrEmptyOptionsEnum.FilteredOut)]
    [InlineData(NullOrEmptyOptionsEnum.NullOrEmptyReferenceException)]
    [InlineData(NullOrEmptyOptionsEnum.ArgumentNullOrEmptyException)]
    public void Materialize_ValidElements_EnumeratesSourceOnce(NullOrEmptyOptionsEnum option)
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
