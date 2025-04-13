namespace CasoUsoCompartida.InterfacesCU
{
    public interface IGetById<T>
    {
        T Execute(int id);
    }
}
