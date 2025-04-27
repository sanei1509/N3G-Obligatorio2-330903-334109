using LogicaNegocio.Excepciones.AgenciaException;

namespace LogicaNegocio.Vo.Envio
{
    public record Peso
    {
        public decimal Value { get; }

        public Peso(decimal value)
        {
            if (value < 0)
                throw new AgenciaException("El peso debe ser un número válido y positivo");
            Value = value;
        }
    }
}
