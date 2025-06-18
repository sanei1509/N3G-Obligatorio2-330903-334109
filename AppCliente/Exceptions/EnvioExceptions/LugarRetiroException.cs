namespace AppCliente.Exceptions.EnvioExceptions
{
    public class LugarRetiroException : LogicaNegocioException
    {
        public LugarRetiroException()
        {
        }

        public LugarRetiroException(string? message) : base(message)
        {
        }

    }
}
