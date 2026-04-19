using Carrigan.Core.Razor.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Carrigan.Core.Razor.ViewComponents;

public class LinkViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(LinkViewModel linkViewModel) =>
        View(linkViewModel);
}
