namespace LogicaNegocio.Vo.Envio
{
    public record Entregado
    {
        public bool Value { get; }

        public Entregado(bool value)
        {
            Value = value;
        }
    }
}
