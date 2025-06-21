using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AppCliente.Filtros
{
    public class UsuarioAutenticado : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isLogueado = context.HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(isLogueado))
            {
                // No está logueado; redirigimos al login
                context.Result = new RedirectToActionResult("Login", "Usuario", null);
            }
        }
    }
}
