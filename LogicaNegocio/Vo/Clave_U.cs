using LogicaNegocio.Excepciones.UsuarioExceptions;

namespace LogicaNegocio.Vo
{
    public record Clave_U
    {
        public string Value { get; }

        public Clave_U(string value) {
            if (string.IsNullOrEmpty(value))
                throw new ClaveException("Clave incorrecta o vacía");
            Value = value;
        }
    }
}
