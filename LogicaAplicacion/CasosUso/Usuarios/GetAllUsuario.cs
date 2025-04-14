using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso.Usuarios
{
    public class GetAllUsuario : IGetAll<UsuarioListadoDto>
    {
        private IRepositorioUsuario _repo;

        public GetAllUsuario(IRepositorioUsuario repo)
        {
            this._repo = repo;
        }

        public IEnumerable<UsuarioListadoDto> Execute()
        {
            return UsuarioMapper.ToListaDto(_repo.GetAll());
        }
    }
}
