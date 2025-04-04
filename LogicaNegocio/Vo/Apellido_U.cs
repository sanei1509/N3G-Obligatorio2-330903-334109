using LogicaNegocio.Excepciones.UsuarioExceptions;

namespace LogicaNegocio.Vo
{
    public record Apellido_U
    {
        public string Value { get; }

        public Apellido_U(string value) {
            if (string.IsNullOrEmpty(value))
                throw new ApellidoException("Apellido incorrecto o vacío");
            Value = value;
        }
    }
}
