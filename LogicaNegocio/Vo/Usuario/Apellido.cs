using LogicaNegocio.Excepciones.UsuarioExceptions;

namespace LogicaNegocio.Vo.Usuario
{
    public record Apellido
    {
        public string Value { get; }

        public Apellido(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ApellidoException("Apellido incorrecto o vacío");
            Value = value;
        }
    }
}
