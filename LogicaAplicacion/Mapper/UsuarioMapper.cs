using CasoUsoCompartida.DTOs.Usuarios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Vo;
using Microsoft.AspNetCore.Identity;

namespace LogicaAplicacion.Mapper
{
    internal class UsuarioMapper
    {
        // De DTO a Entidad
        public static Usuario FromDto(CrearUsuarioDto dto)
        {

            //var hasher = new PasswordHasher<object>();
            //string hashedPassword = hasher.HashPassword(null, dto.Clave);
            //var result = hasher.VerifyHashedPassword(null, hashedPassword, "miContraseniaSegura123");

            // Creás los Value Objects a partir de los campos del DTO
            var nombreVo = new Nombre(dto.Nombre);
            var apellidoVo = new Apellido(dto.Apellido);
            var correoVo = new Correo(dto.Correo);
            var claveVo = new Clave(dto.Clave);
            var telefonoVo = new Telefono(dto.Telefono);

            // Retornás una instancia de Usuario con esos VO
            return new Usuario(
                dto.Id,
                nombreVo,
                apellidoVo,
                correoVo,
                claveVo,
                telefonoVo
            );
        }

        // De Entidad a DTO
        public static UsuarioDto ToDto(Usuario usuario)
        {
            return new UsuarioDto(
                usuario.Id,
                usuario.Nombre.Value,
                usuario.Apellido.Value,
                usuario.Correo.Value,
                usuario.Telefono.Value
            );
        }

        //public static IEnumerable<UsuarioListadoDto> ToListaDto(IEnumerable<Usuario> usuarios)
        //{
        //    List<UsuarioListadoDto> usuariosListadoDto = new List<UsuarioListadoDto>();
        //    foreach (var item in usuarios)
        //    {
        //        usuariosListadoDto.Add(new UsuarioListadoDto(item.Id,
        //                                                     item.Nombre.Value,
        //                                                     item.MiEmail.Value,
        //                                                     item.Rol));
        //    }
        //    return usuariosListadoDto;
        //}

    }
}
