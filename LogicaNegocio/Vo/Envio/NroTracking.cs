using LogicaNegocio.Excepciones.EnvioExceptions;

namespace LogicaNegocio.Vo.Envio
{
    public record NroTracking
    {
        public string Value { get; }

        public NroTracking(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new NroTrackingException("Tracking inválido");
            Value = value;
        }
    }
}
