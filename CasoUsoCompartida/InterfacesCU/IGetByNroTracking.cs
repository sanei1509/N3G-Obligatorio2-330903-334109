
using LogicaNegocio.Vo.Envio;

namespace CasoUsoCompartida.InterfacesCU
{
    public interface IGetByNroTracking<T>
    {
        T Execute(string nroTracking);
    }
}
