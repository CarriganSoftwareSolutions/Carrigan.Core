using Carrigan.Core.Extensions;

namespace Carrigan.Core.Test.Extensions;

//IGNORE SPELLING: uno

public class DictionaryExtensionsTests
{
    [Fact]
    public void Add_SingleEnumerable_AddsKeyValuePairs()
    {
        // Arrange
        Dictionary<int, string> dictionary = [];
        // Prepare a single enumerable of key/value pairs
        IEnumerable<KeyValuePair<int, string>> keyValuePairs =
        [
            new KeyValuePair<int, string>(1, "one"),
            new KeyValuePair<int, string>(2, "two")
        ];

        // Act
        dictionary.Add(keyValuePairs);

        // Assert
        Assert.Equal(2, dictionary.Count);
        Assert.Equal("one", dictionary[1]);
        Assert.Equal("two", dictionary[2]);
    }

    [Fact]
    public void Add_EmptyEnumerable_DoesNothing()
    {
        // Arrange
        Dictionary<int, string> dictionary = [];
        IEnumerable<KeyValuePair<int, string>> keyValuePairs = [];

        // Act
        dictionary.Add(keyValuePairs);

        // Assert
        Assert.Empty(dictionary);
    }

    [Fact]
    public void Add_NoArguments_DoesNothing()
    {
        // Arrange
        Dictionary<int, string> dictionary = new()
        {
            { 1, "one" }
        };

        // Act
        // With no arguments, the params array is empty so nothing is added.
        dictionary.Add();

        // Assert
        Assert.Single(dictionary);
        Assert.Equal("one", dictionary[1]);
    }

    [Fact]
    public void Add_DuplicateKey_ThrowsArgumentException()
    {
        // Arrange
        Dictionary<int, string> dictionary = new()
        {
            { 1, "one" }
        };

        // Attempt to add a key that already exists.
        IEnumerable<KeyValuePair<int, string>> keyValuePairs =
        [
            new KeyValuePair<int, string>(1, "uno")
        ];

        // Act & Assert
        Assert.Throws<ArgumentException>(() => dictionary.Add(keyValuePairs));
    }

    [Fact]
    public void Add_NullDictionary_ThrowsNullReferenceException()
    {
        // Arrange
        Dictionary<int, string>? dictionary = null;
        IEnumerable<KeyValuePair<int, string>> keyValuePairs =
        [
            new KeyValuePair<int, string>(1, "one")
        ];

        // Act & Assert
        // Since the extension method is invoked on a null instance,
        // a NullReferenceException is expected.
        Assert.Throws<NullReferenceException>(() => dictionary!.Add(keyValuePairs));
    }

    [Fact]
    public void Add_SingleKeyValuePair_AddsPair()
    {
        // Arrange
        Dictionary<int, string> dictionary = [];
        KeyValuePair<int, string> singleItem = new(1, "one");

        // Act
        dictionary.Add(singleItem);

        // Assert
        Assert.Single(dictionary);
        Assert.Equal("one", dictionary[1]);
    }

    [Fact]
    public void Add_SingleKeyValuePair_DuplicateKey_ThrowsArgumentException()
    {
        // Arrange
        Dictionary<int, string> dictionary = new()
        {
            { 1, "one" }
        };

        KeyValuePair<int, string> duplicateItem = new(1, "uno");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => dictionary.Add(duplicateItem));
    }

    [Fact]
    public void Add_SingleKeyValuePair_NullDictionary_ThrowsArgumentNullException()
    {
        // Arrange
        Dictionary<int, string>? dictionary = null;
        KeyValuePair<int, string> item = new(1, "one");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => dictionary!.Add(item));
    }

    [Fact]
    public void CollectionExpression_SpreadKeyValuePairs_AddsKeyValuePairs()
    {
        // Arrange
        IEnumerable<KeyValuePair<int, string>> keyValuePairs =
        [
            new(1, "one"),
            new(2, "two")
        ];

        // Act
        Dictionary<int, string> dictionary = [.. keyValuePairs];

        // Assert
        Assert.Equal(2, dictionary.Count);
        Assert.Equal("one", dictionary[1]);
        Assert.Equal("two", dictionary[2]);
    }

    [Fact]
    public void Add_EntireDictionary_AddsAllPairs()
    {
        // Arrange
        Dictionary<int, string> dictionary = [];
        Dictionary<int, string> dictionaryToAdd = new()
        {
            { 1, "one" },
            { 2, "two" },
            { 3, "three" }
        };

        // Act
        // Since Dictionary<TKey, TValue> implements IEnumerable<KeyValuePair<TKey, TValue>>,
        // we can pass the entire dictionary directly.
        dictionary.Add(dictionaryToAdd);

        // Assert
        Assert.Equal(3, dictionary.Count);
        Assert.Equal("one", dictionary[1]);
        Assert.Equal("two", dictionary[2]);
        Assert.Equal("three", dictionary[3]);
    }
}
