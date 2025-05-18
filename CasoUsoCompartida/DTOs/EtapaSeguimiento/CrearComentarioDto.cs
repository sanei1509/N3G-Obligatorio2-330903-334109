using LogicaNegocio.Vo.Envio;
using LogicaNegocio.Vo.EtapaSeguimiento;

namespace CasoUsoCompartida.DTOs.EtapaSeguimiento
{
    public record CrearComentarioDto(
            int IdEnvio,
            string NroTracking,
            string CorreoEmpleado,
            string Comentario
            //int EmpleadoId
        )
    {
    }
}
