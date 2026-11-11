using Carrigan.Core.Enums.Html;
using Carrigan.Core.Enums.Html.Bootstrap;
using Carrigan.Core.Razor.ViewModels;

namespace Carrigan.Core.Razor.Tests;

public class ViewModelTests
{
    [Fact]
    public void ActionLinkViewModel_ExposesDesignAndRouteProperties()
    {
        ActionViewDesign design = new("Open", "Open details", "Open page", ButtonColorsEnum.Info);
        Dictionary<string, string?> parameters = new() { ["id"] = "42", ["optional"] = null };
        ActionProperties action = new("Details", "Orders", "Admin", parameters);

        ActionLinkViewModel model = new("action-link", design, action);

        Assert.Equal("action-link", model.Id);
        Assert.Equal("Open", model.Text);
        Assert.Equal("Open details", model.AriaDescription);
        Assert.Equal("Open page", model.Tooltip);
        Assert.Equal("btn-info", model.ColorText);
        Assert.Equal("Details", model.Action);
        Assert.Equal("Orders", model.Controller);
        Assert.Equal("Admin", model.Area);
        Assert.Equal("42", model.RouteValues["id"]);
        Assert.Equal(string.Empty, model.RouteValues["optional"]);
    }

    [Fact]
    public void ActionLinkViewModel_NoRouteValues_ReturnsEmptyDictionary()
    {
        ActionLinkViewModel model = new(null, NewDesign(), new ActionProperties("Index"));

        Assert.Empty(model.RouteValues);
        Assert.Null(model.Controller);
        Assert.Null(model.Area);
    }

    [Fact]
    public void FormElements_DefaultAndConfiguredValues_AreExposed()
    {
        FormElements defaults = new();
        FormElements custom = new("form", FormMethodEnum.Post, formValidate: false);

        Assert.Null(defaults.FormId);
        Assert.True(defaults.FormValidate);
        Assert.Equal(string.Empty, defaults.FormMethodText);
        Assert.Equal("form", custom.FormId);
        Assert.False(custom.FormValidate);
        Assert.Equal(FormMethodEnum.Post, custom.FormMethod);
        Assert.Equal("post", custom.FormMethodText);
    }

    [Fact]
    public void FormButtonViewModel_ExposesButtonAndFormProperties()
    {
        FormButtonViewModel model = new("save", ButtonTypeEnum.Submit, NewDesign(), new ActionProperties("Save", "Editor", routeValues: new() { ["id"] = "10" }), new FormElements("edit-form", FormMethodEnum.Post, false));

        Assert.Equal("save", model.Id);
        Assert.Equal(ButtonTypeEnum.Submit, model.Type);
        Assert.Equal("submit", model.TypeText);
        Assert.Equal("edit-form", model.FormId);
        Assert.Equal("Save", model.Action);
        Assert.Equal("Editor", model.Controller);
        Assert.Equal("10", model.RouteValues["id"]);
        Assert.False(model.FormValidate);
        Assert.Equal("post", model.FormMethodText);
        Assert.Equal("Continue", model.Text);
    }

    [Fact]
    public void LinkViewModel_StoresLinkAndDesignProperties()
    {
        LinkViewModel model = new("/home", "nav-home", NewDesign(), useLinkButton: true);

        Assert.Equal("/home", model.Href);
        Assert.Equal("nav-home", model.Id);
        Assert.True(model.UseLinkButton);
        Assert.True(model.Enabled);
        Assert.Equal("btn-primary", model.ColorText);
    }

    [Fact]
    public void ModalButtonViewModel_StoresTargetAndDesign()
    {
        ModalButtonViewModel model = new("#confirm", "launch", NewDesign());

        Assert.Equal("#confirm", model.Target);
        Assert.Equal("launch", model.Id);
        Assert.Equal("Continue", model.Text);
        Assert.True(model.Enabled);
    }

    [Fact]
    public void ModalLinkViewModel_StoresTargetAndDesign()
    {
        ModalLinkViewModel model = new("#details", "open", NewDesign());

        Assert.Equal("#details", model.Target);
        Assert.Equal("open", model.Id);
        Assert.Equal("Continue", model.Text);
        Assert.Equal("btn-primary", model.ColorText);
    }

    private static ActionViewDesign NewDesign() =>
        new("Continue", null, null, ButtonColorsEnum.Primary);
}
