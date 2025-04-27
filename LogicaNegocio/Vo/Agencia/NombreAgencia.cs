using LogicaNegocio.Excepciones.AgenciaException;

namespace LogicaNegocio.Vo.Agencia
{
    public record NombreAgencia
    {
        public string Value { get; }

        public NombreAgencia(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new AgenciaException("Nombre de agencia incorrecto o vacío");
            Value = value;
        }
    }
}
