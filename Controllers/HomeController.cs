using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EcoPlay.Models;
using System.Linq;

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
            return View("Nivel1");
        }

        public IActionResult Home()
        {
            var nombreUsuario = HttpContext.Session.GetString("NombreUsuario");
            ViewData["NombreUsuario"] = nombreUsuario ?? "Invitado";
            return View();
        }

        public IActionResult Perfil()
        {
            var mail = HttpContext.Session.GetString("MailUsuario");
            var nombre = HttpContext.Session.GetString("NombreUsuario");

            ViewData["MailUsuario"] = mail ?? "No registrado";
            ViewData["NombreUsuario"] = nombre ?? "Invitado";

            return View();
        }

        public IActionResult Niveles()
        {
            return View();
        }

        public IActionResult Inventario()
        {
            int? items = HttpContext.Session.GetInt32("ItemsRecolectados");
            ViewData["ItemsRecolectados"] = items ?? 0;
            return View();
        }

        public IActionResult NivelesJuego(int id)
        {
            HttpContext.Session.SetInt32("NivelActual", id);
            ViewData["NivelSeleccionado"] = id;

            string titulo;
            switch (id)
            {
                case 0:
                    titulo = "Tutorial";
                    break;
                case 1:
                    titulo = "Nivel 1: Reciclaje Básico";
                    break;
                case 2:
                    titulo = "Nivel 2: Energía y Naturaleza";
                    break;
                case 3:
                    titulo = "Nivel 3: Agua y Contaminación";
                    break;
                default:
                    titulo = "Nivel desconocido";
                    break;
            }

            ViewData["TituloNivel"] = titulo;
            return View();
        }

        [HttpPost]
        public IActionResult GuardarDatosUsuario(string nombre, string mail)
        {
            HttpContext.Session.SetString("NombreUsuario", nombre);
            HttpContext.Session.SetString("MailUsuario", mail);
            return RedirectToAction("Home");
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        // *** NUEVA ACCIÓN AGREGADA PARA EL NIVEL 1 ***

         public IActionResult Nivel1()
        {
            return View();
        }
        // *** NUEVA ACCIÓN PARA GUARDAR RESULTADO DEL NIVEL ***
        [HttpPost]
        public IActionResult GuardarResultadoNivel(int nivelId, int estrellas, int errores, string tiempo)
        {
            var mailUsuario = HttpContext.Session.GetString("MailUsuario");
            if (string.IsNullOrEmpty(mailUsuario))
            {
                return Json(new { success = false, message = "Usuario no logueado." });
            }
            // Parsear tiempo (ej: "1:45" a TimeSpan)
            var tiempoSpan = TimeSpan.Parse("00:" + tiempo);
            var resultado = new ResumenNivelView
            {
                UsuarioMail = mailUsuario,
                NivelId = nivelId,
                Estrellas = estrellas,
                Errores = errores,
                Tiempo = tiempoSpan,
                FechaCompletado = DateTime.Now
            };

            // Opcional: actualizar sesión (ej: incrementar items recolectados)
            var itemsActuales = HttpContext.Session.GetInt32("ItemsRecolectados") ?? 0;
            HttpContext.Session.SetInt32("ItemsRecolectados", itemsActuales + estrellas);  // Ej: +1 ítem por estrella
            return Json(new { success = true, message = "Resultado guardado." });
        }
    }
}