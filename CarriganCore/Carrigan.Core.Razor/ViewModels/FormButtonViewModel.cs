using Carrigan.Core.Enums.Html;
using Carrigan.Core.Enums.Html.Bootstrap;

namespace Carrigan.Core.Razor.ViewModels;

public class FormButtonViewModel
{
    public string? Id { get; private set; }
    public ButtonTypeEnum Type { get; private set; }
    public string TypeText =>
        Type.ToString();
    protected FormElements FormElements { get; private set; }
    protected ActionProperties ActionProperties { get; private set; }
    protected ActionViewDesign ActionViewDesign { get; private set; }

    public FormButtonViewModel(string? htmlId, ButtonTypeEnum buttonType, ActionViewDesign actionViewDesign, ActionProperties actionProperties, FormElements buttonFormElements)
    {
        Id = htmlId; 
        Type = buttonType;
        ActionViewDesign = actionViewDesign;
        ActionProperties = actionProperties;
        FormElements = buttonFormElements;
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

    public string? FormId =>
        FormElements?.FormId;


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

    public bool FormValidate =>
        FormElements.FormValidate;
    public FormMethodEnum FormMethod =>
        FormElements.FormMethod;
    public string FormMethodText =>
        FormElements.FormMethodText;
}
