using LogicaNegocio.Entidades.Envios;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LogicaNegocio.Entidades;

namespace LogicaAccesoDatos.EF.Config
{
    public class EtapaSeguimientoConfiguration : IEntityTypeConfiguration<EtapaSeguimiento>
    {
        public void Configure(EntityTypeBuilder<EtapaSeguimiento> builder)
        {
            builder.HasKey(es => es.Id);

            // 1) FK a Envio → un sólo cascade path
            builder.HasOne(es => es.Envio)
                   .WithMany()
                   .HasForeignKey(es => es.IdEnvio)
                   .OnDelete(DeleteBehavior.Cascade);

            // 2) FK a Usuario (Empleado) → sin cascada
            builder.HasOne(es => es.Empleado)
                   .WithMany()   // o .WithMany(u => u.EtapasRealizadas) si quieres bidireccional
                   .HasForeignKey(es => es.IdEmpleado)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.OwnsOne(es => es.Comentario, comentario =>
            {
                comentario.Property(c => c.Value).HasColumnName("Comentario");
            });

            builder.OwnsOne(es => es.Fecha, fecha =>
            {
                fecha.Property(nt => nt.Value).HasColumnName("Fecha");
            });

            builder.OwnsOne(es => es.NroTracking, nro =>
            {
                nro.Property(n => n.Value)
                   .HasColumnName("NroTracking")
                   .IsRequired();
            });


            builder.ToTable("EtapasSeguimiento");
        }


    }
}
