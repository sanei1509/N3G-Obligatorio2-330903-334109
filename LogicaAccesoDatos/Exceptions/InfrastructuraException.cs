using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Exceptions
{
    public abstract class InfrastructuraException : Exception
    {
        string _message;
        public InfrastructuraException()
        {
        }

        public InfrastructuraException(string? message) : base(message)
        {
            _message = message;
        }

        protected InfrastructuraException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public abstract int StatusCode();

        public Error Error()
        {
            return new Error(
                StatusCode(),
                _message
                );

        }

    }
}
