
using Carrigan.Core.Extensions;
using System.Runtime.CompilerServices;

namespace Carrigan.Core.Exceptions;

/// <summary>
/// The exception that is thrown when a string value is null or empty.
/// </summary>
public class NullOrEmptyException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NullOrEmptyException"/> class.
    /// </summary>
    public NullOrEmptyException()
        : base("Value cannot be null or empty.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NullOrEmptyException"/> class.
    /// </summary>
    /// <param name="expression">The expression that caused the exception.</param>
    public NullOrEmptyException(string? expression)
        : base($"Value '{expression}' cannot be null or empty.")
    {
    }

    /// <summary>
    /// Initializes a new instance with the expression and a description of why it is invalid.
    /// </summary>
    /// <param name="expression">The expression that caused the exception.</param>
    /// <param name="description">The reason the expression is invalid.</param>
    public NullOrEmptyException(string? expression, string description)
        : base($"'{expression}' {description}")
    {
    }

    /// <summary>
    /// Throws a <see cref="NullOrEmptyException"/> if
    /// <paramref name="value"/> is null or empty.
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="expression">The expression being validated.</param>
    /// <exception cref="NullOrEmptyException">
    /// <paramref name="value"/> is null or empty.
    /// </exception>
    public static string ThrowIfNullOrEmpty(string? value, [CallerArgumentExpression(nameof(value))] string? expression = null)
    {
        if (value.IsNullOrEmpty())
            throw new NullOrEmptyException(expression);

        return value;
    }
}
