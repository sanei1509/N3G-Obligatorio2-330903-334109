using CasoUsoCompartida.DTOs.Envios;
using CasoUsoCompartida.DTOs.EtapaSeguimiento;
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using CasoUsoCompartida.DTOs;
using LogicaAplicacion.CasosUso.Envios;
using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Excepciones.AgenciaException;
using LogicaNegocio.Excepciones.EnvioExceptions;
using LogicaNegocio.Excepciones.Envios;
using LogicaNegocio.Excepciones.EtapaSeguimientoExceptions;
using LogicaNegocio.Vo.Envio;
using Microsoft.AspNetCore.Authorization;
using WebMVC.Filtros;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    [UsuarioLogueado]
    public class EnvioController : Controller
    {
        private IGetAll<EnvioListadoDto> _getAll;
        private IAdd<CrearEnvioDto> _add;
        private IFinalizar _finalizar;
        private IAdd<CrearComentarioDto> _addComentario;
        private IGetAll<AgenciaListadoDto> _agencias;

        public EnvioController(IGetAll<EnvioListadoDto> getAll, IAdd<CrearEnvioDto> add, IFinalizar finalizar, IAdd<CrearComentarioDto> comentario, IGetAll<AgenciaListadoDto> agencias)
        {
            _getAll = getAll;
            _add = add;
            _finalizar = finalizar;
            _addComentario = comentario;
            _agencias = agencias;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListadoEnvios(
            string nroTracking,
            DateTime? fechaDesde,
            DateTime? fechaHasta)

        {
            IEnumerable<EnvioListadoDto> listaEnvios = _getAll.Execute();


            // 2) Aplicas filtros si vienen
            if (!string.IsNullOrWhiteSpace(nroTracking))
                listaEnvios = listaEnvios.Where(e =>
                    e.NroTracking.Contains(nroTracking, StringComparison.OrdinalIgnoreCase));

            if (fechaDesde.HasValue)
                listaEnvios = listaEnvios.Where(e => e.FechaCreacion >= fechaDesde.Value);

            if (fechaHasta.HasValue)
                listaEnvios = listaEnvios.Where(e => e.FechaCreacion <= fechaHasta.Value);

            // 3) Pasas los filtros a la vista para volver a mostrarlos
            ViewBag.NroTracking = nroTracking;
            ViewBag.FechaDesde = fechaDesde?.ToString("yyyy-MM-dd");
            ViewBag.FechaHasta = fechaHasta?.ToString("yyyy-MM-dd");

            return View(listaEnvios);
        }

        // Acción para dar de alta un envio
        [HttpGet]
        public IActionResult Crear()
        {
            var listadoAgencias = _agencias.Execute();
            ViewBag.CorreoEmpleado = HttpContext.Session.GetString("CorreoEmpleado");
            ViewData["ListadoAgencias"] = listadoAgencias;
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

        [HttpGet]
        public IActionResult AgregarComentario(int envioId, string nroTracking)
        {
            var empleadoCorreo = HttpContext.Session.GetString("CorreoEmpleado");

            var dto = new CrearComentarioDto(
                     IdEnvio: envioId,
                     NroTracking: nroTracking,
                     CorreoEmpleado: empleadoCorreo,
                     Comentario: string.Empty
                );

            return View(dto);
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
