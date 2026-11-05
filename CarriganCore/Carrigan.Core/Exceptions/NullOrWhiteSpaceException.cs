
using Carrigan.Core.Extensions;
using System.Runtime.CompilerServices;

namespace Carrigan.Core.Exceptions;

/// <summary>
/// The exception that is thrown when a string value is null, empty, or consists only of white-space characters.
/// </summary>
public class NullOrWhiteSpaceException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NullOrWhiteSpaceException"/> class.
    /// </summary>
    public NullOrWhiteSpaceException()
        : base("Value cannot be null, empty, or white space.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NullOrWhiteSpaceException"/> class.
    /// </summary>
    /// <param name="expression">The expression that caused the exception.</param>
    public NullOrWhiteSpaceException(string? expression)
        : base($"Value '{expression}' cannot be null, empty, or white space.")
    {
    }

    /// <summary>
    /// Throws a <see cref="NullOrWhiteSpaceException"/> if
    /// <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="expression">The expression being validated.</param>
    /// <exception cref="NullOrWhiteSpaceException">
    /// <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public static string ThrowIfNullOrWhiteSpace(string? value, [CallerArgumentExpression(nameof(value))] string? expression = null)
    {
        if (value.IsNullOrWhiteSpace())
            throw new NullOrWhiteSpaceException(expression);

        return value;
    }
}
