using Microsoft.AspNetCore.Mvc;
using EcoPlay.Models;

namespace EcoPlay.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/IniciarSesion
        public IActionResult IniciarSesion()
        {
            ViewBag.Mensaje = TempData["Mensaje"];
            ViewBag.Error = TempData["Error"];
            return View();
        }

        // POST: /Account/IniciarSesion
        [HttpPost]
        public IActionResult IniciarSesion(string usuario, string contraseña)
        {
            var user = BD.Login(usuario, contraseña);

            if (user != null)
            {
                TempData["Mensaje"] = $"Inicio de sesión exitoso. ¡Bienvenido {user.Username}!";
                return RedirectToAction("Home", "Home");
            }
            else
            {
                // Guardamos un error temporal para mostrarlo en la vista
                TempData["Error"] = "❌ Usuario o contraseña incorrectos. Por favor, intenta nuevamente.";
                return RedirectToAction("IniciarSesion");
            }
        }

        // GET: /Account/Registrarse
        public IActionResult Registrarse()
        {
            return View();
        }

        // POST: /Account/Registrarse
        [HttpPost]
        public IActionResult Registrarse(string username, string email, string password, string fecha)
        {
            if (!DateTime.TryParse(fecha, out DateTime fechaNacimiento))
            {
                ViewBag.Error = "La fecha no tiene un formato válido (dd/mm/aa).";
                return View();
            }

            bool registro = BD.Registrarse(username, email, fechaNacimiento, password, "default.png");

            if (registro)
            {
                TempData["Mensaje"] = "Registro exitoso. Ahora puedes iniciar sesión.";
                return RedirectToAction("IniciarSesion");
            }
            else
            {
                ViewBag.Error = "El nombre de usuario ya existe. Elija otro.";
                return View();
            }
        }
    }
}
