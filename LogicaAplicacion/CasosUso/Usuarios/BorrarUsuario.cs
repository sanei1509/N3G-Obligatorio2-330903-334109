
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones.UsuarioExceptions;
using LogicaNegocio.InterfacesRepositorio;

namespace Libreria.LogicaAplicacion.CasoUso.Usuarios
{
    public class BorrarUsuario: IRemove
    {
        private IRepositorioUsuario _repo;
        private IRepositorioAuditoria _auditorias;
        private IRepositorioEnvio _repoEnvios;

        public BorrarUsuario(IRepositorioUsuario repo, IRepositorioAuditoria auditorias, IRepositorioEnvio repoEnvios)
        {
            _repo = repo;
            _auditorias = auditorias;
            _repoEnvios = repoEnvios;
        }

        public void Execute(CrearUsuarioDto dto)
        {
            var usuarioResponsable = _repo.GetByEmail(dto.CorreoResponsable);

            // Verificar si tiene envíos asociados
            bool tieneEnvios = _repoEnvios.TieneEnviosAsignados(dto.Id);
            if (tieneEnvios)
            {
                throw new EmpleadoConEnvioException("No se puede eliminar el empleado porque tiene envíos asignados.");
            }

            //eliminado logico
            var usuarioAEliminar = _repo.GetById(dto.Id);
            usuarioAEliminar.Eliminar();

            _repo.Update(dto.Id, usuarioAEliminar);
            var auditoria = new Auditoria
                    (
                        0,
                        usuarioResponsable.Id,
                        dto.Id,
                        "Baja Usuario",
                        DateTime.Now
                    );
            _auditorias.Add(auditoria);
        }

    }
}