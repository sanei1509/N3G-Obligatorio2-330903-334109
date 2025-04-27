

using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Entidades;
using LogicaNegocio.Vo.Agencia;
using LogicaNegocio.Vo.Envio;
using LogicaNegocio.Enums;
using CasoUsoCompartida.DTOs.Envios;

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

         public static EnvioListadoDto ToDto(Envio envio)
        {
            return new EnvioListadoDto(envio.Id,
            envio.NroTracking.Value,
            envio.Empleado.Id,
            envio.Cliente.Id,
            envio.Peso.Value,
            envio.Estado,
            envio.Discriminator,
            envio.Cliente.Correo.Value,
            envio.Cliente.Telefono.Value);
        }

        public static IEnumerable<EnvioListadoDto> ToListaDto(IEnumerable<Envio> envios)
        {
            List<EnvioListadoDto> envioListadoDtos = new List<EnvioListadoDto>();
            foreach (var envio in envios)
            {
                envioListadoDtos.Add(ToDto(envio));
            }
            return envioListadoDtos;
        }
    }
}
