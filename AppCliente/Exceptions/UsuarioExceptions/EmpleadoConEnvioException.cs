
namespace AppCliente.Exceptions.UsuarioExceptions
{
    public class EmpleadoConEnvioException : LogicaNegocioException
    {
        public EmpleadoConEnvioException()
        {
        }

        public EmpleadoConEnvioException(string? message) : base(message)
        {
        }

    }
}
