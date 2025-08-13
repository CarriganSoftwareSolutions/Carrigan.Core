using Carrigan.Core.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;


namespace Carrigan.Core.Mvc.Recaptcha;

public class RecaptchaValidation
{
    private IRecaptchaConfiguration _recaptchaConfiguration;
    public RecaptchaValidation(IRecaptchaConfiguration recaptchaConfiguration)
    {
        _recaptchaConfiguration = recaptchaConfiguration;
    }
    public async Task<bool> ValidateV3(ModelStateDictionary modelState, string? token)
    {
        ReCaptchaV3Response? recaptchaResponse = null;

        if (token.IsNotNullOrWhiteSpace())
        {
            // Prepare the verification URL with query parameters
            string verifyUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={_recaptchaConfiguration.PrivateKeyV3}&response={token}";

            using HttpClient client = new();
            HttpResponseMessage response = await client.PostAsync(verifyUrl, null);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize the response using System.Text.Json
            recaptchaResponse = JsonSerializer.Deserialize<ReCaptchaV3Response>(jsonResponse);
        }

        if (token.IsNullOrWhiteSpace() || recaptchaResponse == null || !recaptchaResponse.Success || recaptchaResponse.Score < 0.5)
        {
            // Handle the failure or low score (e.g., show an error message)
            modelState.AddModelError("", "reCAPTCHA verification failed. Please try again.");

            return false;
        }
        else
        {
            return true;
        }
    }
    public async Task<bool> ValidateV2(ModelStateDictionary modelState, string recaptchaResponse)
    {
        // Prepare the verification URL
        string verifyUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={_recaptchaConfiguration.PrivateKeyV2}&response={recaptchaResponse}";

        using HttpClient client = new();
        HttpResponseMessage response = await client.PostAsync(verifyUrl, null);
        string jsonResponse = await response.Content.ReadAsStringAsync();

        // Deserialize the reCAPTCHA verification result using System.Text.Json
        RecaptchaV2Response? recaptchaResult = JsonSerializer.Deserialize<RecaptchaV2Response>(jsonResponse);

        if (recaptchaResult == null || !recaptchaResult.Success)
        {
            // Add an error message if reCAPTCHA validation fails
            modelState.AddModelError("", "Please verify that you're not a robot.");
        }
        else
        {
            return true;
        }

        return false;
    }
}