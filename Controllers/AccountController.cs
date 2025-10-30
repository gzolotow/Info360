using Microsoft.AspNetCore.Mvc;

namespace EcoPlay.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult IniciarSesion()
        {
            return View();
        }

        public IActionResult Registrarse()
        {
            return View();
        }
    }
}
