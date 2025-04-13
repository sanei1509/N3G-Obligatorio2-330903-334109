using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioUsuario: IRepositorioUsuario
    {
        //inyeccion de Librerira Contexto
        private LibreriaContext _context;

        public RepositorioUsuario(LibreriaContext context)
        {
            _context = context;
        }

        public void Add(Usuario obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Esta vacio");
            }
            obj.Validar();
            _context.Usuarios.Add(obj);
            _context.SaveChanges();
        }

        public Usuario GetByEmail(string correo)
        {
            Usuario unU = _context.Usuarios
                .FirstOrDefault(usuario => usuario.Correo.Value == correo);
            if (unU == null)
            {
                throw new Exception("No se encontro el usuario con ese correo");
            }
            return unU;

        }

        public IEnumerable<Usuario> GetAll()
        {
            throw new NotImplementedException("Este método no se usa/implementa aún.");
        }

        public Usuario GetById(int id)
        {
            throw new NotImplementedException("Pendiente de implementar.");
        }

        public void Remove(int id)
        {
            throw new NotImplementedException("Pendiente de implementar.");
        }

        public void Update(Usuario obj)
        {
            throw new NotImplementedException("Pendiente de implementar.");
        }
    }
}
