using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.Vo.Envio;
using LogicaNegocio.Vo.Usuario;

namespace LogicaNegocio.Entidades.Envios
{
    public class Envio : IEntity
    {
        public int Id { get; set; }
        public NroTracking NroTracking { get; set; }

        public Empleado Empleado { get; set; }
        public Cliente Cliente { get; set; }
        public Peso Peso { get; set; }
        public EstadoEnvio Estado { get; set; }
        public string Discriminator { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaFinalizacion { get; set; }

        protected Envio() { }

        public Envio(int id, NroTracking nroTracking, Empleado empleado, Cliente cliente, Peso peso, EstadoEnvio estado)
        {
            Id = id;
            NroTracking = nroTracking;
            Empleado = empleado;
            Cliente = cliente;
            Peso = peso;
            Estado = estado;
            FechaCreacion = DateTime.Now;
        }

        public virtual void FinalizarEnvio(Envio obj)
        {
            // En este caso solo cambiamos el valor del esado
            Estado = EstadoEnvio.FINALIZADO;
            FechaFinalizacion = DateTime.Now;
        }

        public void Update(Envio obj)
        {
            // En este caso solo cambiamos estos valores 
            Estado = obj.Estado;
            FechaFinalizacion = obj.FechaFinalizacion;
        }
    }
}
