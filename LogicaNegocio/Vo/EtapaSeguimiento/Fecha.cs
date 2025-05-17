using LogicaNegocio.Excepciones.EtapaSeguimientoExceptions;

namespace LogicaNegocio.Vo.EtapaSeguimiento
{
    public record Fecha
    {
        public DateTime Value { get; }

        public Fecha(DateTime value)
        {
            //if (string.IsNullOrEmpty(value))
            //    throw new ComentarioException("Comentario incorrecto o vacío");
            Value = value;
        }
    }
}
