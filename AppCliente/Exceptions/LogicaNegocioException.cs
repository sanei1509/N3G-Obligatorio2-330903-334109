namespace AppCliente.Exceptions
{
    public class LogicaNegocioException: Exception
    { 
        public LogicaNegocioException() 
        {
        }

        public LogicaNegocioException(string? message) : base(message) 
        { 
        }
    }
}
