namespace CasoUsoCompartida.InterfacesCU
{
    public interface IGetByEmail<T>
    {
        T Execute(string correo);
    }
}
