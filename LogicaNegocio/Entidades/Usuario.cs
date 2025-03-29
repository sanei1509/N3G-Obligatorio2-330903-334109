using System.Security.Principal;

namespace LogicaNegocio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }


        protected Usuario()
        {
        }

        public Usuario(int id, string nombre)
        {
            id = id;
            nombre = nombre;
        }


    }
}
