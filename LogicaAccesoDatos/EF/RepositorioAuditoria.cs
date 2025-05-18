using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioAuditoria: IRepositorioAuditoria
    {
        private LibreriaContext _context;

        public RepositorioAuditoria(LibreriaContext context)
        {
            _context = context;
        }

        public void Add(Auditoria obj)
        {
            _context.Auditorias.Add(obj);
            _context.SaveChanges();
        }
    }
}
