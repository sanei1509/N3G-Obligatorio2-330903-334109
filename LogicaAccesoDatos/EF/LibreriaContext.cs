
using LogicaAccesoDatos.EF.Config;
using LogicaAccesoDatos.EF.Config.Envios;
using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Enums;
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
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;
                                      Initial Catalog=Obligatorio;
                                      Integrated Security=True;");
            }
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
                new { Id = 1,Discriminator = "Admin" },
                new { Id = 2, Discriminator = "Admin" },
                new { Id = 3, Discriminator = "Cliente" },
                new { Id = 4, Discriminator = "Cliente" },
                new { Id = 5, Discriminator = "Admin" },
                new { Id = 6, Discriminator = "Admin" },
                new { Id = 7, Discriminator = "Admin" },
                new { Id = 8, Discriminator = "Cliente" },
                new { Id = 9, Discriminator = "Cliente" },
                new { Id = 10, Discriminator = "Funcionario" },
                new { Id = 11, Discriminator = "Funcionario" },
                new { Id = 12, Discriminator = "Funcionario" },
                new { Id = 13, Discriminator = "Cliente" }
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
                new { UsuarioId = 12, Value = "Diego" },
                new { UsuarioId = 13, Value = "Carente" }
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
                new { UsuarioId = 12, Value = "Navarro" },
                new { UsuarioId = 13, Value = "De Envios" }
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
                new { UsuarioId = 12, Value = "diego@gmail.com" },
                new { UsuarioId = 13, Value = "carente@gmail.com" }
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
                new { UsuarioId = 6, Value = "admin123" },
                new { UsuarioId = 7, Value = "admin123" },
                new { UsuarioId = 8, Value = "cliente123" },
                new { UsuarioId = 9, Value = "cliente123" },
                new { UsuarioId = 10, Value = "funcionario123" },
                new { UsuarioId = 11, Value = "funcionario123" },
                new { UsuarioId = 12, Value = "funcionario123" },
                new { UsuarioId = 13, Value = "cliente123" }
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
                new { UsuarioId = 12, Value = "099999000" },
                new { UsuarioId = 13, Value = "555-555-556" }
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
                new { Id = 8 },
                new { Id = 9 },
                new { Id = 10 }
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
                    new { AgenciaId = 8, Value = "Sucursal Toledo – Canelones" },
                    new { AgenciaId = 9, Value = "Sucursal Florida – Florida" },
                    new { AgenciaId = 10, Value = "Sucursal San Antonio – Canelones" }
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
                    new { AgenciaId = 8, Value = "Ruta 85 s/n, Toledo" },
                    new { AgenciaId = 9, Value = "Av. Millan s/n, Salinas" },
                    new { AgenciaId = 10, Value = "Margarita Martínez de Fariña s/n, San Bautista" }
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
                    new { AgenciaId = 8, Latitud = -34.7000m, Longitud = -56.0500m },
                    new { AgenciaId = 9, Latitud = -34.7050m, Longitud = -56.0550m },
                    new { AgenciaId = 10, Latitud = -33.7000m, Longitud = -57.0500m }

                );


            // 5 Envios "Comun"
            modelBuilder.Entity<Comun>().HasData(
                new
                {
                    Id = 1,
                    Discriminator = "Comun",
                    ClienteId = 4,
                    EmpleadoId = 1,
                    Estado = EstadoEnvio.EN_PROCESO,
                    FechaCreacion = new DateTime(2025, 5, 1, 10, 0, 0),
                    FechaFinalizacion = (DateTime?)null,
                    LugarRetiroId = 1
                },
                new
                {
                    Id = 2,
                    Discriminator = "Comun",
                    ClienteId = 4,
                    EmpleadoId = 2,
                    Estado = EstadoEnvio.EN_PROCESO,
                    FechaCreacion = new DateTime(2025, 5, 2, 14, 30, 0),
                    FechaFinalizacion = (DateTime?)null,
                    LugarRetiroId = 2
                },
                new
                {
                    Id = 3,
                    Discriminator = "Comun",
                    ClienteId = 3,
                    EmpleadoId = 1,
                    Estado = EstadoEnvio.FINALIZADO,
                    FechaCreacion = new DateTime(2025, 5, 3, 9, 15, 0),
                    FechaFinalizacion = (DateTime?)null,
                    LugarRetiroId = 1
                },
                new
                {
                    Id = 4,
                    Discriminator = "Comun",
                    ClienteId = 4,
                    EmpleadoId = 2,
                    Estado = EstadoEnvio.EN_PROCESO,
                    FechaCreacion = new DateTime(2025, 5, 4, 11, 45, 0),
                    FechaFinalizacion = (DateTime?)null,
                    LugarRetiroId = 3
                },
                new
                {
                    Id = 9,
                    Discriminator = "Comun",
                    ClienteId = 9,
                    EmpleadoId = 1,
                    Estado = EstadoEnvio.EN_PROCESO,
                    FechaCreacion = new DateTime(2025, 5, 9, 13, 0, 0),
                    FechaFinalizacion = (DateTime?)null,
                    LugarRetiroId = 4
                }
            );

            // 5 Envios "Urgente"
            modelBuilder.Entity<Urgente>().HasData(
                new
                {
                    Id = 5,
                    Discriminator = "Urgente",
                    ClienteId = 8,
                    EmpleadoId = 1,
                    Estado = EstadoEnvio.EN_PROCESO,
                    FechaCreacion = new DateTime(2025, 5, 5, 16, 0, 0),
                    FechaFinalizacion = (DateTime?)null
                },
                new
                {
                    Id = 6,
                    Discriminator = "Urgente",
                    ClienteId = 3,
                    EmpleadoId = 2,
                    Estado = EstadoEnvio.EN_PROCESO,
                    FechaCreacion = new DateTime(2025, 5, 6, 17, 30, 0),
                    FechaFinalizacion = (DateTime?)null
                },
                new
                {
                    Id = 7,
                    Discriminator = "Urgente",
                    ClienteId = 8,
                    EmpleadoId = 1,
                    Estado = EstadoEnvio.EN_PROCESO,
                    FechaCreacion = new DateTime(2025, 5, 7, 18, 45, 0),
                    FechaFinalizacion = (DateTime?)null
                },
                new
                {
                    Id = 8,
                    Discriminator = "Urgente",
                    ClienteId = 9,
                    EmpleadoId = 2,
                    Estado = EstadoEnvio.EN_PROCESO,
                    FechaCreacion = new DateTime(2025, 5, 8, 12, 15, 0),
                    FechaFinalizacion = (DateTime?)null
                },
                new
                {
                    Id = 10,
                    Discriminator = "Urgente",
                    ClienteId = 4,
                    EmpleadoId = 1,
                    Estado = EstadoEnvio.EN_PROCESO,
                    FechaCreacion = new DateTime(2025, 5, 10, 14, 0, 0),
                    FechaFinalizacion = (DateTime?)null
                }
            );

            //
            // 6) Seed de los Value Objects
            //
            // Peso
            modelBuilder.Entity<Envio>().OwnsOne(e => e.Peso).HasData(
                new { EnvioId = 1, Value = 1.5m },
                new { EnvioId = 2, Value = 2.2m },
                new { EnvioId = 3, Value = 5.0m },
                new { EnvioId = 4, Value = 0.8m },
                new { EnvioId = 5, Value = 3.3m },
                new { EnvioId = 6, Value = 10.0m },
                new { EnvioId = 7, Value = 7.7m },
                new { EnvioId = 8, Value = 4.4m },
                new { EnvioId = 9, Value = 2.0m },
                new { EnvioId = 10, Value = 6.6m }
            );

            // NroTracking
            modelBuilder.Entity<Envio>().OwnsOne(e => e.NroTracking).HasData(
                new { EnvioId = 1, Value = "TRK0000001" },
                new { EnvioId = 2, Value = "TRK0000002" },
                new { EnvioId = 3, Value = "TRK0000003" },
                new { EnvioId = 4, Value = "TRK0000004" },
                new { EnvioId = 5, Value = "TRK0000005" },
                new { EnvioId = 6, Value = "TRK0000006" },
                new { EnvioId = 7, Value = "TRK0000007" },
                new { EnvioId = 8, Value = "TRK0000008" },
                new { EnvioId = 9, Value = "TRK0000009" },
                new { EnvioId = 10, Value = "TRK0000010" }
            );

            // En los Urgente, además direcciones y entregado (false al principio):
            modelBuilder.Entity<Urgente>().OwnsOne(u => u.DireccionPostal).HasData(
                new { UrgenteId = 5, Value = "Calle 1 #100" },
                new { UrgenteId = 6, Value = "Av. 2 #200" },
                new { UrgenteId = 7, Value = "Calle 3 #300" },
                new { UrgenteId = 8, Value = "Av. 4 #400" },
                new { UrgenteId = 10, Value = "Calle 5 #500" }
            );
            modelBuilder.Entity<Urgente>().OwnsOne(u => u.Entregado).HasData(
                new { UrgenteId = 5, Value = false },
                new { UrgenteId = 6, Value = false },
                new { UrgenteId = 7, Value = false },
                new { UrgenteId = 8, Value = false },
                new { UrgenteId = 10, Value = false }
            );



            // 7) Seed de la entidad EtapaSeguimiento (solo columnas propias)
            modelBuilder.Entity<EtapaSeguimiento>().HasData(
                new { Id = 1, IdEnvio = 1, IdEmpleado = 1 },
                new { Id = 2, IdEnvio = 1, IdEmpleado = 2 },
                new { Id = 3, IdEnvio = 2, IdEmpleado = 1 },
                new { Id = 4, IdEnvio = 2, IdEmpleado = 2 },
                new { Id = 5, IdEnvio = 3, IdEmpleado = 1 },
                new { Id = 6, IdEnvio = 4, IdEmpleado = 2 },
                new { Id = 7, IdEnvio = 5, IdEmpleado = 1 },
                new { Id = 8, IdEnvio = 6, IdEmpleado = 2 },
                new { Id = 9, IdEnvio = 7, IdEmpleado = 1 },
                new { Id = 10, IdEnvio = 8, IdEmpleado = 2 }
            );

            // 8) Seed de cada VO con OwnsOne:

            // NroTracking
            modelBuilder.Entity<EtapaSeguimiento>()
                .OwnsOne(es => es.NroTracking)
                .HasData(
                    new { EtapaSeguimientoId = 1, Value = "TRK0000001" },
                    new { EtapaSeguimientoId = 2, Value = "TRK0000001" },
                    new { EtapaSeguimientoId = 3, Value = "TRK0000002" },
                    new { EtapaSeguimientoId = 4, Value = "TRK0000002" },
                    new { EtapaSeguimientoId = 5, Value = "TRK0000003" },
                    new { EtapaSeguimientoId = 6, Value = "TRK0000004" },
                    new { EtapaSeguimientoId = 7, Value = "TRK0000005" },
                    new { EtapaSeguimientoId = 8, Value = "TRK0000006" },
                    new { EtapaSeguimientoId = 9, Value = "TRK0000007" },
                    new { EtapaSeguimientoId = 10, Value = "TRK0000008" }
                );

            // Comentario
            modelBuilder.Entity<EtapaSeguimiento>()
                .OwnsOne(es => es.Comentario)
                .HasData(
                    new { EtapaSeguimientoId = 1, Value = "Recibido en almacén" },
                    new { EtapaSeguimientoId = 2, Value = "En tránsito" },
                    new { EtapaSeguimientoId = 3, Value = "Despachado" },
                    new { EtapaSeguimientoId = 4, Value = "En camino" },
                    new { EtapaSeguimientoId = 5, Value = "Llegó a destino" },
                    new { EtapaSeguimientoId = 6, Value = "Intento de entrega" },
                    new { EtapaSeguimientoId = 7, Value = "Entregado" },
                    new { EtapaSeguimientoId = 8, Value = "Pendiente retiro" },
                    new { EtapaSeguimientoId = 9, Value = "Reprogramado" },
                    new { EtapaSeguimientoId = 10, Value = "Cancelado por cliente" }
                );

            // Fecha
            modelBuilder.Entity<EtapaSeguimiento>()
                .OwnsOne(es => es.Fecha)
                .HasData(
                    new { EtapaSeguimientoId = 1, Value = new DateTime(2025, 5, 1, 11, 0, 0) },
                    new { EtapaSeguimientoId = 2, Value = new DateTime(2025, 5, 1, 13, 30, 0) },
                    new { EtapaSeguimientoId = 3, Value = new DateTime(2025, 5, 2, 10, 15, 0) },
                    new { EtapaSeguimientoId = 4, Value = new DateTime(2025, 5, 2, 15, 45, 0) },
                    new { EtapaSeguimientoId = 5, Value = new DateTime(2025, 5, 3, 9, 5, 0) },
                    new { EtapaSeguimientoId = 6, Value = new DateTime(2025, 5, 4, 17, 20, 0) },
                    new { EtapaSeguimientoId = 7, Value = new DateTime(2025, 5, 5, 18, 55, 0) },
                    new { EtapaSeguimientoId = 8, Value = new DateTime(2025, 5, 6, 12, 5, 0) },
                    new { EtapaSeguimientoId = 9, Value = new DateTime(2025, 5, 7, 14, 40, 0) },
                    new { EtapaSeguimientoId = 10, Value = new DateTime(2025, 5, 8, 16, 25, 0) }
                );


        }

        /// Re-hashea en base de datos todas las contraseñas
        /// que estén aún en claro (semillas), usando Usuario.SetPassword.
        /// </summary>
        public void MigrateSeedPasswords()
        {
            // Traemos todos los usuarios (incluye el VO Clave)
            var lista = Usuarios.ToList();
            var dirty = false;

            foreach (var u in lista)
            {
                // Detectamos si Clave.Value parece estar en texto  
                // (por ejemplo no empieza por el prefijo PBKDF2 "AQAAAA")
                if (u.Clave.Value is not null &&
                    !u.Clave.Value.StartsWith("AQAAAA"))
                {
                    // Re-hashea y marca como cambio
                    u.SetPassword(u.Clave.Value);
                    dirty = true;
                }
            }

            if (dirty)
                SaveChanges();
        }
    }
}
