using Carrigan.Core.Enums;
using Carrigan.Core.Exceptions;
using Carrigan.Core.Extensions;
using Carrigan.Core.Test.Extensions.ObjectExtensionTestsSupport;

namespace Carrigan.Core.Test.Extensions;

public class IEnumerableMaterializeStringNullOrEmptyTests
{
    [Theory]
    [InlineData(NullOrEmptyOptionsEnum.Allowed)]
    [InlineData(NullOrEmptyOptionsEnum.FilteredOut)]
    [InlineData(NullOrEmptyOptionsEnum.NullOrEmptyReferenceException)]
    [InlineData(NullOrEmptyOptionsEnum.ArgumentNullOrEmptyException)]
    public void Materialize_EmptyCollection_ReturnsEmptyCollection(NullOrEmptyOptionsEnum option)
    {
        int enumerationCount = 0;
        IEnumerable<string> Enumerate()
        {
            enumerationCount++;
            yield break;
        }

        IEnumerable<string> result = Enumerate().Materialize(option);

        Assert.IsType<IEnumerable<string>>(result, false);
        Assert.Empty(result);
        Assert.Equal(1, enumerationCount);
    }

    [Theory]
    [InlineData(NullOrEmptyOptionsEnum.Allowed)]
    [InlineData(NullOrEmptyOptionsEnum.FilteredOut)]
    [InlineData(NullOrEmptyOptionsEnum.NullOrEmptyReferenceException)]
    [InlineData(NullOrEmptyOptionsEnum.ArgumentNullOrEmptyException)]
    public void Materialize_ValidElements_PreservesOrder(NullOrEmptyOptionsEnum option)
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

        IEnumerable<string> result = source.Materialize(NullOrEmptyOptionsEnum.Allowed);

        Assert.IsType<IEnumerable<string>>(result, false);
        Assert.Equal(source, result);
    }

    [Fact]
    public void Materialize_FilteredOut_RemovesInvalidElements()
    {
        IEnumerable<string> source = [null!, "first", "", " ", "\t", null!, "last"];

        IEnumerable<string> result = source.Materialize(NullOrEmptyOptionsEnum.FilteredOut);

        Assert.Equal(["first", " ", "\t", "last"], result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Materialize_NullOrEmptyReferenceException_InvalidElement_ThrowsExpectedException(string? invalid)
    {
        IEnumerable<string> source = ["first", invalid!, "last"];

        NullOrEmptyException exception = Assert.Throws<NullOrEmptyException>(
            () => source.Materialize(NullOrEmptyOptionsEnum.NullOrEmptyReferenceException, "items").ToArray());

        Assert.Equal("'items' contains disallowed null or empty elements.", exception.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Materialize_ArgumentNullOrEmptyException_InvalidElement_ThrowsExpectedException(string? invalid)
    {
        IEnumerable<string> source = ["first", invalid!, "last"];

        ArgumentNullOrEmptyException exception = Assert.Throws<ArgumentNullOrEmptyException>(
            () => source.Materialize(NullOrEmptyOptionsEnum.ArgumentNullOrEmptyException, "items").ToArray());

        Assert.Equal("items", exception.ParamName);
        Assert.Contains("Collection contains disallowed null or empty elements.", exception.Message);
    }

    [Fact]
    public void Materialize_ArgumentNullOrEmptyException_CapturesCallerExpression()
    {
        IEnumerable<string> items = [""];

        ArgumentNullOrEmptyException exception = Assert.Throws<ArgumentNullOrEmptyException>(
            () => items.Materialize(NullOrEmptyOptionsEnum.ArgumentNullOrEmptyException).ToArray());

        Assert.Equal("items", exception.ParamName);
        Assert.Contains("Collection contains disallowed null or empty elements.", exception.Message);
    }

    [Theory]
    [InlineData(NullOrEmptyOptionsEnum.Allowed)]
    [InlineData(NullOrEmptyOptionsEnum.FilteredOut)]
    [InlineData(NullOrEmptyOptionsEnum.NullOrEmptyReferenceException)]
    [InlineData(NullOrEmptyOptionsEnum.ArgumentNullOrEmptyException)]
    public void Materialize_NullSource_ThrowsArgumentNullException(NullOrEmptyOptionsEnum option)
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
