using CasoUsoCompartida.DTOs.Envios.CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.Excepciones.UsuarioExceptions;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso.Envios
{
    public class CrearEnvio : IAdd<CrearEnvioDto>
    {
        private IRepositorioEnvio _repo;

        public CrearEnvio(IRepositorioEnvio repo)
        {
            _repo = repo;
        }

        public void Execute(CrearEnvioDto envioDto)
        {
            // Primero, verificamos si ya existe un usuario con ese correo.
            var envioExistente = _repo.GetById(envioDto.Id);
            if (envioExistente != null)
            {
                //throw new YaExisteUsuarioException("Ya existe un usuario con ese correo.");
            }
            _repo.Add(EnvioMapper.FromDto(envioDto));
        }
    }
}
