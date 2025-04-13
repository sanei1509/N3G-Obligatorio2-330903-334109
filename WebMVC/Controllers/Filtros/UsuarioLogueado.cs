using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebMVC.Controllers.Filtros
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
