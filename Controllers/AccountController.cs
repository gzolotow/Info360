using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
                // ✅ Guardar datos del usuario en la sesión
                HttpContext.Session.SetString("NombreUsuario", user.Username);
                HttpContext.Session.SetString("MailUsuario", user.Mail);
                HttpContext.Session.SetInt32("IDUsuario", user.IDUsuario);

                // Redirige al Home principal
                return RedirectToAction("Home", "Home");
            }
            else
            {
                // ❌ Guardamos un error temporal para mostrarlo en la vista
                TempData["Error"] = "Usuario o contraseña incorrectos. Por favor, intenta nuevamente.";
                return RedirectToAction("IniciarSesion");
            }
        }

        // GET: /Account/Registrarse
        public IActionResult Registrarse()
        {
            ViewBag.Error = TempData["Error"];
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

        // ✅ Cerrar sesión
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            TempData["Mensaje"] = "Has cerrado sesión correctamente.";
            return RedirectToAction("IniciarSesion");
        }
    }
}
