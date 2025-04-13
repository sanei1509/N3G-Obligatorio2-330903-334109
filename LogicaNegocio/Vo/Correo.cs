using LogicaNegocio.Excepciones.UsuarioExceptions;

namespace LogicaNegocio.Vo
{
    public record Correo
    {
        public string Value {get;}
        public Correo(string value) {
            if (string.IsNullOrEmpty(value))
                throw new CorreoException("Correo incorrecto o vacío");
            Value = value;
        }
    }
}
