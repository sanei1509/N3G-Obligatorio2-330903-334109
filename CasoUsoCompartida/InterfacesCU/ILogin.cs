using CasoUsoCompartida.DTOs.Usuarios;

namespace CasoUsoCompartida.InterfacesCU
{
    public interface ILogin<LoginRespuestaDto>
    {
        public LoginRespuestaDto Execute(LoginEntradaDto datosFormulario);
    }
}
