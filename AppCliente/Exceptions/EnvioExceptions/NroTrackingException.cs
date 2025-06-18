namespace AppCliente.Exceptions.EnvioExceptions
{
    public class NroTrackingException : LogicaNegocioException
    {
        public NroTrackingException()
        {
        }

        public NroTrackingException(string? message) : base(message)
        {
        }

    }
}
