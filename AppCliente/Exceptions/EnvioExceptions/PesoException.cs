﻿namespace AppCliente.Exceptions.EnvioExceptions
{
    public class PesoException : LogicaNegocioException
    {
        public PesoException()
        {
        }

        public PesoException(string? message) : base(message)
        {
        }

    }
}
