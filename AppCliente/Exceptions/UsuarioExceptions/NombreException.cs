namespace AppCliente.Exceptions.UsuarioExceptions
{
    public class NombreException: LogicaNegocioException
    {
        public NombreException()
        {
        }

        public NombreException(string? message) : base(message) 
        { 
        }

    }
}
