    namespace AppCliente.Models.Envios
    {
    public record CrearEnvioDto(
            string TipoEnvio,
            string CorreoEmpleado,
            string CorreoCliente,
            decimal Peso,
            int LugarRetiroId,
            string? DireccionPostal,
            bool? Entregado
        )
    {
    }
}


