using LogicaNegocio.Excepciones.EnvioExceptions;

namespace LogicaNegocio.Vo.Envio
{
    public record Peso
    {
        public decimal Value { get; }

        public Peso(decimal value)
        {
            Value = value;
            Validar();

        }

        private void Validar()
        {
            if (Value <= 0)
                throw new PesoException("El peso debe ser un número válido y positivo");
        }
    }
}
