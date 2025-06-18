namespace AppCliente.Models.Usuarios
{
    public record class LoginRespuestaDto(
        bool Autenticado,
        string Mensaje
        )
    {
    }
}

