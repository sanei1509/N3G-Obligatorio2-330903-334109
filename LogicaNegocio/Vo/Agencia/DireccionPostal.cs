using LogicaNegocio.Excepciones.AgenciaException;
using LogicaNegocio.Excepciones.EnvioExceptions;

namespace LogicaNegocio.Vo.Agencia
{
    public record DireccionPostal
    {
        public string Value { get; }

        public DireccionPostal(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new DireccionException("Dirección postal incorrecta o vacío");
            Value = value;
        }
    }
}
