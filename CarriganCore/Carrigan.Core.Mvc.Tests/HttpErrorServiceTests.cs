using System.Net;
using Carrigan.Core.Exceptions;
using Carrigan.Core.Mvc.Services;
using Carrigan.Core.Mvc.TransferModels;

namespace Carrigan.Core.Mvc.Tests;

public class HttpErrorServiceTests
{
    [Fact]
    public void Create_DefaultArguments_ReturnsServerErrorAndUniqueTrackingCodes()
    {
        HttpErrorService service = new();

        HttpErrorData first = service.Create();
        HttpErrorData second = service.Create();

        Assert.Equal(500, first.HttpStatusCode);
        Assert.Equal("500 Internal Server Error", first.HttpStatusCodeMessage);
        Assert.Contains("contact your system administrator", first.DisplayMessage);
        Assert.True(Guid.TryParse(first.ErrorTrackingCode, out _));
        Assert.NotEqual(first.ErrorTrackingCode, second.ErrorTrackingCode);
    }

    [Fact]
    public void Create_CustomDetailsAndAction_UsesSuppliedMessages()
    {
        HttpErrorService service = new();

        HttpErrorData result = service.Create(HttpStatusCode.Forbidden, "You cannot access this page.", "Sign in.");

        Assert.Equal(403, result.HttpStatusCode);
        Assert.Equal("403 Forbidden", result.HttpStatusCodeMessage);
        Assert.Equal("You cannot access this page. \nSign in.", result.DisplayMessage);
    }

    [Fact]
    public void Create_Exception_TransfersStatusAndUserMessages()
    {
        HttpErrorService service = new();
        HttpStatusCodeException exception = new(HttpStatusCode.NotFound, "Log detail", "Page unavailable", "Return home");

        HttpErrorData result = service.Create(exception);

        Assert.Equal(404, result.HttpStatusCode);
        Assert.Equal("Page unavailable \nReturn home", result.DisplayMessage);
    }

    [Fact]
    public void Create_WhitespaceAction_UsesDefaultAction()
    {
        HttpErrorService service = new();

        HttpErrorData result = service.Create(HttpStatusCode.BadRequest, "Invalid request", "   ");

        Assert.Contains("contact your system administrator", result.DisplayMessage);
    }

    [Fact]
    public void Create_EmptyDetails_UsesStandardReasonPhrase()
    {
        HttpErrorService service = new();

        HttpErrorData result = service.Create(HttpStatusCode.BadRequest, "   ", null);

        Assert.StartsWith("Bad Request", result.DisplayMessage);
    }
}
