using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.InterfacesRepositorio;

namespace Libreria.LogicaAplicacion.CasoUso.Usuarios
{
    public class EditarUsuario : IUpdate<CrearUsuarioDto>
    {
        private IRepositorioUsuario _repo;

        public EditarUsuario(IRepositorioUsuario repo)
        {
            _repo = repo;
        }

        public void Execute(int id, CrearUsuarioDto obj)
        {
            _repo.Update(id, UsuarioMapper.FromDto(obj));
        }
    }
}