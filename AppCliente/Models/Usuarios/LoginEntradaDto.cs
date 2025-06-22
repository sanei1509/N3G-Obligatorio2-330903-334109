using System.ComponentModel.DataAnnotations;

namespace AppCliente.Models.Usuarios
{
    public record LoginEntradaDto
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Clave { get; set; }

        public LoginEntradaDto(string correo, string clave)
        {
            Correo = correo;
            Clave = clave;
        }
        public LoginEntradaDto()
        {
        }
    }

}
