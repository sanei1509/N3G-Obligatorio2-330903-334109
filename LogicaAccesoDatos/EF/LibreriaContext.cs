
using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF
{
    public class LibreriaContext: DbContext
    {
        // el nombre de la tablas dbset
        //public DbSet<Agencia> Agencias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        //public DbSet<Envio> Envios { get; set; }
        // la cadena 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"
            Data Source=(localdb)\MSSQLLocalDB;
            Initial Catalog=Obligatorio;
            Integrated Security=True;
            ");
        }

        //sentencia fluenAPI
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // mapear lo VO
            modelBuilder.Entity<Usuario>(usuario =>
            {
                // Mapea Nombre (VO) a la columna "Nombre"
                usuario.OwnsOne(u => u.Nombre, nombre =>
                {
                    nombre.Property(n => n.Value).HasColumnName("Nombre");
                });

                // Mapea Apellido (VO) a la columna "Apellido"
                usuario.OwnsOne(u => u.Apellido, apellido =>
                {
                    apellido.Property(a => a.Value).HasColumnName("Apellido");
                });

                // Mapea Correo (VO) a la columna "Correo"
                usuario.OwnsOne(u => u.Correo, correo =>
                {
                    correo.Property(c => c.Value).HasColumnName("Correo");
                });

                // Mapea Clave (VO) a la columna "Clave"
                usuario.OwnsOne(u => u.Clave, clave =>
                {
                    clave.Property(c => c.Value).HasColumnName("Clave");
                });

                // Mapea Telefono (VO) a la columna "Telefono"
                usuario.OwnsOne(u => u.Telefono, telefono =>
                {
                    telefono.Property(t => t.Value).HasColumnName("Telefono");
                });

            });
        }
    }
}
