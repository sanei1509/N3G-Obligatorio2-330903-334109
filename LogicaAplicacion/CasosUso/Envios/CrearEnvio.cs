
using CasoUsoCompartida.DTOs.Envios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso.Envios
{
    public class CrearEnvio : IAdd<CrearEnvioDto>
    {
        private IRepositorioEnvio _repo;
        private IRepositorioUsuario _usuario;

        public CrearEnvio(IRepositorioEnvio repo, IRepositorioUsuario usuario)
        {
            _repo = repo;
            _usuario = usuario;
        }

        public void Execute(CrearEnvioDto envioDto)
        {
            var empleado = _usuario.GetById(envioDto.EmpleadoId) as Empleado;
            var cliente = _usuario.GetById(envioDto.ClienteId) as Cliente;

            // Primero, verificamos si ya existe un usuario con ese correo.
            var envioExistente = _repo.GetById(envioDto.ClienteId);
            if (envioExistente != null)
            {
                //throw new YaExisteUsuarioException("Ya existe un usuario con ese correo.");
            }
            _repo.Add(EnvioMapper.FromDto(envioDto, empleado, cliente));
        }
    }
}
