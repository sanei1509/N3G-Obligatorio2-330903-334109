namespace LogicaNegocio.Excepciones.UsuarioExceptions
{
    public class ClaveException: LogicaNegocioException
    {
        public ClaveException()
        {
        }

        public ClaveException(string? message) : base(message) 
        { 
        }

    }
}
