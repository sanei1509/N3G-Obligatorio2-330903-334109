using LogicaNegocio.Excepciones.AgenciaException;

namespace LogicaNegocio.Vo.Agencia
{
    public record Ubicacion
    {
        public decimal Latitud { get; }
        public decimal Longitud { get; }


        public Ubicacion(decimal latitud, decimal longitud)
        {
            Latitud = latitud;
            Longitud = longitud;
        }
    }
}
