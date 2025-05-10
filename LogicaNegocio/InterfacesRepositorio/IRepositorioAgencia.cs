using LogicaNegocio.Entidades;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioAgencia: 
        IRepositorioGetAll<Agencia>,
        IRepositorioGetById<Agencia>
    {
    }
}
