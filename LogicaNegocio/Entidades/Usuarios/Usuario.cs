using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.Vo.Usuario;

namespace LogicaNegocio.Entidades.Usuarios.Usuario
{
    public abstract class Usuario : IEntity
    {
        public int Id { get; set; }
        public Nombre Nombre { get; set; }
        public Apellido Apellido { get; set; }
        public Correo Correo { get; set; }
        public Clave Clave { get; set; }
        public Telefono Telefono { get; set; }
        public string Discriminator {get; set;}
        public bool Eliminado { get; set; } = false;

        protected Usuario()
        {
        }

        public Usuario(int id, Nombre nombre, Apellido apellido, Correo correo, Clave clave, Telefono telefono)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Clave = clave;
            Telefono = telefono;
            Validar();
        }


        public void Validar()
        {
            //ValidarExistenciaUsuario();
            //ValidarNombre();
            //ValidarApellido();
            //ValidarCorreo();
            //ValidarClave();
            //ValidarTelefono();
        }

        public void Update(Usuario obj)
        {
            // En este caso solo cambiamos estos valores 
            Nombre = obj.Nombre;
            Apellido = obj.Apellido;
            Correo = obj.Correo;
            Clave = obj.Clave;
            Telefono = obj.Telefono;
        }

        // Método para “borrado lógico”
        public void Eliminar()
        {
            Eliminado = true;
        }


    }
}
