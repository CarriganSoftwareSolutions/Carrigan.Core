using System.Text.Json.Serialization;
namespace Carrigan.Core.Mvc.Recaptcha;
//ignore spelling: captcha

/// <remarks>
/// This helper assists with technical integration only.
/// Application developers remain responsible for ensuring their implementation
/// complies with Google's reCAPTCHA terms, branding, disclosure, privacy,
/// cookie, and notice requirements.
///
/// If a consuming application hides the reCAPTCHA badge, the application should
/// still provide any disclosure text and related notices required by Google.
///
/// This library does not determine whether a specific implementation satisfies
/// legal, contractual, policy, accessibility, privacy, or user-interface requirements.
/// </remarks>
public class ReCaptchaV3Response
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public ReCaptchaV3Response() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("score")]
    public float Score { get; set; }

    [JsonPropertyName("action")]
    public string Action { get; set; }

    [JsonPropertyName("challenge_ts")]
    public string ChallengeTimeStamp { get; set; }

    [JsonPropertyName("hostname")]
    public string HostName { get; set; }

    [JsonPropertyName("error-codes")]
    public string[] ErrorCodes { get; set; }
}