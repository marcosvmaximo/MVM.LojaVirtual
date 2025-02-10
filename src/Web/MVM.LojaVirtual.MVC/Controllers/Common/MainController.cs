using Microsoft.AspNetCore.Mvc;
using MVM.LojaVirtual.MVC.Models.AuthExternal;

namespace MVM.LojaVirtual.MVC.Controllers.Common;

public class MainController : Controller
{
    protected bool ResponsePossuiErros(ErrorResponse? response)
    {
        if (response != null && response.Errors.Mensagens.Any())
        {
            foreach (string msg in response.Errors.Mensagens)
            {
                ModelState.AddModelError(string.Empty, msg);
            }
            return true;
        }

        return false;
    }
}