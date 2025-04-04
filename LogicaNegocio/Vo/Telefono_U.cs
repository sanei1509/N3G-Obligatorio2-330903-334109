using LogicaNegocio.Excepciones.UsuarioExceptions;

namespace LogicaNegocio.Vo
{
    public record Telefono_U
    {
        public string Value { get; }

        public Telefono_U(string value) {
            if (string.IsNullOrEmpty(value))
                throw new TelefonoException("Télefono incorrecto o vacío");
            Value = value;
        }
    }
}
