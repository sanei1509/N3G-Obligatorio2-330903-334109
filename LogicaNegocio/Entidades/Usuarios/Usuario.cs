using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.Vo;

namespace LogicaNegocio.Entidades.Usuarios.Usuario
{
    public class Usuario : IEntity
    {
        public int Id { get; set; }
        public Nombre Nombre { get; set; }
        public Apellido Apellido { get; set; }
        public Correo Correo { get; set; }
        public Clave Clave { get; set; }
        public Telefono Telefono { get; set; }


        protected Usuario()
        {
        }

        public Usuario(int id, Nombre nombre, Apellido apellido, Correo correo, Clave clave, Telefono telefono)
        {
            Id = id;
            Nombre = Nombre;
            Apellido = Apellido;
            Correo = Correo;
            Clave = Clave;
            Telefono = Telefono;
            Validar();
        }


        public void Validar() 
        {
            
        }

    }
}
