using Microsoft.AspNetCore.Mvc;

namespace MVM.LojaVirtual.MVC.Configurations.Extensions;

public class SummaryViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}