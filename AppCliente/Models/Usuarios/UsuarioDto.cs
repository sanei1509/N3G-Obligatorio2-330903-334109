namespace AppCliente.Models.Usuarios
{
    public record UsuarioDto(
        int Id, 
        string Nombre, 
        string Apellido,
        string Correo,
        string Telefono,
        string Discriminator
        )
    {
    }
}
