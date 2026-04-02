using Carrigan.Core.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Carrigan.Core.Mvc.Annotations;

//ignore spelling: thisorthatisrequired otherproperty
public class ThisOrThatIsRequiredAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly string _comparisonProperty;

    public ThisOrThatIsRequiredAttribute(string comparisonProperty) => 
        _comparisonProperty = comparisonProperty;

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        // Get the property to compare with
        PropertyInfo comparisonPropertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty) ?? throw new ArgumentException($"Unknown property: {_comparisonProperty}");

        // Get the value of the comparison property
        object? comparisonValue = comparisonPropertyInfo.GetValue(validationContext.ObjectInstance);

        // If both values are null, validation fails
        if (value == null && comparisonValue == null)
            return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} or {comparisonPropertyInfo.GetDisplayName()} must have a value.");

        try
        {
            // If either value has content, validation succeeds
            if (value.HasValue() || comparisonValue.HasValue())
                return ValidationResult.Success!;
            else
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} or {comparisonPropertyInfo.GetDisplayName()} must have a value.");
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
        MergeAttribute(context.Attributes, "data-val-thisorthatisrequired", ErrorMessage ?? $"Either {context.ModelMetadata.GetDisplayName()} or {comparisonPropertyInfo?.GetDisplayName()} must have a value.");
        MergeAttribute(context.Attributes, "data-val-thisorthatisrequired-otherproperty", _comparisonProperty);
    }

    private static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
    {
        if (attributes.ContainsKey(key))
            return false;

        attributes.Add(key, value);
        return true;
    }
}
