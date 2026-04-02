using Carrigan.Core.Mvc.Extensions;
using Carrigan.Core.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Carrigan.Core.Mvc.Annotations;

//IGNORE SPELLING: dateaftertoday minvalue datetimeafter

public class DateTimeAfterAttribute : ValidationAttribute, IClientModelValidator
{
    protected readonly DateTime _minDate;
    protected readonly bool _inclusive;

    public DateTimeAfterAttribute(string minDateString, bool inclusive = false)
    {
        if (!DateTime.TryParse(minDateString, out _minDate))
        {
            throw new ArgumentException("Invalid date/time format for DateAfterAttribute.");
        }
        _inclusive = inclusive;
    }
     
    private ValidationResult GenerateErrorMessage(ValidationContext validationContext)
    {
        if (_inclusive)
        {
            return new ValidationResult(ErrorMessage ?? $"Value must be on or after '{validationContext.DisplayName}', {_minDate.ToShortDateString()}.");
        }
        else
        {
            return new ValidationResult(ErrorMessage ?? $"Value must be after '{validationContext.DisplayName}', {_minDate.ToShortDateString()}.");
        }
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        bool Compare(IComparable a, IComparable b) 
        {
            return (_inclusive ? a.CompareTo(b) >= 0 : a.CompareTo(b) > 0);
        }

        bool testResult;

        if (value is DateTime dateTime)
        {
            testResult = Compare(dateTime.Date, _minDate);
        }
        else if (value is DateOnly date)
        {
            testResult = Compare(date, _minDate.ToDateOnly());
        }
        else if (value is TimeOnly time)
        {
            testResult = Compare(time, _minDate.ToTimeOnly());
        }
        else
        {
            throw new Exception("Invalid data type for DateAfterAttribute.");
        }

        if(testResult)
        {
            return ValidationResult.Success!;
        }
        else if(_inclusive)
        {
            return new ValidationResult(ErrorMessage ?? $"Value must be on or after {_minDate.ToShortDateString()}.");
        }
        else
        {
            return new ValidationResult(ErrorMessage ?? $"Value must be after {_minDate.ToShortDateString()}.");
        }
    }

    public virtual void AddValidation(ClientModelValidationContext context)
    {
        Type modelType = context.ModelMetadata.ModelType;
        string minValue;
        string displayValue;

        if (modelType == typeof(DateTime))
        {
            minValue = _minDate.ToHtmlValue();
            displayValue = _minDate.ToMonthDayYearAmPm();
        }
        else if (modelType == typeof(DateOnly))
        {
            minValue = _minDate.ToDateOnly().ToHtmlValue();
            displayValue = _minDate.ToDateOnly().ToMonthDayYearLong();
        }
        else if (modelType == typeof(TimeOnly))
        {
            minValue = _minDate.ToTimeOnly().ToHtmlValue();
            displayValue = _minDate.ToTimeOnly().ToAmPmString();
        }
        else
        {
            throw new Exception("Invalid data type for DateAfterAttribute.");
        }

        MergeAttribute(context.Attributes, "data-val", "true");
        if(_inclusive)
            MergeAttribute(context.Attributes, "data-val-datetimeafter", ErrorMessage ?? $"Value must be on or after {displayValue}.");
        else
            MergeAttribute(context.Attributes, "data-val-datetimeafter", ErrorMessage ?? $"Value must be after {displayValue}.");

        MergeAttribute(context.Attributes, "data-val-datetimeafter-minvalue", minValue);
        MergeAttribute(context.Attributes, "data-val-datetimeafter-inclusive", _inclusive.ToString().ToLower());
    }

    protected bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
    {
        if (attributes.ContainsKey(key))
            return false;

        attributes.Add(key, value);
        return true;
    }
}
