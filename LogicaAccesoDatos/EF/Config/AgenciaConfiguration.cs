using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LogicaNegocio.Entidades;

namespace LogicaAccesoDatos.EF.Config
{
    public class AgenciaConfiguration : IEntityTypeConfiguration<Agencia>
    {
        public void Configure(EntityTypeBuilder<Agencia> builder)
        {
            builder.OwnsOne(a => a.Nombre, nombre =>
            {
                nombre.Property(n => n.Value).HasColumnName("Nombre");
            });

            builder.OwnsOne(a => a.Ubicacion, ubicacion =>
            {
                ubicacion.Property(u => u.Latitud)
                         .HasColumnName("Latitud")
                         .HasPrecision(9, 6);      // hasta 3 dígitos antes del punto y 6 decimales


                ubicacion.Property(u => u.Longitud)
                         .HasColumnName("Longitud")
                         .HasPrecision(9, 6);
            });

            builder.OwnsOne(a => a.DireccionPostal, dir =>
            {
                dir.Property(d => d.Value)
                   .HasColumnName("DireccionPostal");
            });

        }
    }
}
