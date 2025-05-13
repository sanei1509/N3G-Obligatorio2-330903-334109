
using LogicaNegocio.Enums;

namespace CasoUsoCompartida.DTOs.Envios
{

    public record EnvioListadoDto(
        int Id,
        string NroTracking,
        int EmpleadoId,
        int ClienteId,
        decimal Peso,
        EstadoEnvio EstadoEnvio,
        string TipoEnvio,
        string Correo,
        string Telefono
    )
    {
    }
}


