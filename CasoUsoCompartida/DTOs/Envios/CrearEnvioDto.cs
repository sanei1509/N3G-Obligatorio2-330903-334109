using LogicaNegocio.Enums;


    namespace CasoUsoCompartida.DTOs.Envios
    {
    public record CrearEnvioDto(
            string TipoEnvio,
            string CorreoEmpleado,
            string CorreoCliente,
            //int NroTracking,
            //int EmpleadoId,
            //int ClienteId,
            decimal Peso,
            // Para Comun
            int LugarRetiroId,
            // Para Urgente
            string? DireccionPostal,
            bool? Entregado
        )
    {
    }
}


