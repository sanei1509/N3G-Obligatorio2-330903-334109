﻿namespace AppCliente.Models.Usuarios
{
    public record UsuarioListadoDto(
        int Id,
        string Nombre,
        string Apellido,
        string Correo,
        string Telefono,
        string Discriminator
        );
}
