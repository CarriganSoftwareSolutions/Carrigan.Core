using System.Text.Json;
using Carrigan.Core.Mvc.Recaptcha;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Carrigan.Core.Mvc.Tests;
//ignore spelling: signup Deserializes hostname json captcha
public class RecaptchaTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public async Task ValidateV3_MissingToken_AddsModelStateErrorWithoutSendingRequest(string? token)
    {
        RecaptchaValidation validator = new(new Configuration());
        ModelStateDictionary modelState = new();

        bool result = await validator.ValidateV3(modelState, token);

        Assert.False(result);
        Assert.False(modelState.IsValid);
        Assert.Contains("reCAPTCHA verification failed", modelState[string.Empty]!.Errors[0].ErrorMessage);
    }

    [Fact]
    public void ReCaptchaV3Response_DeserializesGoogleFieldNames()
    {
        const string json = """{"success":true,"score":0.8,"action":"signup","challenge_ts":"now","hostname":"example.com","error-codes":[]}""";

        ReCaptchaV3Response? result = JsonSerializer.Deserialize<ReCaptchaV3Response>(json);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(0.8f, result.Score);
        Assert.Equal("signup", result.Action);
        Assert.Equal("example.com", result.HostName);
        Assert.Empty(result.ErrorCodes);
    }

    [Fact]
    public void RecaptchaV2Response_DeserializesGoogleFieldNames()
    {
        const string json = """{"success":false,"challenge_ts":"now","hostname":"example.com","error-codes":["invalid-input-response"]}""";

        RecaptchaV2Response? result = JsonSerializer.Deserialize<RecaptchaV2Response>(json);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("now", result.ChallengeTimeStamp);
        Assert.Equal("invalid-input-response", Assert.Single(result.ErrorCodes));
    }

    private sealed class Configuration : IRecaptchaConfiguration
    {
        public string SiteKeyV2 => "not-used";
        public string PrivateKeyV2 => "not-used";
        public string SiteKeyV3 => "not-used";
        public string PrivateKeyV3 => "not-used";
    }
}
