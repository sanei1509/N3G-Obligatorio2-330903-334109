using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.Excepciones.UsuarioExceptions;
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
            // Primero, verificamos si ya existe un usuario con ese correo.
            var usuarioExistente = _repo.GetByEmail(usuarioDto.Correo);
            if (usuarioExistente != null)
            {
                throw new YaExisteUsuarioException("Ya existe un usuario con ese correo.");
            }
            _repo.Add(UsuarioMapper.FromDto(usuarioDto));
        }
    }
}
