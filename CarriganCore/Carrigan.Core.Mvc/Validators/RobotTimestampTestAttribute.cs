using Carrigan.Core.Mvc.Interfaces;
using Carrigan.Core.Mvc.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Carrigan.Core.Mvc.Validators;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class RobotTimestampTestAttribute : ValidationAttribute
{
    private readonly int _minSecondsToRespond;
    private readonly int _minutesToRespond;

    public RobotTimestampTestAttribute(int minSecondsToRespond) =>
        _minSecondsToRespond = minSecondsToRespond;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.GetService(typeof(IRobotTimestampTestService)) is IRobotTimestampTestService formTimestampService)
        {
            string? token = value as string;

            bool isValid = formTimestampService.TryValidateToken(token ?? string.Empty, _minSecondsToRespond);

            if (isValid)
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage ?? "Are you a Robot?");
        }

        throw new InvalidOperationException($"Unable to resolve {nameof(IRobotTimestampTestService)} from the service provider.");
    }
}