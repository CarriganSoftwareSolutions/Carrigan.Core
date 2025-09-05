using Carrigan.Core.Extensions;

namespace Carrigan.Core.Test.Extensions;

public class IEnumerableExtensionsTests
{
    #region DoesNotContain Tests

    /// <summary>
    /// Tests the DoesNotContain extension method for collections that do not contain the specified value.
    /// </summary>
    [Theory]
    [InlineData(new[] { 1, 2, 3 }, 4, true)]
    [InlineData(new[] { "apple", "banana", "cherry" }, "date", true)]
    public void DoesNotContain_ValueNotInCollection_ReturnsTrue<T>(T[] collection, T value, bool expected)
    {
        // Arrange
        IEnumerable<T> enumerable = collection;

        // Act
        bool result = enumerable.DoesNotContain(value);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the DoesNotContain extension method for collections that contain the specified value.
    /// </summary>
    [Theory]
    [InlineData(new[] { 1, 2, 3 }, 2, false)]
    [InlineData(new[] { "apple", "banana", "cherry" }, "banana", false)]
    public void DoesNotContain_ValueInCollection_ReturnsFalse<T>(T[] collection, T value, bool expected)
    {
        // Arrange
        IEnumerable<T> enumerable = collection;

        // Act
        bool result = enumerable.DoesNotContain(value);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the DoesNotContain extension method for an empty collection.
    /// </summary>
    [Fact]
    public void DoesNotContain_EmptyCollection_ReturnsTrue()
    {
        // Arrange
        IEnumerable<int> enumerable = [];
        int value = 1;

        // Act
        bool result = enumerable.DoesNotContain(value);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests the DoesNotContain extension method when the collection is null.
    /// </summary>
    [Fact]
    public void DoesNotContain_NullCollection_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<int> enumerable = null;
        int value = 1;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => enumerable!.DoesNotContain(value));
    }

    #endregion

    #region ForEach Tests

    /// <summary>
    /// Tests the ForEach extension method to ensure the action is applied to each element.
    /// </summary>
    [Fact]
    public void ForEach_ValidCollection_ActionExecutedForEachElement()
    {
        // Arrange
        IEnumerable<int> enumerable = [1, 2, 3, 4, 5];
        List<int> result = [];

        // Act
        enumerable.ForEach(item => result.Add(item * 2));

        // Assert
        Assert.Equal([2, 4, 6, 8, 10], result);
    }

    /// <summary>
    /// Tests the ForEach extension method on an empty collection to ensure the action is not executed.
    /// </summary>
    [Fact]
    public void ForEach_EmptyCollection_ActionNotExecuted()
    {
        // Arrange
        IEnumerable<int> enumerable = [];
        bool actionExecuted = false;

        // Act
        enumerable.ForEach(item => actionExecuted = true);

        // Assert
        Assert.False(actionExecuted);
    }

    /// <summary>
    /// Tests the ForEach extension method when the source collection is null.
    /// </summary>
    [Fact]
    public void ForEach_NullCollection_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<int> enumerable = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => enumerable!.ForEach(item => { }));
    }

    /// <summary>
    /// Tests the ForEach extension method when the action is null.
    /// </summary>
    [Fact]
    public void ForEach_NullAction_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<int> enumerable = [1, 2, 3];
        Action<int> action = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => enumerable.ForEach(action!));
    }

    #endregion

    #region EndsWith Tests

    [Fact]
    public void EndsWithTest()
    {
        byte[] data = [1, 2, 3, 4, 5];
        byte[] endsWith = [4, 5];

        Assert.True(data.EndsWith(endsWith));
    }
    [Fact]
    public void EndsWithExactTest()
    {
        byte[] data = [1, 2, 3, 4, 5];
        byte[] endsWith = [1, 2, 3, 4, 5];

        Assert.True(data.EndsWith(endsWith));
    }

    [Fact]
    public void EndsWithOverflowTestA()
    {
        byte[] data = [4, 5];
        byte[] endsWith = [1, 2, 3, 4, 5];

        Assert.False(data.EndsWith(endsWith));
    }

    [Fact]
    public void EndsWithOverflowTestB()
    {
        byte[] data = [1, 2, 3,];
        byte[] endsWith = [1, 2, 3, 4, 5];

        Assert.False(data.EndsWith(endsWith));
    }


    [Fact]
    public void EndsWithNullTest()
    {
        byte[] data = [1, 2];
        byte[] endsWith = [];

        Assert.True(data.EndsWith(endsWith));
    }

    /// <summary>
    /// Tests the EndsWith extension method when the collection ends with the specified postfix.
    /// </summary>
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 4, 5 }, true)]
    [InlineData(new[] { "apple", "banana", "cherry" }, new[] { "banana", "cherry" }, true)]
    [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }, true)]
    public void EndsWith_CollectionEndsWithPostfix_ReturnsTrue<T>(T[] collection, T[] postfix, bool expected)
    {
        // Arrange
        IEnumerable<T> enumerable = collection;

        // Act
        bool result = enumerable.EndsWith(postfix);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the EndsWith extension method when the collection does not end with the specified postfix.
    /// </summary>
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 3, 5 }, false)]
    [InlineData(new[] { "apple", "banana", "cherry" }, new[] { "apple", "cherry" }, false)]
    [InlineData(new[] { 1, 2, 3 }, new[] { 2, 3, 4 }, false)]
    public void EndsWith_CollectionDoesNotEndWithPostfix_ReturnsFalse<T>(T[] collection, T[] postfix, bool expected)
    {
        // Arrange
        IEnumerable<T> enumerable = collection;

        // Act
        bool result = enumerable.EndsWith(postfix);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the EndsWith extension method when the postfix is longer than the collection.
    /// </summary>
    [Theory]
    [InlineData(new[] { 1, 2 }, new[] { 1, 2, 3 }, false)]
    [InlineData(new[] { "apple" }, new[] { "apple", "banana" }, false)]
    public void EndsWith_PostfixLongerThanCollection_ReturnsFalse<T>(T[] collection, T[] postfix, bool expected)
    {
        // Arrange
        IEnumerable<T> enumerable = collection;

        // Act
        bool result = enumerable.EndsWith(postfix);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the EndsWith extension method when the postfix is empty.
    /// </summary>
    [Fact]
    public void EndsWith_EmptyPostfix_ReturnsTrue()
    {
        // Arrange
        IEnumerable<int> enumerable = [1, 2, 3];
        IEnumerable<int> postfix = [];

        // Act
        bool result = enumerable.EndsWith(postfix);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests the EndsWith extension method when the collection is empty.
    /// </summary>
    [Fact]
    public void EndsWith_EmptyCollection_WithNonEmptyPostfix_ReturnsFalse()
    {
        // Arrange
        IEnumerable<int> enumerable = [];
        IEnumerable<int> postfix = [1];

        // Act
        bool result = enumerable.EndsWith(postfix);

        // Assert
        Assert.False(result);
    }

    /// <summary>
    /// Tests the EndsWith extension method when both the collection and postfix are empty.
    /// </summary>
    [Fact]
    public void EndsWith_BothCollectionAndPostfixEmpty_ReturnsTrue()
    {
        // Arrange
        IEnumerable<int> enumerable = [];
        IEnumerable<int> postfix = [];

        // Act
        bool result = enumerable.EndsWith(postfix);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests the EndsWith extension method when the postfix is null.
    /// </summary>
    [Fact]
    public void EndsWith_NullPostfix_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<int> enumerable = [1, 2, 3];
        IEnumerable<int> postfix = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => enumerable.EndsWith(postfix!));
    }

    /// <summary>
    /// Tests the EndsWith extension method when the collection is null.
    /// </summary>
    [Fact]
    public void EndsWith_NullCollection_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<int> enumerable = null;
        IEnumerable<int> postfix = [1];

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => enumerable!.EndsWith(postfix));
    }

    #endregion

    #region StartsWith Tests

    [Fact]
    public void StartsWithTest()
    {
        byte[] data = [1, 2, 3, 4, 5];
        byte[] startsWith = [1, 2];

        Assert.True(data.StartsWith(startsWith));
    }

    [Fact]
    public void StartsWithOverflowTest()
    {
        byte[] data = [1, 2];
        byte[] startsWith = [1, 2, 3, 4, 5];

        Assert.False(data.StartsWith(startsWith));
    }

    [Fact]
    public void StartsWithNullTest()
    {
        byte[] data = [1, 2];
        byte[] startsWith = [];

        Assert.True(data.StartsWith(startsWith));
    }

    [Fact]
    public void StartsWithExactTest()
    {
        byte[] data = [1, 2, 3];
        byte[] startsWith = [1, 2, 3];

        Assert.True(data.StartsWith(startsWith));
    }

    /// <summary>
    /// Tests the StartsWith extension method when the collection starts with the specified prefix.
    /// </summary>
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 1, 2 }, true)]
    [InlineData(new[] { "apple", "banana", "cherry" }, new[] { "apple", "banana" }, true)]
    [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }, true)]
    public void StartsWith_CollectionStartsWithPrefix_ReturnsTrue<T>(T[] collection, T[] prefix, bool expected)
    {
        // Arrange
        IEnumerable<T> enumerable = collection;

        // Act
        bool result = enumerable.StartsWith(prefix);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the StartsWith extension method when the collection does not start with the specified prefix.
    /// </summary>
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 2, 3 }, false)]
    [InlineData(new[] { "apple", "banana", "cherry" }, new[] { "banana", "cherry" }, false)]
    [InlineData(new[] { 1, 2, 3 }, new[] { 2, 3, 4 }, false)]
    public void StartsWith_CollectionDoesNotStartWithPrefix_ReturnsFalse<T>(T[] collection, T[] prefix, bool expected)
    {
        // Arrange
        IEnumerable<T> enumerable = collection;

        // Act
        bool result = enumerable.StartsWith(prefix);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the StartsWith extension method when the prefix is longer than the collection.
    /// </summary>
    [Theory]
    [InlineData(new[] { 1, 2 }, new[] { 1, 2, 3 }, false)]
    [InlineData(new[] { "apple" }, new[] { "apple", "banana" }, false)]
    public void StartsWith_PrefixLongerThanCollection_ReturnsFalse<T>(T[] collection, T[] prefix, bool expected)
    {
        // Arrange
        IEnumerable<T> enumerable = collection;

        // Act
        bool result = enumerable.StartsWith(prefix);

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the StartsWith extension method when the prefix is empty.
    /// </summary>
    [Fact]
    public void StartsWith_EmptyPrefix_ReturnsTrue()
    {
        // Arrange
        IEnumerable<int> enumerable = [1, 2, 3];
        IEnumerable<int> prefix = [];

        // Act
        bool result = enumerable.StartsWith(prefix);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests the StartsWith extension method when the collection is empty.
    /// </summary>
    [Fact]
    public void StartsWith_EmptyCollection_WithNonEmptyPrefix_ReturnsFalse()
    {
        // Arrange
        IEnumerable<int> enumerable = [];
        IEnumerable<int> prefix = [1];

        // Act
        bool result = enumerable.StartsWith(prefix);

        // Assert
        Assert.False(result);
    }

    /// <summary>
    /// Tests the StartsWith extension method when both the collection and prefix are empty.
    /// </summary>
    [Fact]
    public void StartsWith_BothCollectionAndPrefixEmpty_ReturnsTrue()
    {
        // Arrange
        IEnumerable<int> enumerable = [];
        IEnumerable<int> prefix = [];

        // Act
        bool result = enumerable.StartsWith(prefix);

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests the StartsWith extension method when the prefix is null.
    /// </summary>
    [Fact]
    public void StartsWith_NullPrefix_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<int> enumerable = [1, 2, 3];
        IEnumerable<int> prefix = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => enumerable.StartsWith(prefix!));
    }

    /// <summary>
    /// Tests the StartsWith extension method when the collection is null.
    /// </summary>
    [Fact]
    public void StartsWith_NullCollection_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<int> enumerable = null;
        IEnumerable<int> prefix = [1];

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => enumerable!.StartsWith(prefix));
    }

    #endregion

    #region None Tests

    /// <summary>
    /// Tests the None extension method for a collection that has elements.
    /// </summary>
    [Theory]
    [InlineData(new[] { 1 }, false)]
    [InlineData(new[] { "apple", "banana" }, false)]
    public void None_CollectionHasElements_ReturnsFalse<T>(T[] collection, bool expected)
    {
        // Arrange
        IEnumerable<T> enumerable = collection;

        // Act
        bool result = enumerable.None();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the None extension method for an empty collection.
    /// </summary>
    [Fact]
    public void None_EmptyCollection_ReturnsTrue()
    {
        // Arrange
        IEnumerable<int> enumerable = [];

        // Act
        bool result = enumerable.None();

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests the None extension method when the collection is null.
    /// </summary>
    [Fact]
    public void None_NullCollection_ReturnsTrue()
    {
        // Arrange
        IEnumerable<int> enumerable = null;
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => enumerable!.None());
    }

    #endregion

    #region IsNullOrEmpty Tests

    /// <summary>
    /// Tests the IsNullOrEmpty extension method for a collection that has elements.
    /// </summary>
    [Theory]
    [InlineData(new[] { 1 }, false)]
    [InlineData(new[] { "apple", "banana" }, false)]
    public void IsNullOrEmpty_CollectionHasElements_ReturnsFalse<T>(T[] collection, bool expected)
    {
        // Arrange
        IEnumerable<T> enumerable = collection;

        // Act
        bool result = enumerable.IsNullOrEmpty();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the IsNullOrEmpty extension method for an empty collection.
    /// </summary>
    [Fact]
    public void IsNullOrEmpty_EmptyCollection_ReturnsTrue()
    {
        // Arrange
        IEnumerable<int> enumerable = [];

        // Act
        bool result = enumerable.IsNullOrEmpty();

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests the IsNullOrEmpty extension method when the collection is null.
    /// </summary>
    [Fact]
    public void IsNullOrEmpty_NullCollection_ReturnsTrue()
    {
        // Arrange
        IEnumerable<int> enumerable = null;

        // Act
        bool result = enumerable.IsNullOrEmpty();

        // Assert
        Assert.True(result);
    }

    #endregion

    #region IsNotNullOrEmpty Tests

    /// <summary>
    /// Tests the IsNotNullOrEmpty extension method for a collection that has elements.
    /// </summary>
    [Theory]
    [InlineData(new[] { 1 })]
    [InlineData(new[] { true, false })]
    public void IsNotNullOrEmpty_CollectionHasElements_ReturnsTrue<T>(T[] collection)
    {
        // Arrange
        IEnumerable<T> enumerable = collection;

        // Act
        bool result = enumerable.IsNotNullOrEmpty();

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests the IsNotNullOrEmpty extension method for an empty collection.
    /// </summary>
    [Fact]
    public void IsNotNullOrEmpty_EmptyCollection_ReturnsFalse()
    {
        // Arrange
        IEnumerable<int> enumerable = [];

        // Act
        bool result = enumerable.IsNotNullOrEmpty();

        // Assert
        Assert.False(result);
    }

    /// <summary>
    /// Tests the IsNotNullOrEmpty extension method when the collection is null.
    /// </summary>
    [Fact]
    public void IsNotNullOrEmpty_NullCollection_ReturnsFalse()
    {
        // Arrange
        IEnumerable<int> enumerable = null;

        // Act
        bool result = enumerable.IsNotNullOrEmpty();

        // Assert
        Assert.False(result);
    }

    #endregion


}
