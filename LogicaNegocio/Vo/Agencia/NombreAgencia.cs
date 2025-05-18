using LogicaNegocio.Excepciones.AgenciaException;

namespace LogicaNegocio.Vo.Agencia
{
    public record NombreAgencia
    {
        public string Value { get; }

        public NombreAgencia(string value)
        {
            Value = value;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Value))
                throw new NombreAgenciaExeption("Nombre de agencia incorrecto o vacío");
        }
    }
}
