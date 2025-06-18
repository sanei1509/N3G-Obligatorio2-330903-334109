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
        private readonly IGetById<UsuarioDto> _getById;
        private readonly IGetByEmail<UsuarioDto> _getByEmail;

        public AuthController(IJwtGenerator jwtGenerator, IGetById<UsuarioDto> getById, IGetByEmail<UsuarioDto> getByEmail)
        {
            _getById = getById;
            _jwtGenerator = jwtGenerator;
            _getByEmail = getByEmail;
        }

        [HttpPost("Generate")]
        public IActionResult Generate([FromBody] LoginEntradaDto usuarioLogin)
        {
            try
            {
                if (usuarioLogin == null)
                    throw new Exception("Datos incompletos");
                    //throw new BadRequestException("Datos incompletos");

                    // Debe tener un caso de uso pas el mail
                    var usuario = _getByEmail.Execute(usuarioLogin.Correo);

                //if (usuario == null)
                //    return Unauthorized("Credenciales inválidas");


                var token = _jwtGenerator.GenerateToken(usuario);
                return Ok(new { token });
            }
            catch (NotFoundException e)
            {
                return StatusCode(e.StatusCode(), e.Error());
            }
            catch (BadRequestException e)
            {
                return StatusCode(e.StatusCode(), e.Error());
            }
            catch (Exception)
            {
                Error error = new Error(500, "Hupp. Proba nuevamente");
                return StatusCode(500, error);
            }

        }


    }
}
