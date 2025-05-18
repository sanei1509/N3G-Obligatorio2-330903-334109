using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasoUsoCompartida.DTOs.EtapaSeguimiento
{
    public record EtapaSeguimientoDto(
        int Id,
        int IdEnvio,
        string NroTracking,
        int IdEmpleado,
        string NombreEmpleado,
        string Comentario,
        DateTime Fecha
    );
}
