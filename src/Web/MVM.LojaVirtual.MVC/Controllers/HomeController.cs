using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVM.LojaVirtual.MVC.Models;

namespace MVM.LojaVirtual.MVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Route("error/{id:length(3,3)}")]
    public IActionResult Error(int id)
    {
        var modelError = new ErrorViewModel();

        if (id == 500)
        {
            modelError.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
            modelError.Title = "Ocorreu um erro!";
            modelError.StatusCode = id;
        }
        else if (id == 404)
        {
            modelError.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
            modelError.Title = "Ops, página não encontrada!";
            modelError.StatusCode = id;
        }
        else if (id == 403)
        {
            modelError.Message = "Você não tem permissão para fazer isso.";
            modelError.Title = "Ops, acesso negado!";
            modelError.StatusCode = id;
        }
        else
        {
            return StatusCode(404);
        }
        
        return View("Error", modelError);
    }
}