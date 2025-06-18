﻿namespace AppCliente.Exceptions.EnvioExceptions
{
    public class TipoEnvioException : LogicaNegocioException
    {
        public TipoEnvioException()
        {
        }

        public TipoEnvioException(string? message) : base(message)
        {
        }

    }
}
