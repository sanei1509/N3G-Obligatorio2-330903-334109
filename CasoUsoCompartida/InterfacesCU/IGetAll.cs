namespace CasoUsoCompartida.InterfacesCU
{
    public interface IGetAll<T>
    {
        IEnumerable<T> Execute();
    }
}
