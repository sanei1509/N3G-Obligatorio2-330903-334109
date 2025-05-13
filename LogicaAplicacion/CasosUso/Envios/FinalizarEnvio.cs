using CasoUsoCompartida.InterfacesCU;
using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso.Envios
{
    public class FinalizarEnvio
    {
        private IRepositorioEnvio _envio;

        public FinalizarEnvio(IRepositorioEnvio envio)
        {
            _envio = envio;
        }

        public void Execute(int envioId)
        {
            // 1 Buscar el envio
            var envio = _envio.GetById(envioId);

            if (envio == null)
            {
                throw new Exception("Envio no encontrado");
            }


            // 2 Ejecutar método Finalizar envio
            envio.FinalizarEnvio(envio);


            // 3 Actualizar Cambios hacia DB
            _envio.Update(envioId, envio);
        }

    }
}
