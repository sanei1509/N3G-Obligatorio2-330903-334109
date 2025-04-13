using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso.Usuarios
{
    public class CrearUsuario: IAdd<CrearUsuarioDto>
    {
        private IRepositorioUsuario _repo;

        public CrearUsuario(IRepositorioUsuario repo)
        {
            _repo = repo;
        }

        public void Execute (CrearUsuarioDto usuarioDto)
        {
            _repo.Add(UsuarioMapper.FromDto(usuarioDto));
        }
    }
}
