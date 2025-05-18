using LogicaNegocio.Excepciones.AgenciaException;
using LogicaNegocio.Excepciones.EnvioExceptions;

namespace LogicaNegocio.Vo.Agencia
{
    public record DireccionPostal
    {
        public string Value { get; }

        public DireccionPostal(string value)
        {
            Value = value;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Value))
                throw new DireccionException("Dirección postal incorrecta o vacío");
        }
    }
}
