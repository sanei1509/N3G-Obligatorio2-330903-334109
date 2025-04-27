
using LogicaAccesoDatos.EF.Config;
using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF
{
    public class LibreriaContext: DbContext
    {
        // el nombre de la tablas dbset
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Envio> Envios { get; set; }
        public DbSet<Agencia> Agencias { get; set; }
        public DbSet<EtapaSeguimiento> EtapaSeguimientos { get; set; }
        
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
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new EnvioConfiguration());
            modelBuilder.ApplyConfiguration(new EtapaSeguimientoConfiguration());
            modelBuilder.ApplyConfiguration(new AgenciaConfiguration());
        }
    }
}
