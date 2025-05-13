namespace CasoUsoCompartida.InterfacesCU
{
    public interface IUpdate<T>
    {
        void Execute(int id, T obj);
    }
}
