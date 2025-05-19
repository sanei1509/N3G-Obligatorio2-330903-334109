
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
        public DbSet<Auditoria> Auditorias { get; set; }


        public LibreriaContext(DbContextOptions<LibreriaContext> options): base(options)
        {
        }


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
            modelBuilder.ApplyConfiguration(new AuditoriaConfiguration());


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
                new { Id = 4, Discriminator = "Cliente" },
                new { Id = 5, Discriminator = "Admin" },
                new { Id = 6, Discriminator = "Cliente" },
                new { Id = 7, Discriminator = "Cliente" },
                new { Id = 8, Discriminator = "Cliente" },
                new { Id = 9, Discriminator = "Cliente" },
                new { Id = 10, Discriminator = "Cliente" },
                new { Id = 11, Discriminator = "Cliente" },
                new { Id = 12, Discriminator = "Cliente" }


            );

            // 2) Seed de cada Value Object via OwnsOne:

// Nombre
modelBuilder.Entity<Usuario>()
    .OwnsOne(u => u.Nombre)
    .HasData(
        // ADMIN
        new { UsuarioId = 1, Value = "Juan" },
        new { UsuarioId = 2, Value = "María" },
        new { UsuarioId = 5, Value = "Gerente" },
        // CLIENTES
        new { UsuarioId = 3, Value = "Carlos" },
        new { UsuarioId = 4, Value = "Fernanda" },
        new { UsuarioId = 6, Value = "Luis" },
        new { UsuarioId = 7, Value = "Sofía" },
        new { UsuarioId = 8, Value = "Pedro" },
        new { UsuarioId = 9, Value = "Ana" },
        new { UsuarioId = 10, Value = "Martín" },
        new { UsuarioId = 11, Value = "Laura" },
        new { UsuarioId = 12, Value = "Diego" }
    );

// Apellido
modelBuilder.Entity<Usuario>()
    .OwnsOne(u => u.Apellido)
    .HasData(
        // ADMIN
        new { UsuarioId = 1, Value = "Pérez" },
        new { UsuarioId = 2, Value = "Gómez" },
        new { UsuarioId = 5, Value = "Apellido" },
        // CLIENTES
        new { UsuarioId = 3, Value = "Rodríguez" },
        new { UsuarioId = 4, Value = "López" },
        new { UsuarioId = 6, Value = "Martínez" },
        new { UsuarioId = 7, Value = "Ruiz" },
        new { UsuarioId = 8, Value = "Díaz" },
        new { UsuarioId = 9, Value = "Fernández" },
        new { UsuarioId = 10, Value = "Sosa" },
        new { UsuarioId = 11, Value = "Castro" },
        new { UsuarioId = 12, Value = "Navarro" }
    );

// Correo
modelBuilder.Entity<Usuario>()
    .OwnsOne(u => u.Correo)
    .HasData(
        // ADMIN
        new { UsuarioId = 1, Value = "juan@gmail.com" },
        new { UsuarioId = 2, Value = "maria@gmail.com" },
        new { UsuarioId = 5, Value = "administrador@gmail.com" },
        // CLIENTES
        new { UsuarioId = 3, Value = "carlitos@gmail.com" },
        new { UsuarioId = 4, Value = "fernanda@gmail.com" },
        new { UsuarioId = 6, Value = "luis@gmail.com" },
        new { UsuarioId = 7, Value = "sofia@gmail.com" },
        new { UsuarioId = 8, Value = "pedro@gmail.com" },
        new { UsuarioId = 9, Value = "ana@gmail.com" },
        new { UsuarioId = 10, Value = "martin@gmail.com" },
        new { UsuarioId = 11, Value = "laura@gmail.com" },
        new { UsuarioId = 12, Value = "diego@gmail.com" }
    );

// Clave
modelBuilder.Entity<Usuario>()
    .OwnsOne(u => u.Clave)
    .HasData(
        // ADMIN
        new { UsuarioId = 1, Value = "admin123" },
        new { UsuarioId = 2, Value = "admin123" },
        new { UsuarioId = 5, Value = "Gerente2025." },
        // CLIENTES
        new { UsuarioId = 3, Value = "cliente123" },
        new { UsuarioId = 4, Value = "cliente123" },
        new { UsuarioId = 6, Value = "cliente123" },
        new { UsuarioId = 7, Value = "cliente123" },
        new { UsuarioId = 8, Value = "cliente123" },
        new { UsuarioId = 9, Value = "cliente123" },
        new { UsuarioId = 10, Value = "cliente123" },
        new { UsuarioId = 11, Value = "cliente123" },
        new { UsuarioId = 12, Value = "cliente123" }
    );

