using LogicaNegocio.Excepciones.AgenciaException;

namespace LogicaNegocio.Vo
{
    public record Nombre_Agencia
    {
        public string Value { get; }

        public Nombre_Agencia(string value) {
            if (string.IsNullOrEmpty(value))
                throw new AgenciaException("Nombre de agencia incorrecto o vacío");
            Value = value;
        }
    }
}
