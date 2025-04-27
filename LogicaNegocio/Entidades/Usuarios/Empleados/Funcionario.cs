using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Vo.Usuario;

namespace LogicaNegocio.Entidades.Usuarios.Empleados
{
    public class Funcionario : Empleado
    {
        protected Funcionario() { }
        public Funcionario(int id, Nombre nombre, Apellido apellido, Correo correo, Clave clave, Telefono telefono) : base(id, nombre, apellido, correo, clave, telefono)
        { }
    }
}
