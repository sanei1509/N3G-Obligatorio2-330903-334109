
using CasoUsoCompartida.DTOs.Envios;
using CasoUsoCompartida.DTOs.EtapaSeguimiento;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Enums;
using LogicaNegocio.Excepciones.EnvioExceptions;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Vo.Envio;
using LogicaNegocio.Vo.EtapaSeguimiento;
using System;

namespace LogicaAplicacion.CasosUso.Envios
{
    public class AgregarComentario : IAdd<CrearComentarioDto>
    {
        private IRepositorioEtapaSeguimiento _repoEtapa;
        private IRepositorioEnvio _repo;
        private IRepositorioUsuario _usuario;

        public AgregarComentario(IRepositorioEnvio repo, IRepositorioUsuario usuario, IRepositorioEtapaSeguimiento repoEtapa)
        {
            _repoEtapa = repoEtapa;
            _repo = repo;
            _usuario = usuario;
        }

        public void Execute(CrearComentarioDto comentarioDto)
        {
            // 1) Traer las entidades
            var envio = _repo.GetById(comentarioDto.IdEnvio);
            var empleado = (Empleado)_usuario.GetByEmail(comentarioDto.CorreoEmpleado);

            // 2) Mapear a dominio
            var etapa = EtapaSeguimientoMapper.FromDto(comentarioDto, envio, empleado);

            // 3) Persistir
            _repoEtapa.Add(etapa);
        }
    }

}
