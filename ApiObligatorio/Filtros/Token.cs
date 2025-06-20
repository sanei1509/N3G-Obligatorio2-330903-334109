//using Microsoft.AspNetCore.Mvc.Filters;

//namespace ApiObligatorio.Filtros
//{
//    public class Token : Attribute, IAuthorizationFilter
//    {
//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            var headers = context.HttpContext.Request.Headers;
//            var valorDolar = headers["ValorDolar"].ToString();
//            var lenguaje = headers["Lenguaje"].ToString();
//            var token = headers["Token"].ToString();

//            if (headers.ContainsKey("Authorization"))
//            {
//                var token1 = headers["Authorization"].ToString();
//                Console.WriteLine("Token recibido: " + token1);
//            }


//        }
//    }
//}
