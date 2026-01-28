using Carrigan.Core.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Carrigan.Core.Mvc.Annotations;

//IGNORE SPELLING: greaterthan

public class GreaterThanAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly string _comparisonProperty;
    private readonly bool _inclusive;


    public GreaterThanAttribute(string comparisonProperty, bool inclusive = false)
    {
        _comparisonProperty = comparisonProperty;
        _inclusive = inclusive;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        // Get the property to compare with
        PropertyInfo? comparisonPropertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty) ?? throw new ArgumentException($"Unknown property: {_comparisonProperty}");

        // Get the value of the comparison property
        object? comparisonValue = comparisonPropertyInfo.GetValue(validationContext.ObjectInstance);

        if (value == null || comparisonValue == null)
            return ValidationResult.Success!; // Handle [Required] separately

        try
        {
            // Convert values to IComparable

            if (value is not IComparable valueComparable || comparisonValue is not IComparable comparisonComparable)
                throw new ArgumentException($"Both properties must be comparable: {_comparisonProperty}");

            // Perform the comparison
            int comparisonResult = valueComparable.CompareTo(comparisonValue);

            bool isValid = _inclusive ? comparisonResult >= 0 : comparisonResult > 0;

            if (isValid)
                return ValidationResult.Success!;
            else
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be greater than {comparisonPropertyInfo.GetDisplayName()}.");
        }
        catch (Exception)
        {
            return new ValidationResult("An error occurred during validation.");
        }
    }
    public void AddValidation(ClientModelValidationContext context)
    {
        string displayName = context.ModelMetadata.GetDisplayName();
        Type modelType = context.ModelMetadata.ModelType;
        string modelTypeString;

        PropertyInfo? comparisonPropertyInfo = context?.ModelMetadata?.ContainerType?.GetProperty(_comparisonProperty);
        ArgumentNullException.ThrowIfNull(comparisonPropertyInfo);

        if (modelType.IsIntegerType())
            modelTypeString = "int";
        else if (modelType.IsFloatingPointType())
            modelTypeString = "float";
        else if (modelType.IsDateOnlyType())
            modelTypeString = "date";
        else if (modelType.IsTimeOnlyType())
            modelTypeString = "time";
        else if (modelType.IsStringType())
            modelTypeString = "string";
        else if (modelType.IsBoolType())
            modelTypeString = "bool";
        else
            throw new Exception("Invalid data type for GreaterThan Attribute.");


        string errorMessage = string.Format(ErrorMessage ?? "{0} must be greater than {1}.", displayName, comparisonPropertyInfo.GetDisplayName());


        ArgumentNullException.ThrowIfNull(context);
        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-greaterthan", errorMessage);
        MergeAttribute(context.Attributes, "data-val-greaterthan-other", _comparisonProperty);
        MergeAttribute(context.Attributes, "data-val-greaterthan-type", modelTypeString);
    }

    protected static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
    {
        if (attributes.ContainsKey(key))
            return false;

        attributes.Add(key, value);
        return true;
    }
}