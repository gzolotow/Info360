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
            ViewBag.Nivel1Completado = HttpContext.Session.GetString("Nivel1Completado") == "true";
            ViewBag.Nivel2Completado = HttpContext.Session.GetString("Nivel2Completado") == "true";
            return View();
        }


        public IActionResult Inventario()
        {
            ViewBag.user = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));
            ViewBag.nivelUsuario = Objeto.StringToObject<NivelUsuario>(HttpContext.Session.GetString("nivelUsuario"));
            ViewBag.AspectoEquipado = BD.BuscarAspectoEquipado(BD.BuscarEquipado(ViewBag.nivelUsuario.IDNivelUsuario));
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
            ViewBag.nivelUsuario = Objeto.StringToObject<NivelUsuario>(HttpContext.Session.GetString("nivelUsuario"));
            ViewBag.AspectoEquipado = BD.BuscarAspectoEquipado(BD.BuscarEquipado(ViewBag.nivelUsuario.IDNivelUsuario));
            return View();
        }
        public IActionResult Nivel2()
        {
            ViewBag.user = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));
            ViewBag.nivelUsuario = Objeto.StringToObject<NivelUsuario>(HttpContext.Session.GetString("nivelUsuario"));
            ViewBag.AspectoEquipado = BD.BuscarAspectoEquipado(BD.BuscarEquipado(ViewBag.nivelUsuario.IDNivelUsuario));
            return View();
        }
        // *** NUEVA ACCIÓN PARA GUARDAR RESULTADO DEL NIVEL ***
        
[HttpPost]
public IActionResult GuardarResultadoNivel([FromBody] NivelCompletadoRequest request)
{
    if (request.nivelId == 1)
    {
        HttpContext.Session.SetString("Nivel1Completado", "true");
    }
    else if (request.nivelId == 2)
    {
        HttpContext.Session.SetString("Nivel2Completado", "true");
    }

    return Json(new { success = true });
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
            ViewBag.user = Objeto.StringToObject<Usuario>(HttpContext.Session.GetString("user"));

            return View("Perfil");
        }

        public IActionResult EditarSkin(int IDNivelUsuario, int IDAspecto)
        {
            NivelUsuario nivelUsuario = Objeto.StringToObject<NivelUsuario>(HttpContext.Session.GetString("nivelUsuario"));
            BD.EditarAspectoEquipado(IDNivelUsuario, IDAspecto);
            nivelUsuario.AspectoEquipado = IDAspecto;
            HttpContext.Session.SetString("nivelUsuario", Objeto.ObjectToString(nivelUsuario));
  
            return RedirectToAction("Inventario");
        }
    }
}