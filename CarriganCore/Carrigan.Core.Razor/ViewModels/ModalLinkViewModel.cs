using Carrigan.Core.Enums.Html;
using Carrigan.Core.Enums.Html.Bootstrap;

namespace Carrigan.Core.Razor.ViewModels;

public class ModalLinkViewModel
{
    public string Target { get; private init; }
    public string? Id { get; private init; }
    protected ActionViewDesign ActionViewDesign { get; private init; }

    public ModalLinkViewModel(string target, string? htmlId, ActionViewDesign actionViewDesigns)
    {
        Target = target;
        Id = htmlId; 
        ActionViewDesign = actionViewDesigns;
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
