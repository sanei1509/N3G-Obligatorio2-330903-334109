namespace AppCliente.Models.Envios
{
    public record FiltroBusquedaEnviosDto
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Estado { get; set; }
        public string? Comentario { get; set; }
    }
}
