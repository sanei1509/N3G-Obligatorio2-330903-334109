using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Vo.Envio;
using LogicaNegocio.Vo.EtapaSeguimiento;

namespace LogicaNegocio.Entidades
{
    public class EtapaSeguimiento
    {
        public int Id { get; set; }
        public int IdEnvio { get; set; }
        public NroTracking NroTracking { get; set; }
        public int IdEmpleado { get; set; }
        public Envio Envio { get; set; }
        public Comentario Comentario { get; set; }
        public Empleado Empleado { get; set; }
        public Fecha Fecha { get; set; }

        protected EtapaSeguimiento()
        { }

        public EtapaSeguimiento(int id, int idEnvio, NroTracking nroTracking, int idEmpleado, Envio envio, Comentario comentario, Empleado empleado, Fecha fecha)
        {
            Id = id;
            IdEnvio = idEnvio;
            NroTracking = nroTracking;
            IdEmpleado = idEmpleado;
            Envio = envio;
            Comentario = comentario;
            Empleado = empleado;
            Fecha = fecha;
        }
    }
}
