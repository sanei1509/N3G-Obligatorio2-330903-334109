
using CasoUsoCompartida.InterfacesCU;
using LogicaNegocio.InterfacesRepositorio;

namespace Libreria.LogicaAplicacion.CasoUso.Usuarios
{
    public class BorrarUsuario: IRemove
    {
        private IRepositorioUsuario _repo;

        public BorrarUsuario(IRepositorioUsuario repo)
        {
            _repo = repo;
        }

        public void Execute(int id)
        {
            _repo.Remove(id);
        }

    }
}