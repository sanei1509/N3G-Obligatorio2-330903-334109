using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace Libreria.LogicaAplicacion.CasoUso.Usuarios
{
    public class EditarUsuario : IUpdate<CrearUsuarioDto>
    {
        private IRepositorioUsuario _repo;
        private IRepositorioAuditoria _auditorias;

        public EditarUsuario(IRepositorioUsuario repo, IRepositorioAuditoria auditorias)
        {
            _repo = repo;
            _auditorias = auditorias;
        }

        public void Execute(int id, CrearUsuarioDto obj)
        {
            var usuarioResponsable = _repo.GetByEmail(obj.CorreoResponsable);
            var usuarioModificado = UsuarioMapper.FromDto(obj);
            _repo.Update(id, usuarioModificado);
            //Registrar auditoria
            var auditoria = new Auditoria
                                (
                                    0,
                                    usuarioResponsable.Id,
                                    usuarioModificado.Id,
                                    "Edicion Usuario",
                                     DateTime.Now
                                );
            _auditorias.Add(auditoria);
        }
    }
}