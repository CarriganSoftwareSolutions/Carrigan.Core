using Carrigan.Core.Enums.Html;
using Carrigan.Core.Enums.Html.Bootstrap;
using Carrigan.Core.Razor.ViewModels;

namespace Carrigan.Core.Razor.Tests;

public class ActionViewDesignTests
{
    [Fact]
    public void Constructor_DefaultsToEnabledAndUsesTextForAccessibility()
    {
        ActionViewDesign design = new("Continue", null, null, ButtonColorsEnum.Primary);

        Assert.Equal("Continue", design.Text);
        Assert.Equal("Continue", design.Tooltip);
        Assert.Equal("Continue", design.AriaDescription);
        Assert.True(design.Enabled);
        Assert.False(design.HideTextOnSmall);
        Assert.Null(design.Icon);
        Assert.Equal(string.Empty, design.IconText);
        Assert.Equal("btn-primary", design.ColorText);
    }

    [Fact]
    public void Constructor_ExplicitProperties_ArePreserved()
    {
        ActionViewDesign design = new("Save", "Save document", "Save changes", ButtonColorsEnum.Success, FontAwesomeEnum.Save, enabled: false, hideTextOnSmall: true);

        Assert.Equal("Save document", design.AriaDescription);
        Assert.Equal("Save changes", design.Tooltip);
        Assert.False(design.Enabled);
        Assert.True(design.HideTextOnSmall);
        Assert.Equal("fa-solid fa-floppy-disk", design.IconText);
        Assert.Equal("btn-success", design.ColorText);
    }

    [Fact]
    public void CopyConstructor_PreservesProperties()
    {
        ActionViewDesign original = new("Delete", "Delete item", "Delete", ButtonColorsEnum.Danger, FontAwesomeEnum.Delete, enabled: false, hideTextOnSmall: true);

        ActionViewDesign copy = new(original);

        Assert.NotSame(original, copy);
        Assert.Equal(original.Text, copy.Text);
        Assert.Equal(original.Tooltip, copy.Tooltip);
        Assert.Equal(original.AriaDescription, copy.AriaDescription);
        Assert.Equal(original.Color, copy.Color);
        Assert.Equal(original.Icon, copy.Icon);
        Assert.Equal(original.Enabled, copy.Enabled);
        Assert.Equal(original.HideTextOnSmall, copy.HideTextOnSmall);
    }

    [Fact]
    public void ChangeColor_ClonesWithoutModifyingOriginal()
    {
        ActionViewDesign original = new("Submit", null, null, ButtonColorsEnum.Primary, FontAwesomeEnum.Submit);

        ActionViewDesign changed = original.ChangeColor(ButtonColorsEnum.Warning);

        Assert.NotSame(original, changed);
        Assert.Equal(ButtonColorsEnum.Primary, original.Color);
        Assert.Equal(ButtonColorsEnum.Warning, changed.Color);
        Assert.Equal(original.Text, changed.Text);
        Assert.Equal(original.IconText, changed.IconText);
    }
}
