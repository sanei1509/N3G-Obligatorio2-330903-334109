using CasoUsoCompartida.DTOs.EtapaSeguimiento;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Enums;
using LogicaNegocio.Vo.Envio;

namespace CasoUsoCompartida.DTOs.Envios
{
    public record EnvioDto (
            int Id,
            NroTracking NroTracking,
            Empleado Empleado,
            Cliente Cliente,
            Peso Peso,
            EstadoEnvio Estado,
            string Discriminator,
            DateTime FechaCreacion,
            DateTime? FechaFinalizacion,
            IEnumerable<EtapaSeguimientoDto> Etapas
        )
    {
    }
}
