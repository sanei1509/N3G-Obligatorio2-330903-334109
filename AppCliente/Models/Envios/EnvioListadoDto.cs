using CasoUsoCompartida.DTOs.EtapaSeguimiento;
using LogicaNegocio.Enums;
//using LogicaNegocio.Enums;

namespace AppCliente.Models.Envios
{

    public record EnvioListadoDto(
        int Id,
        string NroTracking,
        int EmpleadoId,
        int ClienteId,
        decimal Peso,
        string EstadoEnvio,
        string TipoEnvio,
        string Correo,
        string Telefono,
        DateTime FechaCreacion,
        IEnumerable<EtapaSeguimientoDto>? Etapas = null
    )
    {
        public string EstadoEnvioTexto => EstadoEnvio switch
        {
             "EN_PROCESO"  => "En proceso",
            "FINALIZADO" => "Finalizado",
            _ => "Desconocido"
        };
    }
}


