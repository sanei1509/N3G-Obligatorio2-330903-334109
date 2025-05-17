using CasoUsoCompartida.DTOs.Envios;
using CasoUsoCompartida.DTOs.EtapaSeguimiento;
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.CasosUso.Envios;
using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Excepciones.AgenciaException;
using LogicaNegocio.Excepciones.EnvioExceptions;
using LogicaNegocio.Excepciones.Envios;
using LogicaNegocio.Excepciones.EtapaSeguimientoExceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class EnvioController : Controller
    {
        private IGetAll<EnvioListadoDto> _getAll;
        private IAdd<CrearEnvioDto> _add;
        private IFinalizar _finalizar;
        private IAdd<CrearComentarioDto> _addComentario;
        private IGetById<Envio> _getById;


        public EnvioController(IGetAll<EnvioListadoDto> getAll, IAdd<CrearEnvioDto> add, IFinalizar finalizar, IAdd<CrearComentarioDto> comentario, IGetById<Envio> getById)
        {
            _getAll = getAll;
            _add = add;
            _finalizar = finalizar;
            _addComentario = comentario;
            _getById = getById;
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
                ViewBag.Message = "El Correo ingresado no esta registrado como cliente";
                return View();
            }
            catch (PesoException)
            {
                ViewBag.Message = "El Peso debe ser un número positivo";
            }
            catch (LugarRetiroException)
            {
                ViewBag.Message = "Debes seleccionar un lugar de retiro";
            }
            catch (DireccionException)
            {
                ViewBag.Message = "La Dirección es un campo que no puede estar vacio";
            }
            catch (NombreAgenciaExeption)
            {
                ViewBag.Message = "No existe esa Agencia";
            }

            ViewBag.CorreoEmpleado = envioDto.CorreoEmpleado;
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

        //ETAPAS SEGUIMIENTO
        // Acción para dar de alta un envio
        //[HttpGet]
        //public IActionResult AgregarComentario()
        //{
        //    ViewBag.CorreoEmpleado = HttpContext.Session.GetString("CorreoEmpleado");

        //    return View();
        //}

        [HttpGet]
        public IActionResult AgregarComentario(int envioId)
        {
            ViewData["correoEmpleado"] = HttpContext.Session.GetString("CorreoEmpleado");
            ViewData["envioId"] = envioId;

            return View();
        }


        [HttpPost]
        public IActionResult AgregarComentario(CrearComentarioDto comentarioDto)
        {
            try
            {

                _addComentario.Execute(comentarioDto);
                return RedirectToAction("ListadoEnvios");
            }

            catch (ComentarioException)
            {
                ViewBag.Message = "El comentario no puede estar vacío";
                return View();
            }
        }

    }
}
