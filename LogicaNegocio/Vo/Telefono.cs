using LogicaNegocio.Excepciones.UsuarioExceptions;

namespace LogicaNegocio.Vo
{
    public record Telefono
    {
        public string Value { get; }

        public Telefono(string value) {
            if (string.IsNullOrEmpty(value))
                throw new TelefonoException("Teléfono incorrecto o vacío");
            Value = value;
        }
    }
}
