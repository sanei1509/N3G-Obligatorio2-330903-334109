using CasoUsoCompartida.DTOs.Envios;
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.CasosUso.Usuarios;
using LogicaNegocio.Entidades.Envios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Globalization;
using System.Security.Claims;

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
        [SwaggerOperation(Summary = "Lista todos los envíos", Description = "Devuelve una lista de todos los envíos disponibles.")]
        [SwaggerResponse(200, "Listado obtenido con éxito")]
        [SwaggerResponse(500, "Error interno del servidor")]
        public IActionResult GetAll()
        {
            try
            {
                var envios = _getAll.Execute();
                if (envios.Count() == 0)
                {
                    return StatusCode(200, "No se encontraron envios para listar");
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
        [SwaggerOperation(Summary = "Buscar por número de tracking", Description = "Devuelve un envío si coincide con el número de tracking.")]
        [SwaggerResponse(200, "Envío encontrado")]
        [SwaggerResponse(400, "Número inválido")]
        [SwaggerResponse(404, "No encontrado")]
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


        [HttpGet("enviosDelCliente")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Obtiene los envíos del cliente autenticado",
            Description = "Filtra todos los envíos por el ID del cliente extraído del token JWT y los ordena por fecha de creación descendente."
        )]
        [SwaggerResponse(200, "Lista de envíos obtenida correctamente o mensaje indicando que no hay envíos")]
        [SwaggerResponse(401, "No autorizado: token inválido o no contiene el ID del cliente")]
        [SwaggerResponse(500, "Error interno del servidor")]
        public IActionResult ListarEnviosCliente()
        {
            try
            {
                // 1) Obtengo el Id del usuario logueado
                var idCliente = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(idCliente) || !int.TryParse(idCliente, out var userId))
                    return StatusCode(401, "No estas autorizado");

                // 2) Traigo todos los envíos (desde el caso de uso)
                var todos = _getAll.Execute();

                // 3) Filtro solo los del cliente y ordeno por fecha (suponiendo que EnvioListadoDto tiene FechaCreacion)
                var enviosDelCliente = todos
                    .Where(e => e.ClienteId == userId)
                    .OrderByDescending(e => e.FechaCreacion)
                    .ToList();

                if (!enviosDelCliente.Any())
                    return StatusCode(200, "No se encontraron envios del cliente logueado"); // 204

                return Ok(enviosDelCliente);
            }
            catch (Exception)
            {
                return StatusCode(500, "Intente nuevamente");
            }
        }


        [HttpGet("filtrar")]
        [Authorize]
        [SwaggerOperation(Summary = "Filtra los envíos del cliente autenticado", Description = "Permite filtrar por fecha, estado o palabra clave en los comentarios de seguimiento.")]
        [SwaggerResponse(200, "Lista filtrada")]
        [SwaggerResponse(401, "No autorizado")]
        public IActionResult FiltrarEnvios([FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin, [FromQuery] string? estado, [FromQuery] string? comentario)
        {
            // ✅ Obtener el ID del cliente desde el token
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int clienteId = 0;
            if (!int.TryParse(claim, out clienteId))
                return Unauthorized(new { message = "No autorizado." });


            // 2) Obtener todos los envíos
            var todos = _getAll.Execute().AsQueryable();

            // 3) Filtrar por cliente
            var filtrados = todos.Where(e => e.ClienteId == clienteId);

            // 4) Filtrar por fechas
            if (fechaInicio.HasValue)
                filtrados = filtrados.Where(e => e.FechaCreacion.Date >= fechaInicio.Value.Date);
            if (fechaFin.HasValue)
                filtrados = filtrados.Where(e => e.FechaCreacion.Date <= fechaFin.Value.Date);

            // 5) Filtrar por estado
            if (!string.IsNullOrWhiteSpace(estado) && !string.Equals(estado, "Todos", StringComparison.OrdinalIgnoreCase))
                filtrados = filtrados.Where(e => string.Equals(e.EstadoEnvio.ToString(), estado, StringComparison.OrdinalIgnoreCase));

            // 6) Filtrar por comentario en etapas (si vino)
            if (!string.IsNullOrWhiteSpace(comentario))
            {
                var comparer = CultureInfo.CurrentCulture.CompareInfo;
                var options = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace;

                filtrados = filtrados.Where(e =>
                    e.Etapas.Any(et => !string.IsNullOrEmpty(et.Comentario) &&
                        comparer.IndexOf(et.Comentario, comentario, options) >= 0));
            }

            // 7) Ordenar
            List<EnvioListadoDto> resultado;

            if (!string.IsNullOrWhiteSpace(comentario))
            {
                resultado = filtrados
                    .Select(e => new
                    {
                        Envio = e,
                        FechaComentario = e.Etapas
                            .Where(et => !string.IsNullOrEmpty(et.Comentario) &&
                                         CultureInfo.CurrentCulture.CompareInfo
                                            .IndexOf(et.Comentario, comentario, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0)
                            .Select(et => (DateTime?)et.Fecha)
                            .OrderByDescending(f => f)
                            .FirstOrDefault() ?? DateTime.MinValue
                    })
                    .OrderByDescending(x => x.FechaComentario)
                    .Select(x => x.Envio)
                    .ToList();
            }
            else
            {
                resultado = filtrados
                    .OrderBy(e => e.NroTracking)
                    .ToList();
            }



            return Ok(resultado);
        }


    }
}
