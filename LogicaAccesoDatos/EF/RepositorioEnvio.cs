using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioEnvio : IRepositorioEnvio
    {
        private LibreriaContext _context;

        public RepositorioEnvio(LibreriaContext context)
        {
            _context = context;
        }

        public void Add(Envio obj)
        {
            //obj.Validar();
            _context.Envios.Add(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Envio> GetAll()
        {
            return _context.Envios
                        .Where(e => e.Estado == EstadoEnvio.EN_PROCESO)
                        .Include(e => e.Empleado)
                        .Include(e => e.Cliente)
                        .Include(e => (e as Comun).LugarRetiro)
                        .ToList();
        }

        public void Update(int id, Envio obj)
        {
            Envio unE = GetById(id);
            unE.Update(obj);
            _context.Envios.Update(unE);
            _context.SaveChanges();
        }

        public Envio GetById(int id)
        {
            Envio unE = _context.Envios
                .FirstOrDefault(envio => envio.Id == id);
            if (unE == null)
            {
                throw new Exception("No se encontro el id");
            }
            return unE;
        }

    }
}
