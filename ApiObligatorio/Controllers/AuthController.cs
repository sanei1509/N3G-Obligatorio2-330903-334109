using ApiObligatorio.Services;
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAccesoDatos.Exceptions;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiObligatorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IGetByEmail<UsuarioDto> _getByEmail;

        public AuthController(IJwtGenerator jwtGenerator, IGetByEmail<UsuarioDto> getByEmail)
        {
            _jwtGenerator = jwtGenerator;
            _getByEmail = getByEmail;
        }

        [HttpPost("Generate")]
        public IActionResult Generate([FromBody] LoginEntradaDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Correo))
                return BadRequest(new Error(400, "Correo y clave son obligatorios"));

            UsuarioDto usuario;
            try
            {
                usuario = _getByEmail.Execute(dto.Correo);
            }
            catch (NotFoundException)
            {
                return Unauthorized(new Error(401, "Credenciales inválidas"));
            }

            if (dto.Clave != usuario.Clave)
                return Unauthorized(new Error(401, "Credenciales inválidas"));

            var token = _jwtGenerator.GenerateToken(usuario);
            return Ok(new { token });

        }


    }
}
