namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioGetByEmail<T>
    {
        T GetByEmail(string correo);
    }
}
