using LogicaNegocio.Excepciones.UsuarioExceptions;

namespace LogicaNegocio.Vo.Usuario
{
    public record Apellido
    {
        public string Value { get; }

        public Apellido(string value)
        {
            Value = value;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Value))
                throw new ApellidoException("Apellido incorrecto o vacío");
        }

    }
}
