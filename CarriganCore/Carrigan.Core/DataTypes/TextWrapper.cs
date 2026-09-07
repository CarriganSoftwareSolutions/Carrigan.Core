using Carrigan.Core.Extensions;
using Carrigan.Core.Interfaces;

namespace Carrigan.Core.DataTypes;

/// <summary>
/// Base class for strongly typed text wrappers used to give common text
/// contexts (e.g., identifiers, tags, names) a type-safe representation.
/// <para>
/// Equality, ordering, and hashing are performed using the configured
/// <see cref="StringComparison"/> provided at construction.
/// </para>
/// </summary>
public abstract class TextWrapper :
    IComparable<TextWrapper>,
    IEquatable<TextWrapper>,
    IEqualityComparer<TextWrapper>,
    IWhiteSpace,
    IEmpty
{
    protected readonly string _value;
    protected readonly StringComparison _stringComparison;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextWrapper"/> class.
    /// </summary>
    /// <param name="value">The underlying text value (treated as <see cref="string.Empty"/> if <c>null</c>).</param>
    /// <param name="stringComparison">
    /// The comparison mode used for equality, ordering, and hashing.
    /// Defaults to <see cref="StringComparison.Ordinal"/>.
    /// </param>
    protected TextWrapper(string? value, StringComparison stringComparison = StringComparison.Ordinal)
    {
        _stringComparison = stringComparison;
        _value = value ?? string.Empty;
    }

    /// <summary>
    /// Returns the underlying text value of this <see cref="TextWrapper"/>.
    /// </summary>
    public override string ToString() =>
        _value;

    /// <summary>
    /// Compares this instance to another <see cref="TextWrapper"/> and returns a value
    /// indicating the sort order.
    /// </summary>
    /// <param name="other">The other <see cref="TextWrapper"/> to compare.</param>
    /// <returns>
    /// A signed integer indicating relative order: 0 if equal; less than 0 if this instance
    /// precedes <paramref name="other"/>; greater than 0 if it follows.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <paramref name="other"/> uses a different <see cref="StringComparison"/> than this instance.
    /// </exception>
    public int CompareTo(TextWrapper? other)
    {
        if (other is null)
            return 1;

        ThrowIfComparisonMismatch(other);

        return string.Compare(_value, other._value, _stringComparison);
    }

    /// <summary>
    /// Determines whether this <see cref="TextWrapper"/> is equal to another instance
    /// using the configured <see cref="StringComparison"/>.
    /// </summary>
    /// <param name="other">The other <see cref="TextWrapper"/> to compare.</param>
    /// <returns><c>true</c> if both wrap equal text values; otherwise, <c>false</c>.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <paramref name="other"/> uses a different <see cref="StringComparison"/> than this instance.
    /// </exception>
    public bool Equals(TextWrapper? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        ThrowIfComparisonMismatch(other);

        return string.Equals(_value, other._value, _stringComparison);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="obj"/> is a <see cref="TextWrapper"/> equal to this instance;
    /// otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <paramref name="obj"/> is a <see cref="TextWrapper"/> that uses a different
    /// <see cref="StringComparison"/> than this instance.
    /// </exception>
    public override bool Equals(object? obj) =>
        Equals(obj as TextWrapper);

    /// <summary>
    /// Returns a hash code for this <see cref="TextWrapper"/> instance,
    /// consistent with the configured <see cref="StringComparison"/>.
    /// </summary>
    public override int GetHashCode() =>
        _value.GetHashCode(_stringComparison);

    /// <summary>
    /// Determines whether two <see cref="TextWrapper"/> instances are equal
    /// using the configured <see cref="StringComparison"/>.
    /// </summary>
    /// <param name="x">The first <see cref="TextWrapper"/> to compare.</param>
    /// <param name="y">The second <see cref="TextWrapper"/> to compare.</param>
    /// <returns><c>true</c> if both wrap equal text values; otherwise, <c>false</c>.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <paramref name="x"/> or <paramref name="y"/> uses a different
    /// <see cref="StringComparison"/> than this comparer instance.
    /// </exception>
    public bool Equals(TextWrapper? x, TextWrapper? y)
    {
        if (x is null && y is null)
            return true;

        if (x is null || y is null)
            return false;

        ThrowIfComparisonMismatch(x);
        ThrowIfComparisonMismatch(y);

        return string.Equals(x._value, y._value, _stringComparison);
    }

    /// <summary>
    /// Returns a hash code for the specified <see cref="TextWrapper"/> instance,
    /// consistent with the configured <see cref="StringComparison"/>.
    /// </summary>
    /// <param name="obj">The <see cref="TextWrapper"/> for which to compute a hash code.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="obj"/> is <c>null</c>.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <paramref name="obj"/> uses a different <see cref="StringComparison"/> than this comparer instance.
    /// </exception>
    public int GetHashCode(TextWrapper obj)
    {
        ArgumentNullException.ThrowIfNull(obj, nameof(obj));
        ThrowIfComparisonMismatch(obj);

        return obj._value.GetHashCode(_stringComparison);
    }

    /// <summary>
    /// Determines whether two <see cref="TextWrapper"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="TextWrapper"/> to compare.</param>
    /// <param name="right">The second <see cref="TextWrapper"/> to compare.</param>
    /// <returns><c>true</c> if both represent the same value; otherwise, <c>false</c>.</returns>
    /// <remarks>Equivalent to <see cref="Equals(TextWrapper?)"/>.</remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the instances use different <see cref="StringComparison"/> values.
    /// </exception>
    public static bool operator ==(TextWrapper? left, TextWrapper? right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="TextWrapper"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="TextWrapper"/> to compare.</param>
    /// <param name="right">The second <see cref="TextWrapper"/> to compare.</param>
    /// <returns><c>true</c> if they differ; otherwise, <c>false</c>.</returns>
    public static bool operator !=(TextWrapper? left, TextWrapper? right) =>
        !(left == right);

    /// <summary>
    /// Determines whether the underlying text is empty or consists only of white-space characters.
    /// </summary>
    /// <returns><c>true</c> if the underlying text is empty or white space; otherwise, <c>false</c>.</returns>
    public bool IsWhiteSpace() =>
        _value.IsWhiteSpace();

    /// <summary>
    /// Determines whether the underlying text is not empty and not white space.
    /// </summary>
    /// <returns><c>true</c> if the underlying text is neither empty nor white space; otherwise, <c>false</c>.</returns>
    public bool IsNotWhiteSpace() =>
        IsWhiteSpace() is false;

    // <summary>
    /// Determines whether the underlying text is empty.
    /// </summary>
    /// <returns><c>true</c> if the underlying text is empty; otherwise, <c>false</c>.</returns>
    public bool IsEmpty() =>
        _value.IsEmpty();

    /// <summary>
    /// Determines whether the underlying text is not empty.
    /// </summary>
    /// <returns><c>true</c> if the underlying text is not empty; otherwise, <c>false</c>.</returns>
    public bool IsNotEmpty() =>
        IsEmpty() is false;

    private void ThrowIfComparisonMismatch(TextWrapper other)
    {
        if (_stringComparison != other._stringComparison)
        {
            throw new InvalidOperationException(
                $"Cannot compare two {nameof(TextWrapper)} instances because their {nameof(StringComparison)} values differ. " +
                $"Left: {_stringComparison}. Right: {other._stringComparison}.");
        }
    }
}
