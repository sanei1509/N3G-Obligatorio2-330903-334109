using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.Vo.Usuario;
using Microsoft.AspNetCore.Identity;

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

        public void SetPassword(string plainTextPassword)
        {
            // Hashear y asignar al VO Clave
            var hasher = new PasswordHasher<Usuario>();
            var hash = hasher.HashPassword(this, plainTextPassword);
            this.Clave = new Clave(hash);
        }

        public bool CheckPassword(string plainTextPassword)
        {
            var hasher = new PasswordHasher<Usuario>();
            // Intentar verificar el VO Clave.Value
            var result = hasher.VerifyHashedPassword(this, this.Clave.Value, plainTextPassword);
            return result != PasswordVerificationResult.Failed;
        }

    }
}
