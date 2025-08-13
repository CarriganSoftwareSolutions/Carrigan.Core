namespace Carrigan.Core.Enums;

//IGNORE SPELLING: xmark
public static class FontAwesomeEnumExtensions
{
    public static string GetIconText(this FontAwesomeEnum icon)
    {
        return icon switch
        {
            FontAwesomeEnum.Add => "fa-solid fa-file-circle-plus",
            FontAwesomeEnum.Back => "fa-solid fa-delete-left",
            FontAwesomeEnum.CalendarAdd => "fa-solid fa-calendar-plus",
            FontAwesomeEnum.CalendarSubtract => "fa-solid fa-calendar-minus",
            FontAwesomeEnum.CalendarXMark => "fa-solid fa-calendar-xmark",
            FontAwesomeEnum.Calculator => "fa-solid fa-calculator",
            FontAwesomeEnum.Cancel => "fa-solid fa-xmark",
            FontAwesomeEnum.Close => "fa-solid fa-square-xmark",
            FontAwesomeEnum.Confirm => "fa-solid fa-check",
            FontAwesomeEnum.Delete => "fa-solid fa-trash-can",
            FontAwesomeEnum.Bible => "fa-solid fa-book-bible",
            FontAwesomeEnum.Edit => "fa-solid fa-file-pen",
            FontAwesomeEnum.Email => "fa-solid fa-envelope",
            FontAwesomeEnum.Events => "fa-solid fa-calendar-days",
            FontAwesomeEnum.Information => "fa-solid fa-circle-info",
            FontAwesomeEnum.LeftRight => "fa-solid fa-right-left",
            FontAwesomeEnum.MapLocation => "fa-solid fa-map-location-dot",
            FontAwesomeEnum.News => "fa-regular fa-newspaper",
            FontAwesomeEnum.Phone => "fa-solid fa-phone",
            FontAwesomeEnum.Pray => "fa-solid fa-hands-praying",
            FontAwesomeEnum.Print => "fa-solid fa-print",
            FontAwesomeEnum.Recycle => "fa-solid fa-recycle",
            FontAwesomeEnum.Repeat => "fa-solid fa-repeat",
            FontAwesomeEnum.Reset => "fa-solid fa-rotate",
            FontAwesomeEnum.RotateLeft => "fa-solid fa-rotate-left",
            FontAwesomeEnum.RotateRight => "fa-solid fa-rotate-right",
            FontAwesomeEnum.Save => "fa-solid fa-floppy-disk",
            FontAwesomeEnum.SearchMap => "fa-solid fa-magnifying-glass-location",
            FontAwesomeEnum.Search => "fa-solid fa-magnifying-glass",
            FontAwesomeEnum.Sort => "fa-solid fa-arrow-down-1-9",
            FontAwesomeEnum.Submit => "fa-solid fa-paper-plane",
            FontAwesomeEnum.Subscribe => "fa-regular fa-bell",
            FontAwesomeEnum.Unsubscribe => "fa-regular fa-bell-slash",
            FontAwesomeEnum.UpDown => "fas fa-exchange-alt rotate-90",
            FontAwesomeEnum.Upload => "fa-solid fa-upload",
            FontAwesomeEnum.View => "fa-solid fa-magnifying-glass",
            _ => throw new ArgumentOutOfRangeException(nameof(icon), icon, "Unknown FontAwesomeEnum value"),
        };
    }
}
