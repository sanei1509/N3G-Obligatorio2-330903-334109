using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesDominio;

namespace LogicaNegocio.Entidades.Envios
{
    public class Envio : IEntity
    {
        public int Id { get; set; }
        public int NroTracking { get; set; }
        public Usuario Empleado { get; set; }
        public Usuario Cliente { get; set; }
        decimal Peso { get; set; }
        EstadoEnvio Estado;
        protected Envio() { }

        public Envio(int id, int nroTracking, Usuario empleado, Usuario cliente, decimal peso, EstadoEnvio estado)
        {
            Id = id;
            NroTracking = nroTracking;
            Empleado = empleado;
            Cliente = cliente;
            Peso = peso;
            Estado = estado;
        }
    }
}
