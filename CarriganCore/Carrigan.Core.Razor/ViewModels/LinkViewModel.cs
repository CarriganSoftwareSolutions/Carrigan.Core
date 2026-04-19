using Carrigan.Core.Enums.Html;
using Carrigan.Core.Enums.Html.Bootstrap;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Carrigan.Core.Razor.ViewModels;

public class LinkViewModel
{
    public string? Id { get; private set; }
    protected ActionViewDesign ActionViewDesign { get; private set; }
    public bool UseLinkButton { get; private set; }
    public string Href { get; set; }

    public LinkViewModel(string href, string? htmlId, ActionViewDesign actionViewDesign, bool useLinkButton = false)
    {
        Id = htmlId;
        ActionViewDesign = actionViewDesign;
        UseLinkButton = useLinkButton;
        Href = href;
    }

    public ButtonColorsEnum Color =>
        ActionViewDesign.Color;
    public FontAwesomeEnum? Icon =>
        ActionViewDesign.Icon;
    public string Text =>
        ActionViewDesign.Text;
    public string Tooltip =>
        ActionViewDesign.Tooltip;
    public bool Enabled =>
        ActionViewDesign.Enabled;
    public bool HideTextOnSmall =>
        ActionViewDesign.HideTextOnSmall;

    public string IconText =>
        ActionViewDesign.IconText;
    public string ColorText =>
        ActionViewDesign.ColorText;
    public string AriaDescription =>
        ActionViewDesign.AriaDescription;
}
