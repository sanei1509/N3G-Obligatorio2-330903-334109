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
            Usuario usuario = _repo.GetByEmail(datosUsuario.Correo);

            // Validar inputs
            if (string.IsNullOrWhiteSpace(datosUsuario.Correo)
             || string.IsNullOrWhiteSpace(datosUsuario.Clave))
            {
                return new(false, "Debe ingresar correo y contraseña");
            }

            // Existencia usuario
            if (usuario == null)
            {
                return new LoginRespuestaDto(false, "No existe cuenta con ese correo");
            }

            // Verificacion clave
            if (usuario.Clave.Value != datosUsuario.Clave)
            {
                return new LoginRespuestaDto(false, "Contraseña incorrecta");
            }

            return new LoginRespuestaDto(true, "Has ingresado con éxito!");
        }
    }
}   
