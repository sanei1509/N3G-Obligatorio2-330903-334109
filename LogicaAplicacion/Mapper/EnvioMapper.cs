

using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Entidades;
using LogicaNegocio.Vo.Agencia;
using LogicaNegocio.Vo.Envio;
using LogicaNegocio.Enums;
using CasoUsoCompartida.DTOs.Envios;
using CasoUsoCompartida.DTOs.EtapaSeguimiento;

namespace LogicaAplicacion.Mapper
{
    public class EnvioMapper
    {
        // De DTO a Entidad
        public static Envio FromDto(
            CrearEnvioDto dto,
            Empleado empleado,
            EstadoEnvio estado,
            Cliente cliente,
            NroTracking nroVo,
            Agencia? lugarRetiro
            )
        {
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
                    estado
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
                    estado);

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
            envio.Cliente.Telefono.Value,
            // aquí viene la lista de etapas:
            envio.FechaCreacion,
            envio.EtapasSeguimiento?
                .Select(es => new EtapaSeguimientoDto(
                    es.Id,
                    es.IdEnvio,
                    es.NroTracking.Value,
                    es.IdEmpleado,
                    es.Empleado.Nombre.Value + " " + es.Empleado.Apellido.Value,
                    es.Comentario.Value,
                    es.Fecha.Value
                ))
                .ToList()
                );
        }


        //public static EnvioDto ToDtoEnvio(Envio envio)
        //{
        //    return new EnvioDto(
        //        envio.Id,
        //        envio.NroTracking,
        //        envio.Empleado,
        //        envio.Cliente,
        //        envio.Peso,
        //        envio.Estado,
        //        envio.Discriminator,
        //        envio.FechaCreacion,
        //        envio.FechaFinalizacion.Value,
        //        Etapas: envio.EtapasSeguimiento.Select(es => new EtapaSeguimientoDto(
        //            Id: es.Id,
        //            IdEnvio: es.IdEnvio,
        //            NroTracking: es.NroTracking.Value,
        //            IdEmpleado: es.IdEmpleado,
        //            NombreEmpleado: es.Empleado.Nombre.Value + " " + es.Empleado.Apellido.Value,
        //            Comentario: es.Comentario.Value,
        //            Fecha: es.Fecha.Value

        //    );
        //}

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
