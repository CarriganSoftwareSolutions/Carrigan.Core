using System;
using System.ComponentModel.DataAnnotations;

namespace Carrigan.Core.Mvc.Validators;

/// <summary>
/// Validates that a honeypot field remains empty.
/// If a value is submitted, validation fails with an error message
/// that appears to describe a normal required-field issue.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class HoneypotValidationAttribute : ValidationAttribute
{
    /// <summary>
    /// Creates a new <see cref="HoneypotValidationAttribute"/>.
    /// </summary>
    public HoneypotValidationAttribute()
    {
        ErrorMessage = "The {0} field is required.";
    }

    /// <summary>
    /// Returns true only when the submitted value is null or empty.
    /// Any populated value is treated as a failed honeypot validation.
    /// </summary>
    /// <param name="value">The submitted field value.</param>
    /// <returns>
    /// <see cref="ValidationResult.Success"/> when the field is empty;
    /// otherwise a failed <see cref="ValidationResult"/>.
    /// </returns>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        if (value is string stringValue && string.IsNullOrWhiteSpace(stringValue))
        {
            return ValidationResult.Success;
        }

        string errorMessage = FormatErrorMessage(validationContext.DisplayName);
        return new ValidationResult(errorMessage);
    }
}