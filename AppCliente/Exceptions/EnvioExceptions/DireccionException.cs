namespace AppCliente.Exceptions.EnvioExceptions
{
    public class DireccionException : LogicaNegocioException
    {
        public DireccionException()
        {
        }

        public DireccionException(string? message) : base(message)
        {
        }

    }
}
