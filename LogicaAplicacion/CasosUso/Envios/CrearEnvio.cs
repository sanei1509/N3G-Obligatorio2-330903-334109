
using CasoUsoCompartida.DTOs.Envios;
using CasoUsoCompartida.DTOS.Envios;
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
    public class CrearEnvio : IAdd<CrearEnvioDto>
    {
        private IRepositorioEnvio _repo;
        private IRepositorioUsuario _usuario;
        private IRepositorioAgencia _agencia;
        private IRepositorioEtapaSeguimiento _repoEtapa;

        public CrearEnvio(IRepositorioEnvio repo, IRepositorioUsuario usuario, IRepositorioAgencia agencia, IRepositorioEtapaSeguimiento repoEtapa)
        {
            _repo = repo;
            _usuario = usuario;
            _agencia = agencia;
            _repoEtapa = repoEtapa;
        }

        public void Execute(CrearEnvioDto envioDto)
        {
            // 1 Empleado actual
            Empleado empleado = (Empleado)_usuario.GetByEmail(envioDto.CorreoEmpleado);
            
            // 2 Cliente
            var cliente = _usuario.GetByEmail(envioDto.CorreoCliente) as Cliente;
            if (cliente == null)
                throw new CorreoException(
                    $"No existe ningún cliente con correo '{envioDto.CorreoCliente}'"
                );

            // 3) Agencia solo si es envío Comun
            Agencia agencia = null;
            if (envioDto.TipoEnvio == "Comun")
            {
                if (envioDto.LugarRetiroId == 0)
                    throw new LugarRetiroException("Debes seleccionar una agencia para envíos Comunes");
                agencia = _agencia.GetById(envioDto.LugarRetiroId);
            }

            // 4 Generar Nro Tracking único con GUID
            var trackingString = Guid.NewGuid().ToString("N");     // 32 dígitos hex sin guiones
            var nroVo = new NroTracking(trackingString);

            // 5 Estado inicial del envio
            var estado = EstadoEnvio.EN_PROCESO;
            var envioNuevo = EnvioMapper.FromDto(envioDto, empleado, estado, cliente, nroVo, agencia);
            _repo.Add(envioNuevo);

            var comentario = new Comentario("Se procesó en agencia");
            var fecha = new Fecha(DateTime.Now); 

            var etapa = new EtapaSeguimiento(
                id: 0,
                idEnvio: envioNuevo.Id,
                nroTracking: envioNuevo.NroTracking,
                idEmpleado: empleado.Id,
                envio: envioNuevo,
                comentario: comentario,
                empleado: empleado,
                fecha: fecha
            );

            _repoEtapa.Add(etapa);
        }
    }
}
