using LogicaNegocio.Excepciones.UsuarioExceptions;

namespace LogicaNegocio.Vo.Usuario
{
    public record Nombre
    {
        public string Value { get; }

        public Nombre(string value)
        {
            Value = value;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Value))
                throw new NombreException("Nombre incorrecto o vacío");
        }
    }
}
