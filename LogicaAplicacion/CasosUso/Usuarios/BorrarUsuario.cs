
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace Libreria.LogicaAplicacion.CasoUso.Usuarios
{
    public class BorrarUsuario: IRemove
    {
        private IRepositorioUsuario _repo;
        private IRepositorioAuditoria _auditorias;

        public BorrarUsuario(IRepositorioUsuario repo, IRepositorioAuditoria auditorias)
        {
            _repo = repo;
            _auditorias = auditorias;
        }

        public void Execute(CrearUsuarioDto dto)
        {
            var usuarioResponsable = _repo.GetByEmail(dto.CorreoResponsable);

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