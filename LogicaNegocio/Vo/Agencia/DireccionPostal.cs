using LogicaNegocio.Excepciones.AgenciaException;

namespace LogicaNegocio.Vo.Agencia
{
    public record DireccionPostal
    {
        public string Value { get; }

        public DireccionPostal(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new AgenciaException("Dirección postal incorrecta o vacío");
            Value = value;
        }
    }
}
