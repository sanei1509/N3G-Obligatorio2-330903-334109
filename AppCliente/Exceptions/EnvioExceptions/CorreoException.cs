namespace AppCliente.Exceptions.EnvioExceptions
{
    public class CorreoException : LogicaNegocioException
    {
        public CorreoException()
        {
        }

        public CorreoException(string? message) : base(message)
        {
        }

    }
}
