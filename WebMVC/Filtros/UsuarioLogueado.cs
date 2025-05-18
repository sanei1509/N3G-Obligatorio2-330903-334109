using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Filtros
{
   public class UsuarioLogueado : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isLogueado = context.HttpContext.Session.GetString("Logueado");

            if (string.IsNullOrEmpty(isLogueado))
            {
                // No está logueado; redirigimos al login
                context.Result = new RedirectResult("/");
            }
        }
    }
}
