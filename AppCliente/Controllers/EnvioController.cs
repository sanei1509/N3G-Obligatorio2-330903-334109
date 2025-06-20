
using AppCliente.Models.Envios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;

namespace AppCliente.Controllers
{
    public class EnvioController : Controller
    {
        public EnvioController()
        {
        }

        // GET: /Envio/Index
        [HttpGet]
        public IActionResult Index(string nroTracking)
        {
            // Si no viene nroTracking, mostramos la vista vacía
            if (string.IsNullOrWhiteSpace(nroTracking))
                return View();

            try
            {
                // Preparamos el cliente RestSharp
                var client = new RestClient(new RestClientOptions("http://localhost:5064/api") { MaxTimeout = -1 });
                var request = new RestRequest($"envios/{nroTracking}", Method.Get);

                // Agregamos token si existe en sesión
                var token = HttpContext.Session.GetString("token");
                if (!string.IsNullOrEmpty(token))
                    request.AddHeader("Authorization", $"Bearer {token}");

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ViewBag.TrackError = "No existe ese número de tracking.";
                    return View();
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ViewBag.TrackError = "Token inválido o expirado. Por favor inicia sesión.";
                    return View();
                }
                if (!response.IsSuccessful)
                {
                    ViewBag.TrackError = "Error al consultar el envío. Intenta nuevamente.";
                    return View();
                }

                // Deserializamos el JSON al DTO
                var envio = JsonSerializer.Deserialize<EnvioListadoDto>(
                    response.Content!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                // Enviamos el modelo a la vista
                return View(envio);
            }
            catch (Exception ex)
            {
                ViewBag.TrackError = "Ocurrió un error: " + ex.Message;
                return View();
            }
        }

    }
}
