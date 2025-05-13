using LogicaNegocio.Entidades.Usuarios.Usuario;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioUsuario:
        IRepositorioAdd<Usuario>,
        IRepositorioGetAll<Usuario>,
        IRepositorioGetById<Usuario>,
        IRepositorioUpdate<Usuario>,
        IRepositorioDelete<Usuario>,
        IRepositorioGetByEmail<Usuario>
    {

    }
}
