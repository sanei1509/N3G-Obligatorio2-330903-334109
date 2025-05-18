using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones.UsuarioExceptions;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso.Usuarios
{
    public class CrearUsuario: IAdd<CrearUsuarioDto>
    {
        private IRepositorioUsuario _repo;
        private IRepositorioAuditoria _auditoria;

        public CrearUsuario(IRepositorioUsuario repo, IRepositorioAuditoria auditoria)
        {
            _repo = repo;
            _auditoria = auditoria;
        }

        public void Execute (CrearUsuarioDto usuarioDto)
        {
            // Primero, verificamos si ya existe un usuario con ese correo.
            var usuarioExistente = _repo.GetByEmail(usuarioDto.Correo);

            if (usuarioExistente != null)
            {
                throw new YaExisteUsuarioException("Ya existe un usuario con ese correo.");
            }


            if (usuarioDto.Clave.Length < 6)
                throw new ClaveException("La clave debe tener al menos 6 caracteres");

            bool tieneLetra = false;
            bool tieneDigito = false;
            bool tieneEspecial = false;
            char[] especiales = { '+', '.', '#' };

            foreach (var c in usuarioDto.Clave)
            {
                if (!tieneLetra && char.IsLetter(c))
                    tieneLetra = true;
                else if (!tieneDigito && char.IsDigit(c))
                    tieneDigito = true;
                else if (!tieneEspecial && Array.IndexOf(especiales, c) >= 0)
                    tieneEspecial = true;

                // Si ya encontramos todo, salimos
                if (tieneLetra && tieneDigito && tieneEspecial)
                    break;
            }

            if (!tieneLetra)
                throw new ClaveException("La clave debe contener al menos una letra");

            if (!tieneDigito)
                throw new ClaveException("La clave debe contener al menos un número");

            if (!tieneEspecial)
                throw new ClaveException("La clave debe contener al menos uno de estos caracteres: + . #");
            var nuevo = UsuarioMapper.FromDto(usuarioDto);
            _repo.Add(nuevo);

            var usuarioResponsable = _repo.GetByEmail(usuarioDto.CorreoResponsable);
            //Registrar auditoria
            var auditoria = new Auditoria
            (
                0,
                usuarioResponsable.Id,
                nuevo.Id,
                "Alta Usuario",
                DateTime.Now
            );
            _auditoria.Add(auditoria);
        }
    }
}
