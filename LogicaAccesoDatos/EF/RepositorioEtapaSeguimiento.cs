using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Excepciones.EnvioExceptions;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Vo.Envio;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioEtapaSeguimiento : IRepositorioEtapaSeguimiento
    {
        //inyeccion de Librerira Contexto
        private LibreriaContext _context;

        public RepositorioEtapaSeguimiento(LibreriaContext context)
        {
            _context = context;
        }

        public void Add(EtapaSeguimiento obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Esta vacio");
            }
            //obj.Validar();
            _context.EtapaSeguimientos.Add(obj);
            _context.SaveChanges();
        }


        public IEnumerable<EtapaSeguimiento> GetAll()
        {
            return _context.EtapaSeguimientos.ToList();
        }

        //public EtapaSeguimiento GetByNroTracking(NroTracking nroTracking)
        //{
        //    EtapaSeguimiento unE = _context.EtapaSeguimientos
        //        .FirstOrDefault(etapa => etapa.NroTracking == nroTracking);
        //    if (unE == null)
        //    {
        //        throw new NroTrackingException("No se encontro la etapa con ese nro tracking");
        //    }
        //    return unE;
        //}
    }
}
