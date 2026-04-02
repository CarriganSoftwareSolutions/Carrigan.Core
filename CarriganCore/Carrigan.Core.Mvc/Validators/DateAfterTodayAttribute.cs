using Carrigan.Core.Mvc.Extensions;
using Carrigan.Core.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Carrigan.Core.Mvc.Annotations;

//IGNORE SPELLING: dateaftertoday minvalue

public class DateAfterTodayAttribute : DateTimeAfterAttribute, IClientModelValidator
{
    public DateAfterTodayAttribute(bool inclusive = false) : base (DateTime.Today.ToString(), inclusive)
    {
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {

        if (value is DateTime || value is DateOnly)
        {
            return base.IsValid(value, validationContext);
        }
        else
        {
            throw new Exception("Invalid data type for DateAfterTodayAttribute.");
        }
    }
    public override void AddValidation(ClientModelValidationContext context)
    {
        Type modelType = context.ModelMetadata.ModelType;
        string minValue;
        string displayValue;

        if (modelType == typeof(DateOnly) || modelType == typeof(DateTime))
        {
            minValue = _minDate.ToDateOnly().ToHtmlValue();
            displayValue = _minDate.ToDateOnly().ToMonthDayYearLong();
        }
        else 
        {
            throw new Exception("Invalid data type for DateAfterTodayAttribute.");
        }

        MergeAttribute(context.Attributes, "data-val", "true");
        if (_inclusive)
            MergeAttribute(context.Attributes, "data-val-dateaftertoday", ErrorMessage ?? $"Value must be on or after {displayValue}.");
        else
            MergeAttribute(context.Attributes, "data-val-dateaftertoday", ErrorMessage ?? $"Value must be after {displayValue}.");

        MergeAttribute(context.Attributes, "data-val-dateaftertoday-minvalue", minValue);
        MergeAttribute(context.Attributes, "data-val-dateaftertoday-inclusive", _inclusive.ToString().ToLower());
    }
}
