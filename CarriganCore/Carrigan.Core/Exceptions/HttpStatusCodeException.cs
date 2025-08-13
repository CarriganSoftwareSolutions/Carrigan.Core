using System.Net;

namespace Carrigan.Core.Exceptions;

public class HttpStatusCodeException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public string? UserMessage { get; } = null;
    public string? Action { get; } = null;

    public HttpStatusCodeException(HttpStatusCode statusCode, string? exceptionMessage = null, string? userMessage = null, string? action = null)
        : base(exceptionMessage ?? $"HTTP Status Code: {(int)statusCode}")
    {
        StatusCode = statusCode;
        UserMessage = userMessage;
        Action = action;
    }
    public HttpStatusCodeException(HttpStatusCode statusCode, string exceptionMessage, Exception inner, string? userMessage = null, string? action = null)
        : base(exceptionMessage ?? $"HTTP Status Code: {(int)statusCode}", inner)
    {
        StatusCode = statusCode;
        UserMessage = userMessage;
        Action = action;
    }
}
