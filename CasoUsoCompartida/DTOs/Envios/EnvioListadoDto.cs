
using LogicaNegocio.Enums;

namespace CasoUsoCompartida.DTOs.Envios
{

    public record EnvioListadoDto(
        int Id,
        int NroTracking,
        int EmpleadoId,
        int ClienteId,
        decimal Peso,
        EstadoEnvio EstadoEnvio,
        string Discriminator,
        string Correo,
        string Telefono
    )
    {
    }
}


