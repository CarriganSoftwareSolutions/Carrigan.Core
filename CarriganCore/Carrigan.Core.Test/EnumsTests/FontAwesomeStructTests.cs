using Carrigan.Core.Enums.Html;

namespace Carrigan.Core.Test.EnumsTests;

//IGNORE SPELLING: xmark

public class FontAwesomeStructTests
{
    [Theory]
    [InlineData(FontAwesomeEnum.Add, "fa-solid fa-file-circle-plus")]
    [InlineData(FontAwesomeEnum.Back, "fa-solid fa-delete-left")]
    [InlineData(FontAwesomeEnum.CalendarAdd, "fa-solid fa-calendar-plus")]
    [InlineData(FontAwesomeEnum.CalendarSubtract, "fa-solid fa-calendar-minus")]
    [InlineData(FontAwesomeEnum.CalendarXMark, "fa-solid fa-calendar-xmark")]
    [InlineData(FontAwesomeEnum.Calculator, "fa-solid fa-calculator")]
    [InlineData(FontAwesomeEnum.Cancel, "fa-solid fa-xmark")]
    [InlineData(FontAwesomeEnum.Close, "fa-solid fa-square-xmark")]
    [InlineData(FontAwesomeEnum.Confirm, "fa-solid fa-check")]
    [InlineData(FontAwesomeEnum.Delete, "fa-solid fa-trash-can")]
    [InlineData(FontAwesomeEnum.Bible, "fa-solid fa-book-bible")]
    [InlineData(FontAwesomeEnum.Edit, "fa-solid fa-file-pen")]
    [InlineData(FontAwesomeEnum.Email, "fa-solid fa-envelope")]
    [InlineData(FontAwesomeEnum.Events, "fa-solid fa-calendar-days")]
    [InlineData(FontAwesomeEnum.Information, "fa-solid fa-circle-info")]
    [InlineData(FontAwesomeEnum.LeftRight, "fa-solid fa-right-left")]
    [InlineData(FontAwesomeEnum.MapLocation, "fa-solid fa-map-location-dot")]
    [InlineData(FontAwesomeEnum.News, "fa-regular fa-newspaper")]
    [InlineData(FontAwesomeEnum.Phone, "fa-solid fa-phone")]
    [InlineData(FontAwesomeEnum.Pray, "fa-solid fa-hands-praying")]
    [InlineData(FontAwesomeEnum.Print, "fa-solid fa-print")]
    [InlineData(FontAwesomeEnum.Recycle, "fa-solid fa-recycle")]
    [InlineData(FontAwesomeEnum.Repeat, "fa-solid fa-repeat")]
    [InlineData(FontAwesomeEnum.Reset, "fa-solid fa-rotate")]
    [InlineData(FontAwesomeEnum.RotateLeft, "fa-solid fa-rotate-left")]
    [InlineData(FontAwesomeEnum.RotateRight, "fa-solid fa-rotate-right")]
    [InlineData(FontAwesomeEnum.Save, "fa-solid fa-floppy-disk")]
    [InlineData(FontAwesomeEnum.SearchMap, "fa-solid fa-magnifying-glass-location")]
    [InlineData(FontAwesomeEnum.Search, "fa-solid fa-magnifying-glass")]
    [InlineData(FontAwesomeEnum.Sort, "fa-solid fa-arrow-down-1-9")]
    [InlineData(FontAwesomeEnum.Submit, "fa-solid fa-paper-plane")]
    [InlineData(FontAwesomeEnum.Subscribe, "fa-regular fa-bell")]
    [InlineData(FontAwesomeEnum.Unsubscribe, "fa-regular fa-bell-slash")]
    [InlineData(FontAwesomeEnum.UpDown, "fas fa-exchange-alt rotate-90")]
    [InlineData(FontAwesomeEnum.Upload, "fa-solid fa-upload")]
    [InlineData(FontAwesomeEnum.View, "fa-solid fa-magnifying-glass")]
    public void ImplicitConversion_ToString_ReturnsExpectedIcon(FontAwesomeEnum enumValue, string expectedIcon)
    {
        // Arrange: Use the implicit conversion from FontAwesomeEnum to FontAwesomeStruct.
        FontAwesomeStruct iconStruct = enumValue;

        // Act: Use the implicit conversion from FontAwesomeStruct to string.
        string iconText = iconStruct;

        // Assert: Verify that the returned string matches the expected icon text.
        Assert.Equal(expectedIcon, iconText);
    }
}
