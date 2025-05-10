using LogicaNegocio.Enums;


    namespace CasoUsoCompartida.DTOs.Envios
    {
        public record CrearEnvioDto(
                string TipoEnvio,
                int NroTracking,
                int EmpleadoId,
                int ClienteId,
                decimal Peso,
                EstadoEnvio Estado,
                // Para Comun
                int? LugarRetiroId,
                // Para Urgente
                string DireccionPostal,
                bool? Entregado
            )
        {
        }
    }


