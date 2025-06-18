using CasoUsoCompartida.DTOs.Usuarios;

namespace ApiObligatorio.Services
{
    public interface IJwtGenerator
    {
       public string GenerateToken(UsuarioDto usuario);
    }
}
