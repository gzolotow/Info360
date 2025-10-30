using Microsoft.AspNetCore.Mvc;
using EcoPlay.Models;

namespace EcoPlay.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Perfil()
        {
            return View();
        }
      public IActionResult Niveles()
{
    return View();
}
        public IActionResult Inventario()
        {
                  return View();
                } 
            

public IActionResult NivelesJuego(int id)
{
    ViewData["NivelSeleccionado"] = id;

    if (id == 0)
    {
        ViewData["TituloNivel"] = "Tutorial";
    }
    else if (id == 1)
    {
        ViewData["TituloNivel"] = "Nivel 1: Reciclaje Básico";
    }
    else if (id == 2)
    {
        ViewData["TituloNivel"] = "Nivel 2: Energía y Naturaleza";
    }
    else if (id == 3)
    {
        ViewData["TituloNivel"] = "Nivel 3: Agua y Contaminación";
    }
    else
    {
        ViewData["TituloNivel"] = "Nivel desconocido";
    }

    return View();
}
          

}
    }

