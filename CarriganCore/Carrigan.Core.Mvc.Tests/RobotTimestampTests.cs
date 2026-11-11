using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Carrigan.Core.Mvc.Interfaces;
using Carrigan.Core.Mvc.Services;
using Carrigan.Core.Mvc.Validators;
using Microsoft.AspNetCore.DataProtection;

namespace Carrigan.Core.Mvc.Tests;

public class RobotTimestampTests
{
    [Fact]
    public void CreateToken_ImmediatelyValidWithZeroMinimumDelay()
    {
        RobotTimestampTestService service = CreateService();

        string token = service.CreateToken();

        Assert.NotEmpty(token);
        Assert.True(service.TryValidateToken(token, 0, 30));
    }

    [Fact]
    public void TryValidateToken_RejectsEmptyAndMalformedTokens()
    {
        RobotTimestampTestService service = CreateService();

        Assert.False(service.TryValidateToken(string.Empty, 0, 30));
        Assert.False(service.TryValidateToken("not-a-protected-token", 0, 30));
    }

    [Fact]
    public void TryValidateToken_RejectsExpiredAndFutureTimestamps()
    {
        EphemeralDataProtectionProvider provider = new();
        IDataProtector protector = provider.CreateProtector("FormTimestampProtection");
        RobotTimestampTestService service = new(provider);
        string expired = protector.Protect(DateTimeOffset.UtcNow.AddHours(-1).ToString("O", CultureInfo.InvariantCulture));
        string future = protector.Protect(DateTimeOffset.UtcNow.AddHours(1).ToString("O", CultureInfo.InvariantCulture));

        Assert.False(service.TryValidateToken(expired, 0, 30));
        Assert.False(service.TryValidateToken(future, 0, 30));
    }

    [Fact]
    public void TryValidateToken_RejectsPayloadProtectedForDifferentPurpose()
    {
        EphemeralDataProtectionProvider provider = new();
        RobotTimestampTestService service = new(provider);
        string token = provider.CreateProtector("DifferentPurpose").Protect(DateTimeOffset.UtcNow.ToString("O", CultureInfo.InvariantCulture));

        Assert.False(service.TryValidateToken(token, 0, 30));
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public void RobotTimestampTestAttribute_UsesRegisteredValidationService(bool tokenAccepted, bool expected)
    {
        FakeRobotTimestampService service = new(tokenAccepted);
        ServiceProvider provider = new(service);
        RobotTimestampTestAttribute attribute = new(5, 20);
        ValidationContext context = new(new object(), provider, null);

        ValidationResult? result = attribute.GetValidationResult("token", context);

        Assert.Equal(expected, result == ValidationResult.Success);
        Assert.Equal("token", service.LastToken);
        Assert.Equal(5, service.LastMinimumSeconds);
        Assert.Equal(20, service.LastMaximumMinutes);
    }

    [Fact]
    public void RobotTimestampTestAttribute_MissingService_ThrowsInvalidOperationException()
    {
        RobotTimestampTestAttribute attribute = new(5);
        ValidationContext context = new(new object());

        Assert.Throws<InvalidOperationException>(() => attribute.GetValidationResult("token", context));
    }

    private static RobotTimestampTestService CreateService() =>
        new(new EphemeralDataProtectionProvider());

    private sealed class ServiceProvider(IRobotTimestampTestService service) : IServiceProvider
    {
        public object? GetService(Type serviceType) =>
            serviceType == typeof(IRobotTimestampTestService) ? service : null;
    }

    private sealed class FakeRobotTimestampService(bool accepted) : IRobotTimestampTestService
    {
        public string? LastToken { get; private set; }
        public int LastMinimumSeconds { get; private set; }
        public int LastMaximumMinutes { get; private set; }

        public string CreateToken() => "token";

        public bool TryValidateToken(string token, int minSecondsToRespond, int minutesToRespond)
        {
            LastToken = token;
            LastMinimumSeconds = minSecondsToRespond;
            LastMaximumMinutes = minutesToRespond;
            return accepted;
        }
    }
}
