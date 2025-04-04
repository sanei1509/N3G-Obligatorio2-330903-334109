namespace LogicaNegocio.Excepciones.UsuarioExceptions
{
    public class CorreoException: LogicaNegocioException
    {
        public CorreoException()
        {
        }

        public CorreoException(string? message) : base(message) 
        { 
        }

    }
}