// Teléfono
modelBuilder.Entity<Usuario>()
    .OwnsOne(u => u.Telefono)
    .HasData(
        // ADMIN
        new { UsuarioId = 1, Value = "099123456" },
        new { UsuarioId = 2, Value = "099654321" },
        new { UsuarioId = 5, Value = "099100200" },
        // CLIENTES
        new { UsuarioId = 3, Value = "099122555" },
        new { UsuarioId = 4, Value = "099777888" },
        new { UsuarioId = 6, Value = "099666111" },
        new { UsuarioId = 7, Value = "099222333" },
        new { UsuarioId = 8, Value = "099333444" },
        new { UsuarioId = 9, Value = "099444555" },
        new { UsuarioId = 10, Value = "099555666" },
        new { UsuarioId = 11, Value = "099888999" },
        new { UsuarioId = 12, Value = "099999000" }
    );


            // =================AGENCIA

            // 1) Seed de la entidad Agencia (solo la PK)
            modelBuilder.Entity<Agencia>().HasData(
                new { Id = 1 },
                new { Id = 2 },
                new { Id = 3 },
                new { Id = 4 },
                new { Id = 5 },
                new { Id = 6 },
                new { Id = 7 },
                new { Id = 8 }
            );

            // 2) Nombre (VO mapeado con OwnsOne in AgenciaConfiguration)
            modelBuilder.Entity<Agencia>()
                .OwnsOne(a => a.Nombre)
                .HasData(
                    new { AgenciaId = 1, Value = "Palacio de Correos – Montevideo" },
                    new { AgenciaId = 2, Value = "Sucursal Tres Cruces – Montevideo" },
                    new { AgenciaId = 3, Value = "Sucursal Lagomar – Canelones" },
                    new { AgenciaId = 4, Value = "Sucursal Barros Blancos – Canelones" },
                    new { AgenciaId = 5, Value = "Sucursal San Luis – Canelones" },
                    new { AgenciaId = 6, Value = "Sucursal Salinas – Canelones" },
                    new { AgenciaId = 7, Value = "Sucursal San Bautista – Canelones" },
                    new { AgenciaId = 8, Value = "Sucursal Toledo – Canelones" }
                );

            // 3) DireccionPostal
            modelBuilder.Entity<Agencia>()
                .OwnsOne(a => a.DireccionPostal)
                .HasData(
                    new { AgenciaId = 1, Value = "Buenos Aires 451, Ciudad Vieja" },
                    new { AgenciaId = 2, Value = "Bulevar Artigas 1825, Terminal Tres Cruces" },
                    new { AgenciaId = 3, Value = "Av. Ing. Luis Giannattasio Km 21.500" },
                    new { AgenciaId = 4, Value = "Ruta 8 Km 23.800" },
                    new { AgenciaId = 5, Value = "Ruta Interbalnearia Km 62.800" },
                    new { AgenciaId = 6, Value = "Av. Julieta s/n, Salinas" },
                    new { AgenciaId = 7, Value = "Margarita Martínez de Fariña s/n, San Bautista" },
                    new { AgenciaId = 8, Value = "Ruta 85 s/n, Toledo" }
                );

            // 4) Ubicacion (VO con Latitud y Longitud)
            modelBuilder.Entity<Agencia>()
                .OwnsOne(a => a.Ubicacion)
                .HasData(
                    new { AgenciaId = 1, Latitud = -34.9052m, Longitud = -56.2036m },
                    new { AgenciaId = 2, Latitud = -34.8945m, Longitud = -56.1748m },
                    new { AgenciaId = 3, Latitud = -34.8220m, Longitud = -55.9960m },
                    new { AgenciaId = 4, Latitud = -34.7320m, Longitud = -56.0000m },
                    new { AgenciaId = 5, Latitud = -34.7500m, Longitud = -55.6000m },
                    new { AgenciaId = 6, Latitud = -34.7700m, Longitud = -55.8300m },
                    new { AgenciaId = 7, Latitud = -34.4500m, Longitud = -55.9500m },
                    new { AgenciaId = 8, Latitud = -34.7000m, Longitud = -56.0500m }
                );

        }
    }
}
