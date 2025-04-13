using LogicaNegocio.Excepciones.UsuarioExceptions;

namespace LogicaNegocio.Vo
{
    public record Nombre
    {
        public string Value { get; }

        public Nombre(string value) {
            if (string.IsNullOrEmpty(value))
                throw new NombreException("Nombre incorrecto o vacío");
            Value = value;
        }
    }
}
