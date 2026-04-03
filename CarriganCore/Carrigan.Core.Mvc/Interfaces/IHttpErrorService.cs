using Carrigan.Core.Exceptions;
using Carrigan.Core.Mvc.TransferModels;
using System.Net;

namespace Carrigan.Core.Mvc.Interfaces;


public interface IHttpErrorService
{
    HttpErrorData Create
    (
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError,
        string? additionalDetails = null,
        string? action = null
    );

    HttpErrorData Create(HttpStatusCodeException ex);
}
