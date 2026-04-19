using Carrigan.Core.Enums.Html;
using Carrigan.Core.Enums.Html.Bootstrap;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Carrigan.Core.Razor.ViewModels;

public class ActionLinkViewModel
{
    public string? Id { get; private init; }
    protected ActionViewDesign ActionViewDesign { get; private init; }
    protected ActionProperties ActionProperties { get; private init; }

    public ActionLinkViewModel(string? htmlId, ActionViewDesign actionViewDesign, ActionProperties actionProperties)
    {
        Id = htmlId;
        ActionViewDesign = actionViewDesign;
        ActionProperties  = actionProperties;
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
    public string AriaDescription =>
        ActionViewDesign.AriaDescription;

    public string? Controller =>
        ActionProperties.Controller;
    public string? Action =>
        ActionProperties.Action;
    public string? Area =>
        ActionProperties.Area;
    public Dictionary<string, string> RouteValues =>
        ActionProperties
            .RouteValues?
            .ToDictionary
            (
                kvp => kvp.Key,
                kvp => kvp.Value?.ToString() ?? string.Empty
            ) ?? [];

    public string IconText =>
        ActionViewDesign.IconText;
    public string ColorText =>
        ActionViewDesign.ColorText;
}
