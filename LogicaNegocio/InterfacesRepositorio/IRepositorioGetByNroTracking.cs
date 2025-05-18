
using LogicaNegocio.Vo.Envio;

namespace LogicaNegocio.InterfacesRepositorio
{ 
    public interface IRepositorioGetByNroTracking<T>
    {
        T GetByNroTracking(string nroTracking);
    }
}
