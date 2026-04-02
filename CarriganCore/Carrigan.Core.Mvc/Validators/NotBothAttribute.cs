using Carrigan.Core.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Carrigan.Core.Mvc.Annotations;
//ignore spelling: notboth otherproperty

public class NotBothAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly string _comparisonProperty;

    public NotBothAttribute(string comparisonProperty) =>
        _comparisonProperty = comparisonProperty;

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        // Get the property to compare with
        PropertyInfo comparisonPropertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty) ?? throw new ArgumentException($"Unknown property: {_comparisonProperty}");

        // Get the value of the comparison property
        object? comparisonValue = comparisonPropertyInfo.GetValue(validationContext.ObjectInstance);

        try
        {
            // If either value has content, validation succeeds
            if (value.HasValue() && comparisonValue.HasValue())
                return new ValidationResult(ErrorMessage ?? $"Only {validationContext.DisplayName} or {comparisonPropertyInfo.GetDisplayName()} may have a value, both cannot have a value as per the RFC 5545 specification.");
            else 
                return ValidationResult.Success!;
        }
        catch (Exception)
        {
            return new ValidationResult("An error occurred during validation.");
        }
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        PropertyInfo? comparisonPropertyInfo = context.ModelMetadata.ContainerType?.GetProperty(_comparisonProperty);

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-notboth", ErrorMessage ?? $"Only {context.ModelMetadata.GetDisplayName()} or {comparisonPropertyInfo?.GetDisplayName()} may have a value, both cannot have a value as per the RFC 5545 specification.");
        MergeAttribute(context.Attributes, "data-val-notboth-otherproperty", _comparisonProperty);
    }

    private static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
    {
        if (attributes.ContainsKey(key))
            return false;

        attributes.Add(key, value);
        return true;
    }
}
