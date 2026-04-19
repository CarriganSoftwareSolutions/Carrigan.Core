using Carrigan.Core.Razor.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Carrigan.Core.Razor.ViewComponents;

public class ActionLinkViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ActionLinkViewModel actionLinkViewModel) =>
        View(actionLinkViewModel);
}
