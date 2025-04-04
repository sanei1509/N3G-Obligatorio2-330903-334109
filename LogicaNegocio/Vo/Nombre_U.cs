using LogicaNegocio.Excepciones.UsuarioExceptions;

namespace LogicaNegocio.Vo
{
    public record Nombre_U
    {
        public string Value { get; }

        public Nombre_U(string value) {
            if (string.IsNullOrEmpty(value))
                throw new NombreException("Nombre incorrecto o vacío");
            Value = value;
        }
    }
}
