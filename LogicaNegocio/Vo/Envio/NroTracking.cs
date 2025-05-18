using LogicaNegocio.Excepciones.EnvioExceptions;

namespace LogicaNegocio.Vo.Envio
{
    public record NroTracking
    {
        public string Value { get; }

        public NroTracking(string value)
        {
            Value = value;
        }
    }
}
