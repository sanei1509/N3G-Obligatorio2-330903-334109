using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF
    {
    public class RepositorioAgencia : IRepositorioAgencia
    {
            private LibreriaContext _context;

            public RepositorioAgencia(LibreriaContext context)
            {
                _context = context;
            }

            public IEnumerable<Agencia> GetAll()
                {
                    return _context.Agencias
                                   .ToList();
                }

            // Estos métodos no los vamos a usar aún
            public Agencia GetById(int id) 
            {
                Agencia unA = _context.Agencias
                            .FirstOrDefault(agencia => agencia.Id == id);

                if (unA == null)
                {
                    throw new Exception("No se encontro el id");
                }

                return unA;
            }
        }
    }
