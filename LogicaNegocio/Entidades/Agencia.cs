using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Vo.Agencia;
using LogicaNegocio.Vo.Usuario;

namespace LogicaNegocio.Entidades
{
    public class Agencia : IEntity
    {
        public int Id { get; set; }
        public Nombre Nombre { get; set; }
        public DireccionPostal DireccionPostal { get; set; }
        public Ubicacion Ubicacion { get; set; }


        protected Agencia()
        {
        }

        public Agencia(int id, Nombre nombre, DireccionPostal direccionPostal, Ubicacion ubicacion)
        {
            Id = id;
            Nombre = nombre;
            DireccionPostal = direccionPostal;
            Ubicacion = ubicacion;
        }
    }
}
