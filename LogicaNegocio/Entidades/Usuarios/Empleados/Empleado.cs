using LogicaNegocio.Vo.Usuario;

namespace LogicaNegocio.Entidades.Usuarios.Usuario
{
    public class Empleado : Usuario
    {
        protected Empleado() { }
        public Empleado(int id, Nombre nombre, Apellido apellido, Correo correo, Clave clave, Telefono telefono) : base(id, nombre, apellido, correo, clave, telefono)
        { }
    }
}
