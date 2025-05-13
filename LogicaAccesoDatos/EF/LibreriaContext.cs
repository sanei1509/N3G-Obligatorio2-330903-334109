
using LogicaAccesoDatos.EF.Config;
using LogicaAccesoDatos.EF.Config.Envios;
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
            modelBuilder.ApplyConfiguration(new UrgenteConfiguration());
            modelBuilder.ApplyConfiguration(new ComunConfiguration());
            modelBuilder.ApplyConfiguration(new EtapaSeguimientoConfiguration());
            modelBuilder.ApplyConfiguration(new AgenciaConfiguration());


            // 1) Seed de la entidad Usuario (sin los VOs)
            modelBuilder.Entity<Usuario>().HasData(
                new
                {
                    Id = 1,
                    Discriminator = "Admin"
                },
                new
                {
                    Id = 2,
                    Discriminator = "Admin"
                },
                // Ahora dos Clientes
                new { Id = 3, Discriminator = "Cliente" },
                new { Id = 4, Discriminator = "Cliente" }
            );

            // 2) Seed de each Value Object via OwnsOne:

            // Nombre
            modelBuilder.Entity<Usuario>()
                .OwnsOne(u => u.Nombre)
                .HasData(
                    //ADMIN
                    new { UsuarioId = 1, Value = "Juan" },
                    new { UsuarioId = 2, Value = "María" },
                    //CLIENTE
                    new { UsuarioId = 3, Value = "Carlos" },  // Cliente 1
                    new { UsuarioId = 4, Value = "Fernanda" }   // Cliente 2
                );

            // Apellido
            modelBuilder.Entity<Usuario>()
                .OwnsOne(u => u.Apellido)
                .HasData(
                    new { UsuarioId = 1, Value = "Pérez" },
                    new { UsuarioId = 2, Value = "Gómez" },
                    new { UsuarioId = 3, Value = "Rodríguez" },  // Cliente 1
                    new { UsuarioId = 4, Value = "López" }   // Cliente 2
                );

            // Correo
            modelBuilder.Entity<Usuario>()
                .OwnsOne(u => u.Correo)
                .HasData(
                    new { UsuarioId = 1, Value = "juan@gmail.com" },
                    new { UsuarioId = 2, Value = "maria@gmail.com" },
                    new { UsuarioId = 3, Value = "carlitos@gmail.com" }, //cliente
                    new { UsuarioId = 4, Value = "fernanda@gmail.com" }  //cliente
                );

            // Clave
            modelBuilder.Entity<Usuario>()
                .OwnsOne(u => u.Clave)
                .HasData(
                    new { UsuarioId = 1, Value = "admin123" },
                    new { UsuarioId = 2, Value = "admin123" },
                    new { UsuarioId = 3, Value = "cliente123" },  // Cliente 1
                    new { UsuarioId = 4, Value = "cliente123" }   // Cliente 2
                );

            // Telefono
            modelBuilder.Entity<Usuario>()
                .OwnsOne(u => u.Telefono)
                .HasData(
                    new { UsuarioId = 1, Value = "099123456" },
                    new { UsuarioId = 2, Value = "099654321" },
                    new { UsuarioId = 3, Value = "099122555" },
                    new { UsuarioId = 4, Value = "099777888" }
                );


            // =================AGENCIA
            // 3) Seed de la entidad Agencia (solo la PK)
            modelBuilder.Entity<Agencia>().HasData(
                new { Id = 1 },
                new { Id = 2 }
            );

            // 4) Seed de cada Value Object de Agencia

            // Nombre (VO mapeado con OwnsOne in AgenciaConfiguration)
            modelBuilder.Entity<Agencia>()
                .OwnsOne(a => a.Nombre)
                .HasData(
                    new { AgenciaId = 1, Value = "Agencia San Ramón" },
                    new { AgenciaId = 2, Value = "Agencia Montevideo" }
                );

            // DireccionPostal
            modelBuilder.Entity<Agencia>()
                .OwnsOne(a => a.DireccionPostal)
                .HasData(
                    new { AgenciaId = 1, Value = "Av. Principal 123" },
                    new { AgenciaId = 2, Value = "Calle Secundaria 456" }
                );

            // Ubicacion (VO con Latitud y Longitud)
            modelBuilder.Entity<Agencia>()
                .OwnsOne(a => a.Ubicacion)
                .HasData(
                    new { AgenciaId = 1, Latitud = -34.9011m, Longitud = -56.1645m },
                    new { AgenciaId = 2, Latitud = -34.9050m, Longitud = -56.1900m }
                );

        }
    }
}
