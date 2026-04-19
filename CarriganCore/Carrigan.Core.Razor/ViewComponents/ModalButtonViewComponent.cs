using Carrigan.Core.Razor.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Carrigan.Core.Razor.ViewComponents;

public class ModalButtonViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ModalButtonViewModel modalButtonView) =>
        View(modalButtonView);
}
