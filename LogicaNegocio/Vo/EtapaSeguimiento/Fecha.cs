using LogicaNegocio.Excepciones.EtapaSeguimientoExceptions;

namespace LogicaNegocio.Vo.EtapaSeguimiento
{
    public record Fecha
    {
        public DateTime Value { get; }

        public Fecha(DateTime value)
        {
            Value = value;
        }
    }
}
