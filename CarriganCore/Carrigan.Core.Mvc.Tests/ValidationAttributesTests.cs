using System.ComponentModel.DataAnnotations;
using Carrigan.Core.Mvc.Validators;

namespace Carrigan.Core.Mvc.Tests;

public class ValidationAttributesTests
{
    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData("   ", true)]
    [InlineData("spam", false)]
    [InlineData(123, false)]
    public void HoneypotValidation_ValidatesOnlyEmptyValues(object? value, bool expected)
    {
        HoneypotValidationAttribute attribute = new();

        Assert.Equal(expected, IsValid(attribute, value, new object(), "Honeypot"));
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void MustBeTrue_ValidatesOnlyTrue(bool? value, bool expected)
    {
        MustBeTrueAttribute attribute = new();

        Assert.Equal(expected, attribute.IsValid(value));
    }

    [Fact]
    public void MustBeTrue_FormatsDefaultAndCustomMessages()
    {
        MustBeTrueAttribute attribute = new();
        MustBeTrueAttribute custom = new() { ErrorMessage = "Please agree" };

        Assert.Equal("The Consent field must be checked in order to continue.", attribute.FormatErrorMessage("Consent"));
        Assert.Equal("Please agree", custom.FormatErrorMessage("Consent"));
    }

    [Theory]
    [InlineData(null, null, true)]
    [InlineData("first", null, true)]
    [InlineData(null, "second", true)]
    [InlineData("first", "second", false)]
    public void NotBoth_RejectsTwoPopulatedProperties(string? first, string? second, bool expected)
    {
        TextModel model = new() { First = first, Second = second };
        NotBothAttribute attribute = new(nameof(TextModel.Second));

        Assert.Equal(expected, IsValid(attribute, first, model, nameof(TextModel.First)));
    }

    [Theory]
    [InlineData(null, null, false)]
    [InlineData("first", null, true)]
    [InlineData(null, "second", true)]
    [InlineData("first", "second", true)]
    [InlineData(" ", "", false)]
    public void ThisOrThatIsRequired_RequiresOnePopulatedProperty(string? first, string? second, bool expected)
    {
        TextModel model = new() { First = first, Second = second };
        ThisOrThatIsRequiredAttribute attribute = new(nameof(TextModel.Second));

        Assert.Equal(expected, IsValid(attribute, first, model, nameof(TextModel.First)));
    }

    [Theory]
    [InlineData(10, 11, true)]
    [InlineData(10, 10, false)]
    [InlineData(10, 9, false)]
    [InlineData(null, 10, true)]
    [InlineData(10, null, true)]
    public void GreaterThan_UsesStrictComparisonUnlessInclusive(int? lower, int? upper, bool expected)
    {
        NumericModel model = new() { Lower = lower, Upper = upper };
        GreaterThanAttribute attribute = new(nameof(NumericModel.Lower));

        Assert.Equal(expected, IsValid(attribute, upper, model, nameof(NumericModel.Upper)));
    }

    [Fact]
    public void GreaterThan_Inclusive_AllowsEqualValues()
    {
        NumericModel model = new() { Lower = 10, Upper = 10 };
        GreaterThanAttribute attribute = new(nameof(NumericModel.Lower), inclusive: true);

        Assert.True(IsValid(attribute, model.Upper, model, nameof(NumericModel.Upper)));
    }

    [Fact]
    public void ComparisonAttributes_UnknownProperty_ThrowsArgumentException()
    {
        TextModel model = new();
        ValidationContext context = new(model) { MemberName = nameof(TextModel.First) };

        Assert.Throws<ArgumentException>(() => new NotBothAttribute("Missing").GetValidationResult(model.First, context));
        Assert.Throws<ArgumentException>(() => new ThisOrThatIsRequiredAttribute("Missing").GetValidationResult(model.First, context));
        Assert.Throws<ArgumentException>(() => new GreaterThanAttribute("Missing").GetValidationResult(model.First, context));
    }

    [Theory]
    [InlineData(false, false, true)]
    [InlineData(false, true, false)]
    [InlineData(true, true, true)]
    public void DateTimeAfter_UsesConfiguredInclusivity(bool inclusive, bool equalDate, bool expected)
    {
        DateTime minimum = new(2026, 5, 10, 0, 0, 0);
        DateTime actual = equalDate ? minimum : minimum.AddDays(1);
        DateTimeAfterAttribute attribute = new("2026-05-10", inclusive);

        Assert.Equal(expected, IsValid(attribute, actual, new object(), "Date"));
    }

    [Fact]
    public void DateTimeAfter_ComparesDateOnlyAndTimeOnly()
    {
        DateTimeAfterAttribute attribute = new("2026-05-10 12:30", inclusive: true);

        Assert.True(IsValid(attribute, new DateOnly(2026, 5, 10), new object(), "Date"));
        Assert.False(IsValid(attribute, new TimeOnly(12, 29), new object(), "Time"));
        Assert.True(IsValid(attribute, new TimeOnly(12, 30), new object(), "Time"));
    }

    [Fact]
    public void DateTimeAfter_InvalidDateString_ThrowsArgumentException() =>
        Assert.Throws<ArgumentException>(() => new DateTimeAfterAttribute("not-a-date"));

    [Fact]
    public void DateAfterToday_ValidatesPastAndFutureDates()
    {
        DateAfterTodayAttribute attribute = new();
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);

        Assert.False(IsValid(attribute, today.AddDays(-1), new object(), "Date"));
        Assert.True(IsValid(attribute, today.AddDays(1), new object(), "Date"));
    }

    private static bool IsValid(ValidationAttribute attribute, object? value, object model, string memberName)
    {
        ValidationContext context = new(model) { MemberName = memberName };
        return attribute.GetValidationResult(value, context) == ValidationResult.Success;
    }

    private sealed class TextModel
    {
        public string? First { get; init; }
        public string? Second { get; init; }
    }

    private sealed class NumericModel
    {
        public int? Lower { get; init; }
        public int? Upper { get; init; }
    }
}
