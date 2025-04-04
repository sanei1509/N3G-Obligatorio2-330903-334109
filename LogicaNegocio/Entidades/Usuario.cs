using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaNegocio.Entidades
{
    public class Usuario
    {
        static int ultimoId = 1;

        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Correo { get; set; }
        public int Clave { get; set; }
        public int Telefono { get; set; }
        protected Usuario()
        {
        }

        public Usuario(int id, Nombre nombre, Email email, string password, string rol)
        {
            Id = id;
            Nombre = nombre;
            Email = email;
            Passwrod = password;
            Rol = rol;
            Validar();
        }


    }
}
