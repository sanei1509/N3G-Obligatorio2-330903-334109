namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioGetById<T>
    {
        T GetById(int id);
    }
}
