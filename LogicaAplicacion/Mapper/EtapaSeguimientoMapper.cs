using CasoUsoCompartida.DTOs.EtapaSeguimiento;
using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Entidades;
using LogicaNegocio.Vo.EtapaSeguimiento;

namespace LogicaAplicacion.Mapper
{
    public static class EtapaSeguimientoMapper
    {
        /// Mapea sólo los valores puros del DTO y las entidades ya cargadas
        /// a un nuevo agregado EtapaSeguimiento.
        public static EtapaSeguimiento FromDto(
            CrearComentarioDto dto,
            Envio envio,
            Empleado empleado
           )
        {
            // 1) Construir los Value Objects
            var comentarioVo = new Comentario(dto.);
            var fechaVo = new Fecha(DateTime.UtcNow);

            // 2) Devolver la entidad
            return new EtapaSeguimiento(
                id: 0,                  // EF lo generará
                idEnvio: dto.idEnvio,        // FK al envío
                nroTracking: envio.NroTracking,  // tracking heredado
                idEmpleado: empleado.Id,        // FK al empleado
                envio: envio,
                comentario: comentarioVo,
                empleado: empleado,
                fecha: fechaVo
            );
        }
    }
}