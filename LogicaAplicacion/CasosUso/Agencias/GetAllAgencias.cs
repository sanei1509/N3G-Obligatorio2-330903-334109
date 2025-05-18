using CasoUsoCompartida.DTOs;
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso.Agencias
{
    public class GetAllAgencias : IGetAll<AgenciaListadoDto>
    {
        private IRepositorioAgencia _repo;

        public GetAllAgencias(IRepositorioAgencia repo)
        {
            this._repo = repo;
        }

        public IEnumerable<AgenciaListadoDto> Execute()
        {
            return AgenciaMapper.ToListaDto(_repo.GetAll());
        }
    }
}
