namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioGetAll<T>
    {
        IEnumerable<T> GetAll();
    }
}