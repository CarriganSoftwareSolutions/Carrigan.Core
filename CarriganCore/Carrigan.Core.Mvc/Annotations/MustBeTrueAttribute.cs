
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GuthrieMethodistChurch.Annotations;

//ignore spelling: mustbetrue

public class MustBeTrueAttribute : ValidationAttribute,IClientModelValidator
{
    public void AddValidation(ClientModelValidationContext context)
    {
        Type modelType = context.ModelMetadata.ModelType;

        if (modelType != typeof(bool) && modelType != typeof(bool?))
        {
            throw new Exception("Invalid data type for MustBeTrueAttribute.");
        }
        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-mustbetrue", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));

    }

    public override string FormatErrorMessage(string name)
    {
        return ErrorMessage ?? $"The {name} field must be checked in order to continue.";
    }

    public override bool IsValid(object? value) =>
         (bool?)value ?? false;
    protected bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
    {
        if (attributes.ContainsKey(key))
            return false;

        attributes.Add(key, value);
        return true;
    }
}
