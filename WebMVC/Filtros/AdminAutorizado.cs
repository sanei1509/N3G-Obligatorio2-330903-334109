using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Filtros
{
    public class AdminAutorizado : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (context.HttpContext.Session.GetString("Rol") != "Admin")
            {
                context.Result = new RedirectResult("/Usuario/Gestion");
            }
        }
    }
}
