using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Carrigan.Core.Mvc.TransferModels;

public sealed class HttpErrorData
{
    public required int HttpStatusCode { get; set; }
    public required string HttpStatusCodeMessage { get; set; }
    public required string DisplayMessage { get; set; }
    public required string ErrorTrackingCode { get; set; }
};
