using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Enums;
using LogicaNegocio.Vo.Agencia;
using LogicaNegocio.Vo.Envio;

namespace LogicaNegocio.Entidades.Envios
{
    public class Urgente : Envio
    {
        public DireccionPostal DireccionPostal { get; set; }
        public Entregado Entregado { get; set; }
        protected Urgente() 
        {
        }
        public Urgente(DireccionPostal direccionPostal, Entregado entregado, int id, NroTracking nroTracking, Empleado empleado, Cliente cliente, Peso peso, EstadoEnvio estado) : base(id, nroTracking, empleado, cliente, peso, estado)
        {
            DireccionPostal = direccionPostal;
            Entregado = entregado;
        }
    }
}
