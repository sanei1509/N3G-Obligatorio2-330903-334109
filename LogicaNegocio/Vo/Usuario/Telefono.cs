using LogicaNegocio.Excepciones.UsuarioExceptions;

namespace LogicaNegocio.Vo.Usuario
{
    public record Telefono
    {
        public string Value { get; }

        public Telefono(string value)
        {
            Value = value;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Value))
                throw new TelefonoException("Teléfono incorrecto o vacío");
        }
    }
}
