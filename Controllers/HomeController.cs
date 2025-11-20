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
            ViewBag.user = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));

            return View();
        }

        public IActionResult Home()
        {
            ViewBag.user = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));
            return View();
        }

        public IActionResult Perfil()
        {
            ViewBag.user = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));
            return View();
        }

        public IActionResult Niveles()
        {
            ViewBag.user = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));
            return View();
        }

        public IActionResult Inventario()
        {
            ViewBag.user = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));
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
                    titulo = "???";
                    break;
            }

            ViewData["TituloNivel"] = titulo;
            return View();
        }

        [HttpPost]
        public IActionResult GuardarDatosUsuario(string nombre, string mail)
        {
            ViewBag.user = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));
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
            ViewBag.user = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));
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

        public IActionResult Editar()
        {
            ViewBag.user = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));
            return View();
        }

        [HttpPost]
        public IActionResult EditarUsuario(int IDUsuario, string username, string email, string password, string fecha, string image, int IDNivelUsuario)
        {
            Usuario usu = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));
            
            if (!DateTime.TryParse(fecha, out DateTime fechaNacimiento))
            {
                ViewBag.Error = "La fecha no tiene un formato válido (dd/mm/aa).";
                return View("Editar");
            }
            
            if(image == null){
                usu = BD.ModificarUsuario(IDUsuario, username, email, fechaNacimiento, password, "default.png", IDNivelUsuario);
            }else{
                usu = BD.ModificarUsuario(IDUsuario, username, email, fechaNacimiento, password, image, IDNivelUsuario);
            }

            HttpContext.Session.SetString("user", Objeto.ObjectToString(usu));

            return View("Perfil");
        }
    }
}