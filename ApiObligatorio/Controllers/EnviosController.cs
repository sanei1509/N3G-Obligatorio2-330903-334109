using CasoUsoCompartida.DTOs.Envios;
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.CasosUso.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiObligatorio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnviosController : ControllerBase
    {
        IGetAll<EnvioListadoDto> _getAll;
        IGetByNroTracking<EnvioListadoDto> _getByNroTracking;
        public EnviosController(IGetAll<EnvioListadoDto> getAll, IGetByNroTracking<EnvioListadoDto> getByNroTracking)
        {
            _getAll = getAll;
            _getByNroTracking = getByNroTracking;
        }

        // GET api/envios
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            try
            {
                var envios = _getAll.Execute();
                if (envios.Count() == 0)
                {
                    return StatusCode(204);
                }
                return Ok(envios);
            }
            catch (Exception)
            {

                return StatusCode(500, "Intente nuevamente");
            }
        }



        [HttpGet("debug")]
        [Authorize]
        public IActionResult DebugToken()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(claims);
        }


        [HttpGet("{nroTracking}")]
        [AllowAnonymous]
        public IActionResult GetByNroTracking(string nroTracking)
        {
            // 1) Si el parámetro no viene o es nulo/vacío devolvemos BadRequest
            if (string.IsNullOrWhiteSpace(nroTracking))
                return BadRequest("Debe especificar un número de tracking en la URL.");

            try
            {

                var dto = _getByNroTracking.Execute(nroTracking);
                // 2) Si no existe, devolvemos 404
                if (dto == null)
                    return NotFound($"No se encontró ningún envío con tracking '{nroTracking}'.");

                // 3) De lo contrario OK(200) con el DTO
                return Ok(dto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Intente nuevamente");
            }
        }



        // Clase -> estandares por el profe
        //[HttpGet]
        //public IActionResult GetById(string id) 
        //{

        //    try 
        //    {
        //        int idUsuario;
        //        int.TryParse(id, out idUsuario);
        //        if (idUsuario == 0) 
        //        {
        //            throw new BadRequest("Hubo un problema al obtener el usuario");
        //        }


        //    }
        //    catch (Exception){
        //        Error error = new Error(500, "Error, proba nuevamente");
        //        return StatusCor
        //    }
        //}


    }
}
