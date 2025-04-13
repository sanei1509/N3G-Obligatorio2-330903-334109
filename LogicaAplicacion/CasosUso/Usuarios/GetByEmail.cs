using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso.Usuarios
{
    public class GetByEmail: IGetByEmail<UsuarioDto>
    {
        private IRepositorioUsuario _repo;

        public GetByEmail(IRepositorioUsuario repo)
        {
            _repo = repo;
        }

        public UsuarioDto Execute(string correo) 
        {
            return UsuarioMapper.ToDto(_repo.GetByEmail(correo));
        }
    }
}
