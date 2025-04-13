using CasoUsoCompartida.InterfacesCU;
using CasoUsoCompartida.DTOs.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using Microsoft.AspNetCore.Identity;
using LogicaNegocio.Vo;
namespace LogicaAplicacion.CasosUso.Usuarios
{
    public class Login : ILogin<LoginRespuestaDto>
    {
        private IRepositorioUsuario _repo;

        public Login(IRepositorioUsuario repo)
        {
            _repo = repo;
        }

        public LoginRespuestaDto Execute(LoginEntradaDto datosUsuario) 
        {
            //var hasher = new PasswordHasher<object>();
            Usuario usuario = _repo.GetByEmail(datosUsuario.Correo);

            if (usuario == null)
            {
                return new LoginRespuestaDto(false, "El usuario no existe");
            }
            //Hashed password
            //var result = hasher.VerifyHashedPassword(null, usuario.Clave.Value, datosUsuario.Clave);

            if (usuario.Clave.Value != datosUsuario.Clave)
            {
                return new LoginRespuestaDto(false, "Contraseña incorrecta");
            }

            return new LoginRespuestaDto(true, "Has ingresado con éxito!");
        }
    }
}   
