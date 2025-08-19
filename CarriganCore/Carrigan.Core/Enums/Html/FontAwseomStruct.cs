namespace Carrigan.Core.Enums.Html;

/// <summary>
/// Boilerplate to allow implicit conversions to and from string and the enum
/// </summary>
public class FontAwesomeStruct
{
    public FontAwesomeEnum Value { get; }

    public FontAwesomeStruct(FontAwesomeEnum value)
    {
        Value = value;
    }

    // Implicit conversion from FontAwesomeEnum to FontAwesomeIcon
    public static implicit operator FontAwesomeStruct(FontAwesomeEnum value)
    {
        return new FontAwesomeStruct(value);
    }

    // Implicit conversion from FontAwesomeIcon to string using a stub conversion function
    public static implicit operator string(FontAwesomeStruct icon)
    {
        return icon.Value.ToHtml();
    }
}