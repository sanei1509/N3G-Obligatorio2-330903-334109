using LogicaNegocio.Excepciones.UsuarioExceptions;

namespace LogicaNegocio.Vo
{
    public record Clave
    {
        public string Value { get; }

        public Clave(string value) {
            if (string.IsNullOrEmpty(value))
                throw new ClaveException("Clave incorrecta o vacía");
            Value = value;
        }
    }
}
