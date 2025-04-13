using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Vo;

namespace LogicaNegocio.Entidades
{
    public class Agencia: IEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        protected Agencia()
        {
        }

        public Agencia(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
