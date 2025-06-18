namespace AppCliente.Exceptions.UsuarioExceptions
{
    public class ApellidoException: LogicaNegocioException
    {
        public ApellidoException()
        {
        }

        public ApellidoException(string? message) : base(message) 
        { 
        }

    }
}
