using Carrigan.Core.Enums.Html;
using Carrigan.Core.Enums.Html.Bootstrap;
using Carrigan.Core.Razor.ViewComponents;
using Carrigan.Core.Razor.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Carrigan.Core.Razor.Tests;

public class ViewComponentTests
{
    [Fact]
    public void ActionLinkViewComponent_Invoke_PassesModelToDefaultView()
    {
        ActionLinkViewModel model = new("action", NewDesign(), new ActionProperties("Index"));
        ActionLinkViewComponent component = new();

        ViewViewComponentResult result = Assert.IsType<ViewViewComponentResult>(component.Invoke(model));

        Assert.Null(result.ViewName);
        Assert.NotNull(result.ViewData);
        Assert.Same(model, result.ViewData.Model);
    }

    [Fact]
    public void FormButtonViewComponent_Invoke_PassesModelToDefaultView()
    {
        FormButtonViewModel model = new("save", ButtonTypeEnum.Submit, NewDesign(), new ActionProperties("Save"), new FormElements());
        FormButtonViewComponent component = new();

        ViewViewComponentResult result = Assert.IsType<ViewViewComponentResult>(component.Invoke(model));

        Assert.Null(result.ViewName);
        Assert.NotNull(result.ViewData);
        Assert.Same(model, result.ViewData.Model);
    }

    [Fact]
    public void LinkViewComponent_Invoke_PassesModelToDefaultView()
    {
        LinkViewModel model = new("/", "link", NewDesign());
        LinkViewComponent component = new();

        ViewViewComponentResult result = Assert.IsType<ViewViewComponentResult>(component.Invoke(model));

        Assert.Null(result.ViewName);
        Assert.NotNull(result.ViewData);
        Assert.Same(model, result.ViewData.Model);
    }

    [Fact]
    public void ModalButtonViewComponent_Invoke_PassesModelToDefaultView()
    {
        ModalButtonViewModel model = new("#dialog", "modal-button", NewDesign());
        ModalButtonViewComponent component = new();

        ViewViewComponentResult result = Assert.IsType<ViewViewComponentResult>(component.Invoke(model));

        Assert.Null(result.ViewName);
        Assert.NotNull(result.ViewData);
        Assert.Same(model, result.ViewData.Model);
    }

    [Fact]
    public void ModalLinkViewComponent_Invoke_PassesModelToDefaultView()
    {
        ModalLinkViewModel model = new("#dialog", "modal-link", NewDesign());
        ModalLinkViewComponent component = new();

        ViewViewComponentResult result = Assert.IsType<ViewViewComponentResult>(component.Invoke(model));

        Assert.Null(result.ViewName);
        Assert.NotNull(result.ViewData);
        Assert.Same(model, result.ViewData.Model);
    }

    private static ActionViewDesign NewDesign() =>
        new("Continue", null, null, ButtonColorsEnum.Primary);
}
