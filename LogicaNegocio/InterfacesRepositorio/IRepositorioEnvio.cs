using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Usuario;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioEnvio: 
        IRepositorioAdd<Envio>,
        IRepositorioGetAll<Envio>,
        IRepositorioGetById<Envio>,
        IRepositorioUpdate<Envio>,
        IRepositorioGetByNroTracking<Envio>
    {

    bool TieneEnviosAsignados(int idEmpleado);
    
    }
}
