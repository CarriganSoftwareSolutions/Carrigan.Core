using Carrigan.Core.Razor.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Carrigan.Core.Razor.ViewComponents;

public class ModalLinkViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ModalLinkViewModel modalLinkView) =>
        View(modalLinkView);
}
