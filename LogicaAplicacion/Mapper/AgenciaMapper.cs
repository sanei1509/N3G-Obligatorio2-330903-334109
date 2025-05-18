using CasoUsoCompartida.DTOs;
using CasoUsoCompartida.DTOs.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.Mapper
{
    public static class AgenciaMapper
    {
        public static AgenciaListadoDto ToDto(Agencia agencia)
        {
            return new AgenciaListadoDto(
                Id: agencia.Id,
                Nombre: agencia.Nombre.Value
            );
        }

        public static IEnumerable<AgenciaListadoDto> ToListaDto(IEnumerable<Agencia> agencias)
        {
            List<AgenciaListadoDto> agenciaListadoDto = new List<AgenciaListadoDto>();
            foreach (var item in agencias)
            {
                agenciaListadoDto.Add(new AgenciaListadoDto(
                                                item.Id,
                                                item.Nombre.Value
                                        ));
            }
            return agenciaListadoDto;
        }
    }
}
