
using CasoUsoCompartida.DTOs.Envios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.Mapper;
using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Usuarios.Usuario;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Vo.Envio;
using System;

namespace LogicaAplicacion.CasosUso.Envios
{
    public class CrearEnvio : IAdd<CrearEnvioDto>
    {
        private IRepositorioEnvio _repo;
        private IRepositorioUsuario _usuario;
        private IRepositorioAgencia _agencia;

        public CrearEnvio(IRepositorioEnvio repo, IRepositorioUsuario usuario, IRepositorioAgencia agencia)
        {
            _repo = repo;
            _usuario = usuario;
            _agencia = agencia;
        }

        public void Execute(CrearEnvioDto envioDto)
        {
            // 1 Empleado actual
            Empleado empleado = (Empleado)_usuario.GetByEmail(envioDto.CorreoEmpleado);
            
            // 2 Cliente
            var cliente = _usuario.GetByEmail(envioDto.CorreoCliente) as Cliente;
            if (cliente == null)
                throw new Exception(
                    $"No existe ningún cliente con correo '{envioDto.CorreoCliente}'"
                );

            // 3) Agencia solo si es envío Comun
            Agencia agencia = null;
            if (envioDto.TipoEnvio == "Comun")
            {
                if (envioDto.LugarRetiroId == 0)
                    throw new Exception("Debes seleccionar una agencia para envíos Comunes");
                agencia = _agencia.GetById(envioDto.LugarRetiroId);
            }

            // 4 Generar Nro Tracking único con GUID
            var trackingString = Guid.NewGuid().ToString("N");     // 32 dígitos hex sin guiones
            var nroVo = new NroTracking(trackingString);

            // 5 Estado inicial del envio
            var estado = EstadoEnvio.EN_PROCESO;

            _repo.Add(EnvioMapper.FromDto(envioDto, empleado, estado , cliente, nroVo, agencia));
        }
    }
}
