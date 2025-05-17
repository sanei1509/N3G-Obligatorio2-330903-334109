using LogicaNegocio.Excepciones.EtapaSeguimientoExceptions;

namespace LogicaNegocio.Vo.EtapaSeguimiento
{
    public record Comentario
    {
        public string Value { get; }

        public Comentario(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ComentarioException("Comentario incorrecto o vacío");
            Value = value;
        }
    }
}

