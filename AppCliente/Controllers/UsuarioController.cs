using AppCliente.Models.Envios;
using AppCliente.Models.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;

namespace AppCliente.Controllers
{
    public class UsuarioController : Controller
    {
        public UsuarioController()
        {
        }

        // GET: /Usuario/Login?nroTracking=ABC123
        [HttpGet]
        public IActionResult Login(string nroTracking)
        {
            // Si vino nroTracking en querystring, hacemos la consulta
            if (!string.IsNullOrWhiteSpace(nroTracking))
            {
                try
                {
                    var client = new RestClient(new RestClientOptions("http://localhost:5064/api") { MaxTimeout = -1 });
                    var req = new RestRequest($"envios/{Uri.EscapeDataString(nroTracking)}", Method.Get);

                    // sólo envía el header si el usuario realmente está autenticado
                    if (User.Identity?.IsAuthenticated == true)
                    {
                        var token = HttpContext.Session.GetString("token");
                        if (!string.IsNullOrEmpty(token))
                            req.AddHeader("Authorization", $"Bearer {token}");
                    }

                    var resp = client.Execute(req);

                    if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.TrackError = "No existe ese número de tracking.";
                    }
                    else if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        ViewBag.TrackError = "Token inválido o expirado. Por favor inicia sesión.";
                    }
                    else if (!resp.IsSuccessful)
                    {
                        ViewBag.TrackError = "Error al consultar el envío. Intenta nuevamente.";
                    }
                    else
                    {
                        // Deserializa y guarda en ViewBag.Envio
                        var envio = JsonSerializer.Deserialize<EnvioListadoDto>(
                            resp.Content!,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        ViewBag.Envio = envio;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.TrackError = "Ocurrió un error: " + ex.Message;
                }
            }

            // Siempre devolvemos la vista con el modelo vacío (login)
            return View(new LoginEntradaDto(string.Empty, string.Empty));
        }


        [HttpPost]
        public IActionResult Login(LoginEntradaDto user)
        {
            // Validación previa
            if (!ModelState.IsValid)
                return View("Login", user); // devuelve errores de validación


            try
            {
               
                var options = new RestClientOptions("http://localhost:5064/api")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest($"Auth/Generate", Method.Post)
                                    .AddJsonBody(user);


                RestResponse response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception("Credenciales inválidas, Inténtalo de nuevo");

                var loginResp = JsonSerializer.Deserialize<LoginRespuestaDto>(
                    response.Content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                // Guardamos JWT y datos de usuario en sesión:
                HttpContext.Session.SetString("token", loginResp.Token);
                HttpContext.Session.SetString("userId", loginResp.User.Id.ToString());
                HttpContext.Session.SetString("userName", loginResp.User.Nombre);
                HttpContext.Session.SetString("userEmail", loginResp.User.Correo);
                HttpContext.Session.SetString("rolUsuario", loginResp.User.Discriminator);


                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
               
            }
           
            return View("Login");
        }

        // Acción para cerrar sesión
        [HttpPost]
        public IActionResult Logout()
        {
            // Elimina todo lo que haya en Session
            HttpContext.Session.Clear();

            // Redirigir al inicio o a la página de login
            return RedirectToAction("Login");
        }



        // GET: /Usuario/CambiarClave
        [HttpGet]
        public IActionResult CambiarClave()
        {
            return View(new CambiarClaveViewModel());
        }


        [HttpPost]
        public IActionResult CambiarClave(CambiarClaveViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                // Aquí haces la llamada PUT a la API
                var client = new RestClient("http://localhost:5064");
                var request = new RestRequest("/api/Auth/CambiarClave", Method.Put)
                    .AddJsonBody(new
                    {
                        ClaveActual = model.ClaveActual,
                        NuevaClave = model.NuevaClave
                    });
                // agrega tu token de sesión
                var token = HttpContext.Session.GetString("token");
                request.AddHeader("Authorization", $"Bearer {token}");

                var resp = client.Execute(request);

                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ViewBag.Message = "Contraseña actualizada con éxito.";
                    return View(new CambiarClaveViewModel());
                }
                else if (resp.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    // La API devolvió 400 cuando la clave actual no coincide,
                    // o bien faltó enviar un campo obligatorio.
                    ViewBag.Error = "La contraseña actual no coincide. Inténtalo de nuevo.";
                    return View(model);
                }
                else
                {
                    //si da 401 redirige a login
                    if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        return RedirectToAction("Login", "Usuario");

                    ViewBag.Error = "Error al cambiar la clave, Inténtalo de nuevo";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al cambiar la clave, Inténtalo de nuevo";
                return View(model);
            }
        }



    }
}
