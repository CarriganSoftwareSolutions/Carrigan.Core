using Carrigan.Core.Enums.Html;
using Carrigan.Core.Enums.Html.Bootstrap;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Carrigan.Core.Razor.ViewModels;

public class ActionViewDesign
{
    #region Properties
    private readonly string? _tooltip;
    private readonly string? _ariaDescription;
    public ButtonColorsEnum Color { get; private init; }
    public FontAwesomeEnum? Icon { get; private init; }
    public string Text { get; private init; }
    public string AriaDescription
    {
        get =>
            _ariaDescription ?? Text;
        private init =>
            _ariaDescription = value;
    }
    public string Tooltip
    {
        get =>
            _tooltip ?? Text;
        private init =>
            _tooltip = value;
    }
    public bool Enabled { get; private init; } = true;
    public bool HideTextOnSmall { get; private init; } = false;
    #endregion

    #region Constructors

    [SetsRequiredMembers]
    public ActionViewDesign(string text, string? ariaDescription, string? tooltip, ButtonColorsEnum color, FontAwesomeEnum? icon = null, bool? enabled = null, bool? hideTextOnSmall = null)
    {
        _tooltip = tooltip;
        _ariaDescription = ariaDescription;
        Color = color;
        Icon = icon;
        Text = text;
        if(enabled is not null)
            Enabled = enabled.Value;
        if(hideTextOnSmall is not null)
            HideTextOnSmall = hideTextOnSmall.Value;
    }

    [SetsRequiredMembers]
    public ActionViewDesign(ActionViewDesign cloneThis) : this(cloneThis.Text, cloneThis._ariaDescription, cloneThis._tooltip, cloneThis.Color, cloneThis.Icon, cloneThis.Enabled, cloneThis.HideTextOnSmall)
    {
    }
    #endregion

    #region Calculated Fields
    public string IconText =>
        Icon?.ToHtml() ?? string.Empty;
    public string ColorText =>
            Color.ToBootStrapClass();
    #endregion

    #region Chain Modifiers
    public  ActionViewDesign ChangeColor(ButtonColorsEnum color)
    {
        ActionViewDesign clone = new(this)
        {
            Color = color
        };
        return clone;
    }
    #endregion
}

