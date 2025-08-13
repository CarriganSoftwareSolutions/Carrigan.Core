using System.Text.Json.Serialization;

namespace Carrigan.Core.Mvc.Recaptcha;

public class RecaptchaV2Response
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public RecaptchaV2Response() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("challenge_ts")]
    public string ChallengeTimeStamp { get; set; }

    [JsonPropertyName("hostname")]
    public string HostName { get; set; }

    [JsonPropertyName("error-codes")]
    public string[] ErrorCodes { get; set; }
}
