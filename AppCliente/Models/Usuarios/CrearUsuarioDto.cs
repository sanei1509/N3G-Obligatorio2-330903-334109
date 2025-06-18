﻿namespace AppCliente.Models.Usuarios
{
    public record CrearUsuarioDto(
        int Id, 
        string Nombre, 
        string Apellido, 
        string Correo, 
        string Clave, 
        string Telefono,
        string CorreoResponsable
        )
    {
    }
}
