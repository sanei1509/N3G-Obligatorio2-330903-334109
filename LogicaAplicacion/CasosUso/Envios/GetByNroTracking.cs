
using CasoUsoCompartida.DTOs.Envios;
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso.Envios
{
    public class GetByNroTracking : IGetByNroTracking<EnvioListadoDto>
    {
        private IRepositorioEnvio _repo;

        public GetByNroTracking(IRepositorioEnvio repo)
        {
            _repo = repo;
        }

        public EnvioListadoDto Execute(string nroTracking)
        {
            var envio = _repo.GetByNroTracking(nroTracking);
            if (envio == null)
            return null;

            return EnvioMapper.ToDto(envio);
        }
    }
}