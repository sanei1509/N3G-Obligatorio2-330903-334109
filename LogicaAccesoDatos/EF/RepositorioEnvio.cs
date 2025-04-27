using LogicaNegocio.Entidades.Envios;
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
                           .Include(e => e.Peso)
                           .Include(e => e.NroTracking)
                           .ToList();
        }

        // Estos métodos no los vamos a usar aún
        public Envio GetById(int id) =>
            throw new NotSupportedException("No está implementado para Envios");

        public Envio GetByEmail(string correo) =>
            throw new NotSupportedException("No aplica a Envios");

        public void Remove(int id) =>
            throw new NotSupportedException("No está implementado para Envios");

        public void Update(int id, Envio obj) =>
            throw new NotSupportedException("No está implementado para Envios");
    }
}
