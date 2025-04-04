using LogicaNegocio.Excepciones.UsuarioExceptions;

namespace LogicaNegocio.Vo
{
    public record Correo_U
    {
        public string Value {get;}
        public Correo_U(string value) {
            if (string.IsNullOrEmpty(value))
                throw new CorreoException("Correo incorrecto o vacío");
            Value = value;
        }
    }
}
