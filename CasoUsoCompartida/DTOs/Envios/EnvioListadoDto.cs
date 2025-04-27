using System.Security.Cryptography.X509Certificates;
using

using LogicaNegocio.Enums;

namespace CasoUsoCompartida.DTOs.Envios
{
    namespace CasoUsoCompartida.DTOs.Usuarios
    {
        public record EnvioListadoDto(
            int Id,
            string TipoEnvio,
            int NroTracking,
            int EmpleadoId,
            int ClienteId,
            decimal Peso,
            EstadoEnvio EstadoEnvio
        )
        {
        }
    }

}
