

using CasoUsoCompartida.DTOs.Envios.CasoUsoCompartida.DTOs.Usuarios;
using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Entidades;
using LogicaNegocio.Vo.Agencia;
using LogicaNegocio.Vo.Envio;

namespace LogicaAplicacion.Mapper
{
    public class EnvioMapper
    {
        // De DTO a Entidad
        public static Envio FromDto(CrearEnvioDto dto,
            Empleado empleado,
            Cliente cliente,
            Agencia lugarRetiro = null)
        {
            var nroVo = new NroTracking(dto.NroTracking);
            var pesoVo = new Peso(dto.Peso);


            if (dto.TipoEnvio == "Comun")
            {
                return new Comun(
                    lugarRetiro,
                    0,
                    nroVo,
                    empleado,
                    cliente,
                    pesoVo,
                    dto.Estado
                    );
            }
            else
            {
                return new Urgente(
                    new DireccionPostal(dto.DireccionPostal),
                    new Entregado(dto.Entregado ?? false),
                    0,
                    nroVo,
                    empleado,
                    cliente,
                    pesoVo,
                    dto.Estado);

            }



        }





        }
}
