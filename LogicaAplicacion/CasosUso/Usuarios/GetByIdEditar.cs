using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso.Usuarios
{
    public class GetByIdEditar: IGetById<CrearUsuarioDto>
    {
        private IRepositorioUsuario _repo;

        public GetByIdEditar(IRepositorioUsuario repo)
        {
            _repo = repo;
        }

        public CrearUsuarioDto Execute(int id)
        {
            return UsuarioMapper.ToDtoEdit(_repo.GetById(id));
        }
    }
}
