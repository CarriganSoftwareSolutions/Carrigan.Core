using Carrigan.Core.Razor.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Carrigan.Core.Razor.ViewComponents;

public class FormButtonViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(FormButtonViewModel formButtonViewModel) =>
        View(formButtonViewModel);
}
