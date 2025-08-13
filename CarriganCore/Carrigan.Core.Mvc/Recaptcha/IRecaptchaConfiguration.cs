namespace Carrigan.Core.Mvc.Recaptcha;

public interface IRecaptchaConfiguration
{
    public string SiteKeyV2 { get; }
    public string PrivateKeyV2 { get; }
    public string SiteKeyV3 { get;  }
    public string PrivateKeyV3 { get;  }

}
