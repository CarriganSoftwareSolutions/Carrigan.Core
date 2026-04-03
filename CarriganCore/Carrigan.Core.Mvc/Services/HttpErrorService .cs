
using Carrigan.Core.Exceptions;
using Carrigan.Core.Mvc.Interfaces;
using Carrigan.Core.Mvc.TransferModels;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace Carrigan.Core.Mvc.Services;

public sealed class HttpErrorService : IHttpErrorService
{
    /// <summary>
    /// Creates a new instance of the HttpErrorData class representing an HTTP error with the specified status code,
    /// details, and recommended action.
    /// </summary>
    /// <remarks>The returned object includes a unique tracking code for error correlation. The DisplayMessage
    /// property combines the additional details and action message for user display.</remarks>
    /// <param name="httpStatusCode">The HTTP status code to associate with the error. The default is InternalServerError (500).</param>
    /// <param name="additionalDetails">Optional. Additional details describing the error. If null, the standard reason phrase for the status code is
    /// used.</param>
    /// <param name="action">Optional. A recommended action for the user or administrator. If null, a default message is provided.</param>
    /// <returns>A new HttpErrorData instance containing the specified error information, including a unique error tracking code.</returns>
    /// <example>
    /// Registering the HttpErrorService in the dependency injection container:
    /// builder.Services.AddScoped<IHttpErrorService, HttpErrorService>();
    /// </example>
    public HttpErrorData Create
    (
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError,
        string? additionalDetails = null,
        string? action = null
    )
    {
        string errorTrackingCode = Guid.CreateVersion7().ToString();

        additionalDetails ??= ReasonPhrases.GetReasonPhrase((int)httpStatusCode);
        action ??= "If the problem persists, contact your system administrator.";

        return new HttpErrorData
        {
            HttpStatusCode = (int)httpStatusCode,
            HttpStatusCodeMessage = $"{(int)httpStatusCode} {ReasonPhrases.GetReasonPhrase((int)httpStatusCode)}",
            DisplayMessage = $"{additionalDetails} \n{action}",
            ErrorTrackingCode = errorTrackingCode
        };
    }

    public HttpErrorData Create(HttpStatusCodeException ex) =>
        Create(ex.StatusCode, ex.UserMessage, ex.Action);
}