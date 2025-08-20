using System.Net;

namespace Carrigan.Core.Exceptions;

/// <summary>
/// This class will likely have little value to others, but was part the core library being open sourced.
/// This just provides a simply way to set up error messages in .Net web applications, when used in conjunction with internal error handlers.
/// Note: there is a closed source method that parses the exception into a log message and error page message, which maybe released as open source in future.
/// </summary>
public class HttpStatusCodeException : Exception
{
    /// <summary>
    /// The HTTP Status Code
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Message to display to the user
    /// </summary>
    public string? UserMessage { get; } = null;
    /// <summary>
    /// Action to display to the user
    /// </summary>
    public string? Action { get; } = null;

    /// <summary>
    /// Constructor for HttpStatusCodeException
    /// </summary>
    /// <param name="statusCode">HTTP Status Code</param>
    /// <param name="exceptionMessage">this is the message to use in the log file</param>
    /// <param name="userMessage">this is the message to display to the user</param>
    /// <param name="action">this is suggested course of action for a user to take after getting the error</param>
    public HttpStatusCodeException(HttpStatusCode statusCode, string? exceptionMessage = null, string? userMessage = null, string? action = null)
        : base(exceptionMessage ?? $"HTTP Status Code: {(int)statusCode}")
    {
        StatusCode = statusCode;
        UserMessage = userMessage;
        Action = action;
    }

    /// <summary>
    /// Constructor for HttpStatusCodeException
    /// </summary>
    /// <param name="statusCode">HTTP Status Code</param>
    /// <param name="exceptionMessage">this is the message to use in the log file</param>
    /// <param name="inner">inner exception</param>
    /// <param name="userMessage">this is the message to display to the user</param>
    /// <param name="action">this is suggested course of action for a user to take after getting the error</param>
    public HttpStatusCodeException(HttpStatusCode statusCode, string exceptionMessage, Exception inner, string? userMessage = null, string? action = null)
        : base(exceptionMessage ?? $"HTTP Status Code: {(int)statusCode}", inner)
    {
        StatusCode = statusCode;
        UserMessage = userMessage;
        Action = action;
    }
}
