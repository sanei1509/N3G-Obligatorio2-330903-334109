namespace AppCliente.Exceptions.UsuarioExceptions
{
    public class TelefonoException: LogicaNegocioException
    {
        public TelefonoException()
        {
        }

        public TelefonoException(string? message) : base(message) 
        { 
        }

    }
}
