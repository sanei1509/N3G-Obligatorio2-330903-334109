using ApiObligatorio.Services;
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAccesoDatos.Exceptions;
using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiObligatorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IGetByEmail<UsuarioDto> _getByEmail;
        private readonly IRepositorioUsuario _repo;

        public AuthController(IJwtGenerator jwtGenerator, IGetByEmail<UsuarioDto> getByEmail, IRepositorioUsuario repo)
        {
            _jwtGenerator = jwtGenerator;
            _getByEmail = getByEmail;
            _repo = repo;
        }

        [HttpPost("Generate")]
        public IActionResult Generate([FromBody] LoginEntradaDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Correo))
                return BadRequest(new Error(400, "Correo y clave son obligatorios"));

            Usuario usuario;
           
            try
            {
                usuario = _repo.GetByEmail(dto.Correo);
                
            }
            catch (NotFoundException)
            {
                return Unauthorized(new Error(401, "Usuario no autorizado"));
            }

            if (!usuario.CheckPassword(dto.Clave))
                return Unauthorized(new Error(400, "Credenciales inválidas"));

            UsuarioDto usuarioDto = _getByEmail.Execute(dto.Correo);
            var token = _jwtGenerator.GenerateToken(usuarioDto);

            return Ok(new
            {
                token,
                user = new
                {
                    usuarioDto.Id,
                    usuarioDto.Nombre,
                    usuarioDto.Correo,
                    usuarioDto.Discriminator
                }
            });

        }

        [HttpPut("CambiarClave")]
        [Authorize]
        public IActionResult CambiarClave([FromBody] CambiarClaveDto claveDto) 
        {
            if (claveDto == null || string.IsNullOrWhiteSpace(claveDto.ClaveActual) || string.IsNullOrWhiteSpace(claveDto.NuevaClave))
                return BadRequest(new Error(400, "Debes ingresar tu Clave Actual y Clave Nueva"));

            // 1) Obtener correo
            //var email = User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;   // no se usa para leer cosas del usuario
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized(new Error(401, "No se pudo leer tu correo del token"));


            // 2) Obtener el usuario
            var usuario = _repo.GetByEmail(email);
            
            // 3) Verificar Clave actual
            if (!usuario.CheckPassword(claveDto.ClaveActual))
                return BadRequest(new { message = "Contraseña actual incorrecta" });

            // 4) Actualizo el hash y confirmo en base de datos
            usuario.SetPassword(claveDto.NuevaClave);
            _repo.Update(usuario.Id, usuario);

            return Ok(new { message = "Clave actualizada exitosamente" });
        }


    }
}
