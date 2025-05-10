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

        protected Envio() { }

        public Envio(int id, NroTracking nroTracking, Empleado empleado, Cliente cliente, Peso peso, EstadoEnvio estado)
        {
            Id = id;
            NroTracking = nroTracking;
            Empleado = empleado;
            Cliente = cliente;
            Peso = peso;
            Estado = estado;
        }

        public void FinalizarEnvio(Envio obj)
        {
            // En este caso solo cambiamos el valor del esado
            Estado = obj.Estado;
        }
    }
}
