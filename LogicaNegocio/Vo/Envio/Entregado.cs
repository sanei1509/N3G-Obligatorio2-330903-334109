namespace LogicaNegocio.Vo.Envio
{
    public record Entregado
    {
        public bool Value { get; set; }

        public Entregado(bool value)
        {
            Value = value;
        }
    }
}
