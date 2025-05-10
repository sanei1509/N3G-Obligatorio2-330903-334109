namespace LogicaNegocio.Excepciones.EnvioExceptions
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
