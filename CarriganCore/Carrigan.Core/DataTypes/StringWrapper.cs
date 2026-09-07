using Carrigan.Core.Extensions;
using Carrigan.Core.Interfaces;

namespace Carrigan.Core.DataTypes;

/// <summary>
/// Base class for strongly typed string wrappers used to give common string
/// contexts (e.g., identifiers, tags, names) a type-safe representation.
/// <para>
/// Equality, ordering, and hashing are performed using the configured
/// <see cref="StringComparison"/> provided at construction.
/// </para>
/// </summary>
public abstract class StringWrapper : 
    TextWrapper,
    IComparable<StringWrapper>,
    IEquatable<StringWrapper>,
    IEqualityComparer<StringWrapper>,
    IWhiteSpace
{

    /// <summary>
    /// Initializes a new instance of the <see cref="StringWrapper"/> class.
    /// </summary>
    /// <param name="value">The underlying string value (treated as <see cref="string.Empty"/> if <c>null</c>).</param>
    /// <param name="stringComparison">
    /// The comparison mode used for equality, ordering, and hashing.
    /// Defaults to <see cref="StringComparison.Ordinal"/>.
    /// </param>
    protected StringWrapper(string? value, StringComparison stringComparison = StringComparison.Ordinal) : base (value, stringComparison)
    {
    }

    /// <summary>
    /// Implicitly converts a <see cref="StringWrapper"/> to its underlying string value.
    /// </summary>
    /// <param name="stringWrapper">The <see cref="StringWrapper"/> to convert.</param>
    /// <returns>The wrapped string value, or <see cref="string.Empty"/> if <paramref name="stringWrapper"/> is <c>null</c>.</returns>
    public static implicit operator string(StringWrapper stringWrapper)
    {
        return stringWrapper?._value ?? string.Empty;
    }

    /// <summary>
    /// Compares this instance to another <see cref="StringWrapper"/> and returns a value
    /// indicating the sort order.
    /// </summary>
    /// <param name="other">The other <see cref="StringWrapper"/> to compare.</param>
    /// <returns>
    /// A signed integer indicating relative order: 0 if equal; less than 0 if this instance
    /// precedes <paramref name="other"/>; greater than 0 if it follows.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <paramref name="other"/> uses a different <see cref="StringComparison"/> than this instance.
    /// </exception>
    public int CompareTo(StringWrapper? other) =>
        base.CompareTo(other);

    /// <summary>
    /// Determines whether this <see cref="StringWrapper"/> is equal to another instance
    /// using the configured <see cref="StringComparison"/>.
    /// </summary>
    /// <param name="other">The other <see cref="StringWrapper"/> to compare.</param>
    /// <returns><c>true</c> if both wrap equal string values; otherwise, <c>false</c>.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <paramref name="other"/> uses a different <see cref="StringComparison"/> than this instance.
    /// </exception>
    public bool Equals(StringWrapper? other) =>
        base.Equals(other);

    /// <summary>
    /// Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="obj"/> is a <see cref="StringWrapper"/> equal to this instance;
    /// otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <paramref name="obj"/> is a <see cref="StringWrapper"/> that uses a different
    /// <see cref="StringComparison"/> than this instance.
    /// </exception>
    public override bool Equals(object? obj) =>
        base.Equals(obj);

    /// <summary>
    /// Returns a hash code for this <see cref="StringWrapper"/> instance,
    /// consistent with the configured <see cref="StringComparison"/>.
    /// </summary>
    public override int GetHashCode() =>
        base.GetHashCode();

    /// <summary>
    /// Determines whether two <see cref="StringWrapper"/> instances are equal
    /// using the configured <see cref="StringComparison"/>.
    /// </summary>
    /// <param name="x">The first <see cref="StringWrapper"/> to compare.</param>
    /// <param name="y">The second <see cref="StringWrapper"/> to compare.</param>
    /// <returns><c>true</c> if both wrap equal string values; otherwise, <c>false</c>.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <paramref name="x"/> or <paramref name="y"/> uses a different
    /// <see cref="StringComparison"/> than this comparer instance.
    /// </exception>
    public bool Equals(StringWrapper? x, StringWrapper? y) =>
        base.Equals(x, y);

    /// <summary>
    /// Returns a hash code for the specified <see cref="StringWrapper"/> instance,
    /// consistent with the configured <see cref="StringComparison"/>.
    /// </summary>
    /// <param name="obj">The <see cref="StringWrapper"/> for which to compute a hash code.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="obj"/> is <c>null</c>.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <paramref name="obj"/> uses a different <see cref="StringComparison"/> than this comparer instance.
    /// </exception>
    public int GetHashCode(StringWrapper obj) =>
        base.GetHashCode(obj);

    /// <summary>
    /// Determines whether two <see cref="StringWrapper"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="StringWrapper"/> to compare.</param>
    /// <param name="right">The second <see cref="StringWrapper"/> to compare.</param>
    /// <returns><c>true</c> if both represent the same value; otherwise, <c>false</c>.</returns>
    /// <remarks>Equivalent to <see cref="Equals(StringWrapper?)"/>.</remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the instances use different <see cref="StringComparison"/> values.
    /// </exception>
    public static bool operator ==(StringWrapper? left, StringWrapper? right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="StringWrapper"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="StringWrapper"/> to compare.</param>
    /// <param name="right">The second <see cref="StringWrapper"/> to compare.</param>
    /// <returns><c>true</c> if they differ; otherwise, <c>false</c>.</returns>
    public static bool operator !=(StringWrapper? left, StringWrapper? right) =>
        !(left == right);
}
