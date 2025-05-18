using LogicaNegocio.Entidades.Usuarios.Empleados;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LogicaAccesoDatos.EF.Config
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Mapea Nombre (VO) a la columna "Nombre"
            builder.OwnsOne(u => u.Nombre, nombre =>
            {
                nombre.Property(n => n.Value).HasColumnName("Nombre");
            });

            // Mapea Apellido (VO) a la columna "Apellido"
            builder.OwnsOne(u => u.Apellido, apellido =>
            {
                apellido.Property(a => a.Value).HasColumnName("Apellido");
            });

            // Mapea Correo (VO) a la columna "Correo"
            builder.OwnsOne(u => u.Correo, correo =>
            {
                correo.Property(c => c.Value).HasColumnName("Correo");
            });

            // Mapea Clave (VO) a la columna "Clave"
            builder.OwnsOne(u => u.Clave, clave =>
            {
                clave.Property(c => c.Value).HasColumnName("Clave");
            });

            // Mapea Telefono (VO) a la columna "Telefono"
            builder.OwnsOne(u => u.Telefono, telefono =>
            {
                telefono.Property(t => t.Value).HasColumnName("Telefono");
            });

            builder
                .ToTable("Usuarios")
                .HasDiscriminator<string>("Discriminator")  
                .HasValue<Cliente>("Cliente")
                .HasValue<Funcionario>("Funcionario")
                .HasValue<Administrador>("Admin");

            builder.Property<bool>("Eliminado")
               .HasColumnName("Eliminado")
               .HasDefaultValue(false);

            builder.HasQueryFilter(u => !u.Eliminado);
        }
    }
}
