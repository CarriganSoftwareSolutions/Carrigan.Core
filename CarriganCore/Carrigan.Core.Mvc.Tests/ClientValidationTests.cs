using Carrigan.Core.Mvc.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Carrigan.Core.Mvc.Tests;
//ignore spelling: datetimeafter minvalue dateaftertoday greaterthan notboth otherproperty thisorthatisrequired mustbetrue
public class ClientValidationTests 
{
    [Theory]
    [InlineData(false, "false")]
    [InlineData(true, "true")]
    public void DateTimeAfter_AddValidation_WritesDateConstraints(bool inclusive, string expectedInclusive)
    {
        DateTimeAfterAttribute attribute = new("2026-05-10", inclusive);
        ClientModelValidationContext context = CreateContext(nameof(ClientModel.End));

        attribute.AddValidation(context);

        Assert.Equal("true", context.Attributes["data-val"]);
        Assert.Equal("2026-05-10T00:00", context.Attributes["data-val-datetimeafter-minvalue"]);
        Assert.Equal(expectedInclusive, context.Attributes["data-val-datetimeafter-inclusive"]);
    }

    [Fact]
    public void DateAfterToday_AddValidation_UsesDateOnlyFormat()
    {
        DateAfterTodayAttribute attribute = new(inclusive: true);
        ClientModelValidationContext context = CreateContext(nameof(ClientModel.Date));

        attribute.AddValidation(context);

        Assert.Equal(DateOnly.FromDateTime(DateTime.Today).ToString("yyyy-MM-dd"), context.Attributes["data-val-dateaftertoday-minvalue"]);
        Assert.Equal("true", context.Attributes["data-val-dateaftertoday-inclusive"]);
    }

    [Fact]
    public void GreaterThan_AddValidation_WritesComparisonMetadata()
    {
        GreaterThanAttribute attribute = new(nameof(ClientModel.Lower));
        ClientModelValidationContext context = CreateContext(nameof(ClientModel.Upper));

        attribute.AddValidation(context);

        Assert.Equal("Lower", context.Attributes["data-val-greaterthan-other"]);
        Assert.Equal("int", context.Attributes["data-val-greaterthan-type"]);
        Assert.Equal("true", context.Attributes["data-val"]);
    }

    [Fact]
    public void NotBoth_AddValidation_WritesComparisonProperty()
    {
        NotBothAttribute attribute = new(nameof(ClientModel.First));
        ClientModelValidationContext context = CreateContext(nameof(ClientModel.Second));

        attribute.AddValidation(context);

        Assert.Equal("First", context.Attributes["data-val-notboth-otherproperty"]);
        Assert.NotEmpty(context.Attributes["data-val-notboth"]);
    }

    [Fact]
    public void ThisOrThatIsRequired_AddValidation_WritesComparisonProperty()
    {
        ThisOrThatIsRequiredAttribute attribute = new(nameof(ClientModel.First));
        ClientModelValidationContext context = CreateContext(nameof(ClientModel.Second));

        attribute.AddValidation(context);

        Assert.Equal("First", context.Attributes["data-val-thisorthatisrequired-otherproperty"]);
        Assert.NotEmpty(context.Attributes["data-val-thisorthatisrequired"]);
    }

    [Fact]
    public void MustBeTrue_AddValidation_WritesClientMessage()
    {
        MustBeTrueAttribute attribute = new();
        ClientModelValidationContext context = CreateContext(nameof(ClientModel.Accepted));

        attribute.AddValidation(context);

        Assert.Contains("must be checked", context.Attributes["data-val-mustbetrue"]);
    }

    [Fact]
    public void MustBeTrue_AddValidation_PreservesExistingAttributes()
    {
        MustBeTrueAttribute attribute = new();
        ClientModelValidationContext context = CreateContext(nameof(ClientModel.Accepted));
        context.Attributes["data-val-mustbetrue"] = "original";

        attribute.AddValidation(context);

        Assert.Equal("original", context.Attributes["data-val-mustbetrue"]);
    }

    private static ClientModelValidationContext CreateContext(string propertyName)
    {
        IModelMetadataProvider provider = new EmptyModelMetadataProvider();
        ModelMetadata metadata = provider.GetMetadataForProperty(typeof(ClientModel), propertyName);
        Dictionary<string, string> attributes = [];
        return new ClientModelValidationContext(new ActionContext(), metadata, provider, attributes);
    }

    private sealed class ClientModel
    {
        public int Lower { get; init; }
        public int Upper { get; init; }
        public string? First { get; init; }
        public string? Second { get; init; }
        public bool Accepted { get; init; }
        public DateOnly Date { get; init; }
        public DateTime End { get; init; }
    }
}
