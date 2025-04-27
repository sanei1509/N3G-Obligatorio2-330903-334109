using CasoUsoCompartida.DTOs.Envios.CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.CasosUso.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class EnvioController : Controller
    {
        private IGetAll<EnvioListadoDto> _getAll;
        private IAdd<CrearEnvioDto> _add;


        public EnvioController(IGetAll<EnvioListadoDto> getAll, IAdd<CrearEnvioDto> add)
        {
            _getAll = getAll;
            _add = add;
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
            return View();
        }

    }
}
