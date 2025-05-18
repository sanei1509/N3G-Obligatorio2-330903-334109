using LogicaNegocio.Excepciones.EtapaSeguimientoExceptions;

namespace LogicaNegocio.Vo.EtapaSeguimiento
{
    public record Comentario
    {
        public string Value { get; }

        public Comentario(string value)
        {
            Value = value;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Value))
                throw new ComentarioException("Comentario incorrecto o vacío");
        }
    }
}

