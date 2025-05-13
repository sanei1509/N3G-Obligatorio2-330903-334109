using CasoUsoCompartida.DTOs.Envios;
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.CasosUso.Envios;
using LogicaNegocio.Excepciones.EnvioExceptions;
using LogicaNegocio.Excepciones.Envios;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class EnvioController : Controller
    {
        private IGetAll<EnvioListadoDto> _getAll;
        private IAdd<CrearEnvioDto> _add;
        private FinalizarEnvio _finalizar;


        public EnvioController(IGetAll<EnvioListadoDto> getAll, IAdd<CrearEnvioDto> add, FinalizarEnvio finalizar)
        {
            _getAll = getAll;
            _add = add;
            _finalizar = finalizar;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListadoEnvios()
        {
            IEnumerable<EnvioListadoDto> listaEnvios = _getAll.Execute();
            return View(listaEnvios);
        }


        // Acción para dar de alta un envio
        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.CorreoEmpleado = HttpContext.Session.GetString("CorreoEmpleado");

            return View();
        }

        [HttpPost]
        public IActionResult Crear(CrearEnvioDto envioDto)
        {
            try
            {

                _add.Execute(envioDto);
                return RedirectToAction("ListadoEnvios");
            }
            catch (TipoEnvioException)
            {
                ViewBag.Message = "El Nombre es incorrecto";
            }
            catch (CorreoException)
            {
                ViewBag.Message = "El Apellido es incorrecto";
            }
            catch (PesoException)
            {
                ViewBag.Message = "El Correo es incorrecto";
            }
            catch (LugarRetiroException)
            {
                ViewBag.Message = "La Clave es incorrecta";
            }
            catch (DireccionException)
            {
                ViewBag.Message = "La Dirección es incorrecta";
            }

            return View();

        }


        [HttpGet]
        public IActionResult Finalizar(int id) 
        {
            try
            {

                _finalizar.Execute(id);

                return RedirectToAction("ListadoEnvios");
            }
            catch (TipoEnvioException)
            {
                ViewBag.Message = "Error Al finalizar envio";
            }
            return RedirectToAction("ListadoEnvios");
        }
    }
}
