using LogicaNegocio.InterfacesDominio;

namespace LogicaNegocio.Entidades
{
    public class Auditoria : IEntity
    {
        public int Id { get; set; }
        public int IdResponsable {  get; set; }
        public int IdEmpleado { get; set; }
        public string Accion {  get; set; }
        public DateTime Fecha { get; set; }


        protected Auditoria()
        {
        }

        public Auditoria(int id, int idResponsable, int idEmpleado, string accion, DateTime fecha)
        {
            Id = id;
            IdResponsable = idResponsable;
            IdEmpleado = idEmpleado;
            Accion = accion;
            Fecha = fecha;
        }
    }
}