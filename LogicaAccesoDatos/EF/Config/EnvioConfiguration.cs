using LogicaNegocio.Entidades.Usuarios.Usuario;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LogicaNegocio.Entidades.Envios;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF.Config
{
    public class EnvioConfiguration : IEntityTypeConfiguration<Envio>
    {
        public void Configure(EntityTypeBuilder<Envio> builder)
        {
            // 3) Relación Cliente → Envios con shadow FK "ClienteId"
            builder.HasOne(e => e.Cliente)
                   .WithMany()                           // Envios en Usuario no declarado
                   .HasForeignKey("ClienteId")           // crea columna ClienteId en Envios
                   .IsRequired()                         // NOT NULL
                   .OnDelete(DeleteBehavior.Cascade);    // al borrar Cliente, borras Envios

            // 4) Relación Empleado → Envios con shadow FK "EmpleadoId"
            builder.HasOne(e => e.Empleado)
                   .WithMany()                           // Envios en Usuario no declarado
                   .HasForeignKey("EmpleadoId")          // crea columna EmpleadoId en Envios
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);   // evita cascada múltiple

            // 5) Opcional: configurar las shadow properties (nombre, tipo)
            builder.Property<int>("ClienteId")
                   .HasColumnName("ClienteId");
            builder.Property<int>("EmpleadoId")
                   .HasColumnName("EmpleadoId");

            builder.OwnsOne(e => e.Peso, peso =>
            {
                peso.Property(p => p.Value)
                    .HasColumnName("Peso")
                    .HasPrecision(10, 3);     // ejemplo: hasta 7 enteros y 3 decimales

            });

            builder.OwnsOne(nt => nt.NroTracking, nroTracking =>
            {
                nroTracking.Property(nt => nt.Value).HasColumnName("NroTracking");
            });

            builder
            .ToTable("Envios")
            .HasDiscriminator<string>("TipoEnvio")
            .HasValue<Comun>("Comun")
            .HasValue<Urgente>("Urgente");
        }
    }
}
