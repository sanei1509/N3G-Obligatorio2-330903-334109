using CasoUsoCompartida.DTOs.Envios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.InterfacesRepositorio;


namespace LogicaAplicacion.CasosUso.Envios
{
    public class GetAllEnvios : IGetAll<EnvioListadoDto>
    {
        private IRepositorioEnvio _repo;

        public GetAllEnvios(IRepositorioEnvio repo)
        {
            this._repo = repo;
        }

        public IEnumerable<EnvioListadoDto> Execute()
        {
            return EnvioMapper.ToListaDto(_repo.GetAll());
        }
    }
}
