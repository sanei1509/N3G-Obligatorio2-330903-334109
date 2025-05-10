
using LogicaNegocio.Entidades.Envios;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF.Config.Envios
{
    public class UrgenteConfiguration : IEntityTypeConfiguration<Urgente>
    {
        public void Configure(EntityTypeBuilder<Urgente> builder)
        {
            builder.OwnsOne(u => u.DireccionPostal, dp =>
            {
                dp.Property(d => d.Value).HasColumnName("DireccionPostal");
            });
            builder.Navigation(u => u.DireccionPostal)
                   .IsRequired(false);      // <-- permite NULL y no materializa el VO

            builder.OwnsOne(u => u.Entregado, e =>
            {
                e.Property(v => v.Value).HasColumnName("Entregado");
            });
            builder.Navigation(u => u.Entregado)
                    .IsRequired(false);      // <-- igual para Entregado
        }
    }
}
