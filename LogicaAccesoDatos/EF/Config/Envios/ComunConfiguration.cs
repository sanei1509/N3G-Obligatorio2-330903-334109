using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LogicaNegocio.Entidades.Envios;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.EF.Config.Envios
{
    public class ComunConfiguration : IEntityTypeConfiguration<Comun>
    {
        public void Configure(EntityTypeBuilder<Comun> builder)
        {
            // Configura la relación a Agencia (Lugar de Retiro)
            builder.HasOne(c => c.LugarRetiro)
                   .WithMany()                          // Sin navegación inversa en Agencia
                   .HasForeignKey("LugarRetiroId")    // FK sombra en Envios
                   .IsRequired()                        // No puede ser null
                   .OnDelete(DeleteBehavior.Restrict); // Evita cascadas múltiples

            // Shadow property para LugarRetiroId
            builder.Property<int>("LugarRetiroId")
                   .HasColumnName("LugarRetiroId");
        }

    }
}
