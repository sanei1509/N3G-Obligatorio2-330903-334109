using LogicaNegocio.Vo.Envio;
using LogicaNegocio.Vo.EtapaSeguimiento;

namespace CasoUsoCompartida.DTOs.EtapaSeguimiento
{
    public record CrearComentarioDto(
            int idEnvio,
            NroTracking NroTracking,
            string CorreoEmpleado,
             Comentario Comentario,
            int EmpleadoId
           
        )
    {
    }
}
