using Carrigan.Core.Extensions;
using Carrigan.Core.Mvc.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Carrigan.Core.Mvc.Services;

/// <summary>
/// 
/// </summary>
/// <example>
/// builder.Services.AddDataProtection();
/// builder.Services.AddScoped<IRobotTimestampTestService, RobotTimestampTestService>();
/// </example>
/// <example>
//  @inject IRobotTimestampTestService RobotTimestampTestService
//  @{
//      Model.Token = RobotTimestampTestService.CreateToken();
//  }
/// </example>
public sealed class RobotTimestampTestService : IRobotTimestampTestService
{
    private readonly IDataProtector _protector;

    public RobotTimestampTestService(IDataProtectionProvider provider) => 
        _protector = provider.CreateProtector("FormTimestampProtection");

    public string CreateToken()
    {
        string payload = $"{DateTimeOffset.UtcNow.ToString("O", CultureInfo.InvariantCulture)}";
        return _protector.Protect(payload);
    }

    public bool TryValidateToken(string token, int secondsToRespond)
    {
        TimeSpan minAge = new(0, 0, secondsToRespond);

        if (token.IsNotNullOrWhiteSpace())
        {
            try
            {
                string unprotected = _protector.Unprotect(token);


                if (DateTimeOffset.TryParse(unprotected, null, DateTimeStyles.RoundtripKind, out DateTimeOffset timestampUtc))
                {
                    TimeSpan age = DateTimeOffset.UtcNow - timestampUtc;

                    return (age >= minAge);
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}