using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace LogicaAccesoDatos.EF.Config
{
    public class AuditoriaConfiguration : IEntityTypeConfiguration<Auditoria>
    {
        public void Configure(EntityTypeBuilder<Auditoria> builder)
        {
            // Nombre de la tabla
            builder.ToTable("Auditorias");

            // Clave primaria
            builder.HasKey(a => a.Id);

            // IdResponsable (FK potencial a Usuario, si quieres)
            builder.Property(a => a.IdResponsable)
                   .HasColumnName("IdResponsable")
                   .IsRequired();

            // IdEmpleado (FK potencial a Usuario)
            builder.Property(a => a.IdEmpleado)
                   .HasColumnName("IdEmpleado")
                   .IsRequired();

            // Acción realizada
            builder.Property(a => a.Accion)
                   .HasColumnName("Accion")
                   .IsRequired()
                   .HasMaxLength(200);

            // Fecha de la auditoría
            builder.Property(a => a.Fecha)
                   .HasColumnName("Fecha")
                   .IsRequired();
        }
    }
}
