using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Carrigan.Core.Exceptions;
public class HttpNotFoundException : HttpStatusCodeException
{
    public HttpNotFoundException(string? exceptionMessage = "Resource not found.", string? userMessage = null, string? action = null)
        : base(HttpStatusCode.NotFound, exceptionMessage, userMessage, action)
    {
    }

    public HttpNotFoundException(string exceptionMessage, Exception inner, string? userMessage = null, string? action = null)
        : base(HttpStatusCode.NotFound, exceptionMessage, inner, userMessage, action)
    {
    }

    /// <summary>
    /// Throws an HttpNotFoundException if the provided value is null.
    /// </summary>
    public static void ThrowIfNull<T>([NotNull] T? value, string? exceptionMessage = "Value cannot be null.", string? userMessage = null, string? action = null)
    {
        if (value is null)
        {
            throw new HttpNotFoundException(exceptionMessage, userMessage, action);
        }
    }

    /// <summary>
    /// Throws an HttpNotFoundException if the provided string is null or empty.
    /// </summary>
    public static void ThrowIfNullOrEmpty([NotNull] string? value, string? exceptionMessage = "Value cannot be null.", string? userMessage = null, string? action = null)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new HttpNotFoundException(exceptionMessage, userMessage, action);
        }
    }

    /// <summary>
    /// Throws an HttpNotFoundException if the provided guid is null or empty.
    /// </summary>
    public static void ThrowIfNullOrEmpty([NotNull] Guid? value, string? exceptionMessage = "Value cannot be null.", string? userMessage = null, string? action = null)
    {
        if (value is null || value == Guid.Empty)
        {
            throw new HttpNotFoundException(exceptionMessage, userMessage, action);
        }
    }

    /// <summary>
    /// Throws an HttpNotFoundException if the provided string is null, empty, or consists only of white-space.
    /// </summary>
    public static void ThrowIfNullOrWhiteSpace([NotNull] string? value, string? exceptionMessage = "Value cannot be null.", string? userMessage = null, string? action = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new HttpNotFoundException(exceptionMessage, userMessage, action);
        }
    }
}
