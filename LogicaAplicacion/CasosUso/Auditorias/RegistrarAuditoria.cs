using CasoUsoCompartida.InterfacesCU;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso.Auditorias
{
    public class RegistrarAuditoria: IAdd<Auditoria>
    {
        private IRepositorioAuditoria _repo;

        public RegistrarAuditoria(IRepositorioAuditoria repo)
        {
            _repo = repo;
        }

        public void Execute(Auditoria obj) 
        {
            _repo.Add(obj);
        }
    }
}
