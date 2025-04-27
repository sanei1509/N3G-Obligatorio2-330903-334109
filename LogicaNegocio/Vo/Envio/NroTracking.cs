namespace LogicaNegocio.Vo.Envio
{
    public record NroTracking
    {
        public int Value { get; }

        public NroTracking(int value)
        {
            //if (!value)
            //    throw new AgenciaException("Nro Tracking incorrecto");
            Value = value;
        }
    }
}
