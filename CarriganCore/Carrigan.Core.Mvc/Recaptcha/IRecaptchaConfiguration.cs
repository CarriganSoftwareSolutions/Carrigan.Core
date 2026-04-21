namespace Carrigan.Core.Mvc.Recaptcha;

/// <summary>
/// Interface for <see cref="RecaptchaValidation"/>, defines key settings needed for the class.
/// </summary>
/// /// <remarks>
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
public interface IRecaptchaConfiguration
{
    public string SiteKeyV2 { get; }
    public string PrivateKeyV2 { get; }
    public string SiteKeyV3 { get; }
    public string PrivateKeyV3 { get; }

}
