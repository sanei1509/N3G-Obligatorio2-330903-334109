using CasoUsoCompartida.DTOs.Envios.CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
