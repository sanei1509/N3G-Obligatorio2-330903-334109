using CasoUsoCompartida.DTOs.Usuarios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiObligatorio.Services
{
    public class JwtGenerator : IJwtGenerator
    {
        //INYECCION DE LA CONFIG
        private readonly IConfiguration _config;

        public JwtGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(UsuarioDto usuario)
        {
            var key = Encoding.UTF8.GetBytes(_config["JWT:Key"]);

            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Email, usuario.Correo),
                 new Claim(JwtRegisteredClaimNames.Name, usuario.Nombre),
                new Claim(JwtRegisteredClaimNames.NameId, usuario.Id.ToString() ),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60), // Sugerido: 15-60 min
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Se serializa a un string el bearer token
            return tokenHandler.WriteToken(token);
        }


    }
}
