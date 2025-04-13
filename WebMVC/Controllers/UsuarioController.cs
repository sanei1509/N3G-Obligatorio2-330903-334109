using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class UsuarioController : Controller
    {
        //inyectar caso de uso de usuario
        private ILogin<LoginRespuestaDto> _login;

        public UsuarioController(ILogin<LoginRespuestaDto> login)
        {
            _login = login;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Usuario/Login
        [HttpPost]
        public IActionResult Login(LoginEntradaDto model)
        {
            var result = _login.Execute(model);

            if (!result.Autenticado)
            {
                ViewBag.Error = result.Mensaje;
                return View(model);
            }

            // Si todo OK, guardamos la sesión:
            HttpContext.Session.SetString("Logueado", "true");

            return RedirectToAction("Index", "Home");
        }

    }
}
