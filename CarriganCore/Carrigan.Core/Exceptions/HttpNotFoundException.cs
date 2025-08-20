using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Carrigan.Core.Exceptions;
/// <summary>
/// This class will likely have little value to others, but was part the core library being open sourced.
/// This just provides a simply way to set up error messages in .Net web applications, when used in conjunction with internal error handlers.
/// This one is specifically for 404 errors.
/// Note: there is a closed source method that parses the exception into a log message and error page message, which maybe released as open source in future.
/// </summary>
public class HttpNotFoundException : HttpStatusCodeException
{
    /// <summary>
    /// Constructor for the HttpNotFoundException class.
    /// </summary>
    /// <param name="exceptionMessage">this is the message to use in the log file</param>
    /// <param name="userMessage">this is the message to display to the user</param>
    /// <param name="action">this is suggested course of action for a user to take after getting the error</param>
    public HttpNotFoundException(string? exceptionMessage = "Resource not found.", string? userMessage = null, string? action = null)
        : base(HttpStatusCode.NotFound, exceptionMessage, userMessage, action)
    {
    }

    /// <summary>
    /// 
    /// Constructor for the HttpNotFoundException class. Also has an inner exception
    /// </summary>
    /// <param name="exceptionMessage">this is the message to use in the log file</param>
    /// <param name="inner">inner exception</param>
    /// <param name="userMessage">message to display to the user</param>
    /// <param name="action">this is suggested course of action for a user to take after getting the error</param>
    public HttpNotFoundException(string exceptionMessage, Exception inner, string? userMessage = null, string? action = null)
        : base(HttpStatusCode.NotFound, exceptionMessage, inner, userMessage, action)
    {
    }


    /// <summary>
    /// Throws an HttpNotFoundException if the provided value is null.
    /// Similar to ArgumentNullException's throw if null static method. It provides a simple one liner for throwing when null.
    /// </summary>
    /// <typeparam name="T">the Type of the value</typeparam>
    /// <param name="value">the value we are check to see if is null</param>
    /// <param name="exceptionMessage">this is the message to use in the log file</param>
    /// <param name="userMessage">this is the message to display to the user</param>
    /// <param name="action">this is suggested course of action for a user to take after getting the error</param>
    /// <exception cref="HttpNotFoundException"></exception>
    public static void ThrowIfNull<T>([NotNull] T? value, string? exceptionMessage = "Value cannot be null.", string? userMessage = null, string? action = null)
    {
        if (value is null)
        {
            throw new HttpNotFoundException(exceptionMessage, userMessage, action);
        }
    }

    /// <summary>
    /// Throws an HttpNotFoundException if the provided string value is null or empty.
    /// Similar to ArgumentNullException's throw if null static method. It provides a simple one liner for throwing when null or empty.
    /// </summary>
    /// <param name="value">the value we are check to see if is null</param>
    /// <param name="exceptionMessage">this is the message to use in the log file</param>
    /// <param name="userMessage">this is the message to display to the user</param>
    /// <param name="action">this is suggested course of action for a user to take after getting the error</param>
    /// <exception cref="HttpNotFoundException"></exception>
    public static void ThrowIfNullOrEmpty([NotNull] string? value, string? exceptionMessage = "Value cannot be null.", string? userMessage = null, string? action = null)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new HttpNotFoundException(exceptionMessage, userMessage, action);
        }
    }

    /// <summary>
    /// Throws an HttpNotFoundException if the provided guid value is null or empty.
    /// Similar to ArgumentNullException's throw if null static method. It provides a simple one liner for throwing when null or empty.
    /// </summary>
    /// <param name="value">the value we are check to see if is null</param>
    /// <param name="exceptionMessage">this is the message to use in the log file</param>
    /// <param name="userMessage">this is the message to display to the user</param>
    /// <param name="action">this is suggested course of action for a user to take after getting the error</param>
    /// <exception cref="HttpNotFoundException"></exception>
    public static void ThrowIfNullOrEmpty([NotNull] Guid? value, string? exceptionMessage = "Value cannot be null.", string? userMessage = null, string? action = null)
    {
        if (value is null || value == Guid.Empty)
        {
            throw new HttpNotFoundException(exceptionMessage, userMessage, action);
        }
    }

    /// <summary>
    /// Throws an HttpNotFoundException if the provided string value is null, empty or whitespace.
    /// Similar to ArgumentNullException's throw if null static method. It provides a simple one liner for throwing when null, empty or whitespace.
    /// </summary>
    /// <param name="value">the value we are check to see if is null</param>
    /// <param name="exceptionMessage">this is the message to use in the log file</param>
    /// <param name="userMessage">this is the message to display to the user</param>
    /// <param name="action">this is suggested course of action for a user to take after getting the error</param>
    /// <exception cref="HttpNotFoundException"></exception>
    public static void ThrowIfNullOrWhiteSpace([NotNull] string? value, string? exceptionMessage = "Value cannot be null.", string? userMessage = null, string? action = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new HttpNotFoundException(exceptionMessage, userMessage, action);
        }
    }
}
