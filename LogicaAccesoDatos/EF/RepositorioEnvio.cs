using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Text;

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
                        
                        .Include(e => e.Empleado)
                        .Include(e => e.Cliente)
                        .Include(e => (e as Comun).LugarRetiro)
                         // Incluimos las etapas de seguimiento y el empleado de cada etapa para poder luego proyectarlas en el DTO
                        .Include(e => e.EtapasSeguimiento)
                            .ThenInclude(es => es.Empleado)
                                .ThenInclude(emp => emp.Nombre)
                        .Include(e => e.EtapasSeguimiento)
                            .ThenInclude(es => es.Empleado)
                                .ThenInclude(emp => emp.Apellido)
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

        public Envio GetByNroTracking(string nroTracking)
        {
            return _context.Envios
                        .Include(e => e.Empleado)
                            .ThenInclude(u => u.Nombre)
                        .Include(e => e.Empleado)
                            .ThenInclude(u => u.Apellido)
                        .Include(e => e.Cliente)
                            .ThenInclude(u => u.Correo)
                        .Include(e => e.Cliente)
                            .ThenInclude(u => u.Telefono)
                        // *** incluir las etapas ***
                        .Include(e => e.EtapasSeguimiento)
                            .ThenInclude(es => es.Empleado)     // para NombreEmpleado
                                .ThenInclude(emp => emp.Nombre)
                        .Include(e => e.EtapasSeguimiento)
                            .ThenInclude(es => es.Empleado)
                                .ThenInclude(emp => emp.Apellido)
                        .SingleOrDefault(e => e.NroTracking.Value == nroTracking);
           
            }

        public bool TieneEnviosAsignados(int idEmpleado)
        {
            return _context.Envios.Any(e => e.Id == idEmpleado);
        }

    }
}
