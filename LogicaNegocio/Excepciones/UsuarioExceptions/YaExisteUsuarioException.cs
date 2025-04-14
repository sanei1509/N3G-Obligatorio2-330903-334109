namespace LogicaNegocio.Excepciones.UsuarioExceptions
{
    public class YaExisteUsuarioException : LogicaNegocioException
    {
        public YaExisteUsuarioException()
        {
        }

        public YaExisteUsuarioException(string? message) : base(message)
        {
        }

    }
}
