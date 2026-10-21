using System.Runtime.CompilerServices;

namespace Carrigan.Core.Exceptions;

/// <summary>
/// The exception that is thrown when a string argument is null, empty, or consists only of white-space characters.
/// </summary>
public class ArgumentNullOrWhiteSpaceException : ArgumentException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArgumentNullOrWhiteSpaceException"/> class.
    /// </summary>
    public ArgumentNullOrWhiteSpaceException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArgumentNullOrWhiteSpaceException"/> class.
    /// </summary>
    /// <param name="paramName">The name of the parameter that caused the exception.</param>
    public ArgumentNullOrWhiteSpaceException(string? paramName)
        : base("Value cannot be null, empty, or white space.", paramName)
    {
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullOrWhiteSpaceException"/> if
    /// <paramref name="argument"/> is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="argument">The string argument to validate.</param>
    /// <param name="paramName">
    /// The name of the parameter being validated.
    /// </param>
    /// <exception cref="ArgumentNullOrWhiteSpaceException">
    /// <paramref name="argument"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public static new string ThrowIfNullOrWhiteSpace(string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (string.IsNullOrWhiteSpace(argument))
            throw new ArgumentNullOrWhiteSpaceException(paramName);

        return argument;
    }

    /// <summary>
    /// Hides the base method ThrowIfNullOrEmpty, do not call, it throws a NotImplementedException
    /// </summary>
    private static new void ThrowIfNullOrEmpty(string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null) =>
        throw new NotImplementedException();
}