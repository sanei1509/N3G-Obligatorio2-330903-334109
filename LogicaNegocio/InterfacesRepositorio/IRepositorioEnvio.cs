using LogicaNegocio.Entidades.Envios;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioEnvio: 
        IRepositorioAdd<Envio>,
        IRepositorioGetAll<Envio>,
        IRepositorioGetById<Envio>,
        IRepositorioUpdate<Envio>,
        IRepositorioGetByNroTracking<Envio>
    {

    }
}
