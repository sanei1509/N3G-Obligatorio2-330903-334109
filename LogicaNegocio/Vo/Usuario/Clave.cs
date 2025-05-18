using LogicaNegocio.Excepciones.UsuarioExceptions;

namespace LogicaNegocio.Vo.Usuario
{
    public record Clave
    {
        public string Value { get; }

        public Clave(string value)
        {
            Value = value;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrWhiteSpace(Value))
                throw new ClaveException("Clave incorrecta o vacía");
        }
    }
}
