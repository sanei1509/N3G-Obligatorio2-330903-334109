
using AppCliente.Models.Envios;
using LogicaNegocio.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net;
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



        // GET /Envio/Listado
        [HttpGet]
        public IActionResult ListadoEnvios()
        {
            var token = HttpContext.Session.GetString("token");
            var client = new RestClient("http://localhost:5064");
            var request = new RestRequest("/api/Envios/enviosDelCliente", Method.Get);
            request.AddHeader("Authorization", $"Bearer {token}");

            var resp = client.Execute<List<EnvioListadoDto>>(request);
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                return View(resp.Data);
            }
            else if (resp.StatusCode == HttpStatusCode.NoContent)
            {
                ViewBag.Message = "No tienes envíos para mostrar.";
                return View(new List<EnvioListadoDto>());
            }
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                ViewBag.Error = "Error al cargar tus envíos. Inténtalo de nuevo.";
                return View(new List<EnvioListadoDto>());
            }
        }


        // GET: /Envio/Detalles?nroTracking=TRK0000001
        [HttpGet]
        public IActionResult DetalleEnvio(string nroTracking)
        {
            if (string.IsNullOrWhiteSpace(nroTracking))
                return RedirectToAction("ListadoEnvios");

            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Usuario");

            var client = new RestClient("http://localhost:5064");
            var request = new RestRequest($"/api/Envios/{Uri.EscapeDataString(nroTracking)}", Method.Get);
            request.AddHeader("Authorization", $"Bearer {token}");

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var envio = JsonSerializer.Deserialize<EnvioListadoDto>(response.Content, opciones)!;
                return View(envio);
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                TempData["Error"] = "No se encontró el envío.";
                return RedirectToAction("ListadoEnvios");
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                TempData["Error"] = "Error al cargar el detalle del envío.";
                return RedirectToAction("ListadoEnvios");
            }
        }

        [HttpGet]
        public IActionResult Buscar(DateTime? fechaInicio, DateTime? fechaFin, string? estado, string? comentario)
        {

            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Usuario");



            var filtro = new FiltroBusquedaEnviosDto
            {
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                Estado = estado,
                Comentario = comentario
            };

            var client = new RestClient("http://localhost:5064");
            var request = new RestRequest("/api/Envios/filtrar", Method.Get);

            // Agregar headers y parámetros
            request.AddHeader("Authorization", $"Bearer {token}");

            if (fechaInicio.HasValue)
                request.AddQueryParameter("fechaInicio", fechaInicio.Value.ToString("yyyy-MM-dd"));
            if (fechaFin.HasValue)
                request.AddQueryParameter("fechaFin", fechaFin.Value.ToString("yyyy-MM-dd"));
            if (!string.IsNullOrWhiteSpace(estado))
                request.AddQueryParameter("estado", estado);
            if (!string.IsNullOrWhiteSpace(comentario))
                request.AddQueryParameter("comentario", comentario);

            var response = client.Execute<List<EnvioListadoDto>>(request);


            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return RedirectToAction("Login", "Usuario");

            ViewBag.Envios = response.Data ?? new List<EnvioListadoDto>();

            // Podés armar una lista fija o cargarla desde tu API si lo preferís
            ViewBag.Estados = Enum.GetNames(typeof(EstadoEnvio)).ToList();

            return View("Buscar", filtro);
        }



    }
}
