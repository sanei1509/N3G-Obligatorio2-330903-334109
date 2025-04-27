using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Enums;
using LogicaNegocio.Vo.Envio;

namespace LogicaNegocio.Entidades.Envios
{
    public class Comun : Envio
    {
        public Agencia LugarRetiro { get; set; }
        protected Comun() 
        {
        }

        public Comun(Agencia lugarRetiro, int id, NroTracking nroTracking, Empleado empleado, Cliente cliente, Peso peso, EstadoEnvio estado) : base (id, nroTracking, empleado, cliente, peso, estado)
        {
            LugarRetiro = lugarRetiro;
        }
            
    }
}
