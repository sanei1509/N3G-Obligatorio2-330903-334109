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
                    throw new Exception("Credenciales inválidas");

                var loginResp = JsonSerializer.Deserialize<LoginRespuestaDto>(
                    response.Content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                // Guardamos JWT y datos de usuario en sesión:
                HttpContext.Session.SetString("token", loginResp.Token);
                HttpContext.Session.SetString("userId", loginResp.User.Id.ToString());
                HttpContext.Session.SetString("userName", loginResp.User.Nombre);
                HttpContext.Session.SetString("userEmail", loginResp.User.Correo);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ViewBag.mensaje = e.Message;
            }
            return View("Index");
        }


        // POST: /Usuario/Login
        //[HttpPost]
        //public IActionResult Login(LoginEntradaDto model)
        //{

        //    try
        //    {

        //        var options = new RestClientOptions("")
        //        {
        //            MaxTimeout = -1
        //        };
        //        var client = new RestClient(options);
        //        //var request = new RestRequest($"/api/v1/Usuarios/{user.Email}"), Method.Get;
        //        //RestResponse response = client.Execute(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);

        //    }
        //        //var result = _login.Execute(model);

        //        //if (!result.Autenticado)
        //        //{
        //        //    ViewBag.Error = result.Mensaje;
        //        //    return View(model);
        //        //}

        //        // Obtenemos el usuario para preguntar
        //        //var usuario = _getByEmail.Execute(model.Correo);

        //        //if (usuario.Discriminator.ToLower() == "admin")
        //        //{
        //        //    HttpContext.Session.SetString("Rol", "Admin");
        //        //}
        //        //else
        //        //{
        //        //    HttpContext.Session.SetString("Rol", "NoAdmin");
        //        //}

        //        //// Si todo OK, guardamos la sesión:
        //        //HttpContext.Session.SetString("Logueado", "true");
        //        //HttpContext.Session.SetString("CorreoEmpleado", model.Correo);

        //        return RedirectToAction("Index", "Home");
        //}


        // Acción para dar de alta un usuario
        //[UsuarioLogueado]
        //[AdminAutorizado]

        [HttpGet]
        public IActionResult Crear()
        {
            var model = new CrearUsuarioDto(
                                Id: 0,
                                Nombre: "",
                                Apellido: "",
                                Correo: "",
                                Clave: "",
                                Telefono: "",
                                CorreoResponsable: HttpContext.Session.GetString("CorreoEmpleado")
                            );
            return View(model);
        }

        // Acción para cerrar sesión
        [HttpPost]
        public IActionResult Logout()
        {
            // Eliminar la sesión
            HttpContext.Session.Remove("Logueado");

            // Redirigir al inicio o a la página de login
            return RedirectToAction("Login");
        }


    }
}
