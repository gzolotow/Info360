using Microsoft.AspNetCore.Mvc;
using EcoPlay.Models;

namespace EcoPlay.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/IniciarSesion
        public IActionResult IniciarSesion()
        {
            return View();
        }

        // POST: /Account/IniciarSesion
        [HttpPost]
        public IActionResult IniciarSesion(string usuario, string contraseña)
        {
            var user = BD.Login(usuario, contraseña);

            if (user != null)
            {
                // Si el login es correcto, redirigimos al Home
                // (Podés cambiarlo por otra vista o página de bienvenida)
                ViewBag.Mensaje = "Inicio de sesión exitoso. ¡Bienvenido " + user.Username + "!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Si falla, mostramos un mensaje de error
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
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
            DateTime fechaNacimiento;
            if (!DateTime.TryParse(fecha, out fechaNacimiento))
            {
                ViewBag.Error = "La fecha no tiene un formato válido (dd/mm/aa).";
                return View();
            }

            bool registro = BD.Registrarse(username, email, fechaNacimiento, password, "default.png");

            if (registro)
            {
                // Si se registra bien, lo mandamos al login
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
