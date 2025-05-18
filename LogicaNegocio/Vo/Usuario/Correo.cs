using LogicaNegocio.Excepciones.UsuarioExceptions;

namespace LogicaNegocio.Vo.Usuario
{
    public record Correo
    {
        public string Value { get; }
        public Correo(string value)
        {
            Value = value;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Value))
                throw new CorreoException("Correo incorrecto o vacío");
        }
    }
}
