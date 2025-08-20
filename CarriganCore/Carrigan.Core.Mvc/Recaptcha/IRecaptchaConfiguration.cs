namespace Carrigan.Core.Mvc.Recaptcha;

/// <summary>
/// Interface for <see cref="RecaptchaValidation"/>, defines key settings needed for the class.
/// </summary>
public interface IRecaptchaConfiguration
{
    public string SiteKeyV2 { get; }
    public string PrivateKeyV2 { get; }
    public string SiteKeyV3 { get;  }
    public string PrivateKeyV3 { get;  }

}
