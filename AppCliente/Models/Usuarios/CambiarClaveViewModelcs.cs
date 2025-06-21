using System.ComponentModel.DataAnnotations;

namespace AppCliente.Models.Usuarios
{
    public class CambiarClaveViewModel
    {
        [Required(ErrorMessage = "Debes ingresar tu contraseña actual")]
        [DataType(DataType.Password)]
        public string ClaveActual { get; set; }

        [Required(ErrorMessage = "Debes ingresar la nueva contraseña")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La nueva contraseña debe tener al menos 6 caracteres")]
        public string NuevaClave { get; set; }

        [Required(ErrorMessage = "Debes confirmar la nueva contraseña")]
        [DataType(DataType.Password)]
        [Compare("NuevaClave", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarNuevaClave { get; set; }
    }
}
