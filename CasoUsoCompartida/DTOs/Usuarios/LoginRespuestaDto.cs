namespace CasoUsoCompartida.DTOs.Usuarios
{
    public record class LoginRespuestaDto(
        bool Autenticado,
        string Mensaje
        )
    {
    }
}

