
//using CasoUsoCompartida.DTOs.EtapaSeguimiento;
//using LogicaNegocio.Enums;

namespace AppCliente.Models.Envios
{

    public record EnvioListadoDto(
        int Id,
        string NroTracking,
        int EmpleadoId,
        int ClienteId,
        decimal Peso,
        //EstadoEnvio EstadoEnvio,
        string TipoEnvio,
        string Correo,
        string Telefono
        //DateTime FechaCreacion,
        //IEnumerable<EtapaSeguimientoDto>? Etapas = null
    )
    {
    }
}


