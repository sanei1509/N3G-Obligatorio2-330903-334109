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
            //if (unU == null)
            //{
            //    throw new Exception("No se encontro el usuario con ese correo");
            //}
            return unU;

        }

        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario GetById(int id)
        {
            Usuario unU = _context.Usuarios
                .FirstOrDefault(usuario => usuario.Id == id);
            if (unU == null)
            {
                throw new Exception("No se encontro el id");
            }
            return unU;

        }

        public void Remove(int id)
        {
            Usuario unU = GetById(id);
            _context.Usuarios.Remove(unU);
            _context.SaveChanges();
        }


        public void Update(int id, Usuario obj)
        {
            Usuario unU = GetById(id);
            unU.Update(obj);
            _context.Usuarios.Update(unU);
            _context.SaveChanges();
        }
    }
}
