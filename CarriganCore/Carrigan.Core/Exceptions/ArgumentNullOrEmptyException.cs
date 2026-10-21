using System.Runtime.CompilerServices;

namespace Carrigan.Core.Exceptions;

/// <summary>
/// The exception that is thrown when a string argument is null or empty.
/// </summary>
public class ArgumentNullOrEmptyException : ArgumentException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArgumentNullOrEmptyException"/> class.
    /// </summary>
    public ArgumentNullOrEmptyException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArgumentNullOrEmptyException"/> class.
    /// </summary>
    /// <param name="paramName">The name of the parameter that caused the exception.</param>
    public ArgumentNullOrEmptyException(string? paramName)
        : base("Value cannot be null or empty.", paramName)
    {
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullOrEmptyException"/> if
    /// <paramref name="argument"/> is null or empty.
    /// </summary>
    /// <param name="argument">The string argument to validate.</param>
    /// <param name="paramName">
    /// The name of the parameter being validated.
    /// </param>
    /// <exception cref="ArgumentNullOrEmptyException">
    /// <paramref name="argument"/> is null or empty.
    /// </exception>
    public static new void ThrowIfNullOrEmpty(string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (string.IsNullOrEmpty(argument))
            throw new ArgumentNullOrEmptyException(paramName);
    }

    /// <summary>
    /// Hides the base method ThrowIfNullOrWhiteSpace, do not call, it throws a NotImplementedException
    /// </summary>
    private static new void ThrowIfNullOrWhiteSpace(string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null) =>
        throw new NotImplementedException();
}