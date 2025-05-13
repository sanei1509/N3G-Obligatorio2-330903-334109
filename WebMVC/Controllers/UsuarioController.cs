using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.CasosUso.Usuarios;
using LogicaNegocio.Excepciones.UsuarioExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class UsuarioController : Controller
    {
        //inyectar caso de uso de usuario
        private ILogin<LoginRespuestaDto> _login;
        private IGetAll<UsuarioListadoDto> _getAll;
        private IGetById<UsuarioDto> _getById;
        private IGetById<CrearUsuarioDto> _getByIdEditar;
        private IAdd<CrearUsuarioDto> _add;
        private IRemove _remove;
        private IUpdate<CrearUsuarioDto> _update;

        public UsuarioController(ILogin<LoginRespuestaDto> login, IGetAll<UsuarioListadoDto> getAll, IAdd<CrearUsuarioDto> add, IRemove remove, IUpdate<CrearUsuarioDto> update, IGetById<UsuarioDto> getById, IGetById<CrearUsuarioDto> getByIdEditar)
        {
            _login = login;
            _getAll = getAll;
            _add = add;
            _remove = remove;
            _update = update;
            _getById = getById;
            _getByIdEditar = getByIdEditar;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Usuario/Login
        [HttpPost]
        public IActionResult Login(LoginEntradaDto model)
        {
            var result = _login.Execute(model);

            if (!result.Autenticado)
            {
                ViewBag.Error = result.Mensaje;
                return View(model);
            }

            // Si todo OK, guardamos la sesión:
            HttpContext.Session.SetString("Logueado", "true");
            HttpContext.Session.SetString("CorreoEmpleado", model.Correo);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Gestion()
        {
            IEnumerable<UsuarioListadoDto> listaUsuarios = _getAll.Execute();
            return View(listaUsuarios);
        }


        // Acción para dar de alta un usuario
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Borrar(int id)
        {
            _remove.Execute(id);
            return RedirectToAction("Gestion");
        }


        [HttpGet]
        public IActionResult Editar(int id)
        {
            // Se llama al caso de uso GetById para obtener los datos actuales
            var usuarioDto = _getByIdEditar.Execute(id);
            return View(usuarioDto);
        }

        [HttpPost]
        public IActionResult Editar(int id, CrearUsuarioDto usuarioDto)
        {
            try
            {
                _update.Execute(id, usuarioDto);
                return RedirectToAction("Gestion");
            }
            catch (NombreException)
            {
                ViewBag.Message = "El Nombre es incorrecto";
            }
            catch (ApellidoException)
            {
                ViewBag.Message = "El Apellido es incorrecto";
            }
            catch (CorreoException)
            {
                ViewBag.Message = "El Correo es incorrecto";
            }
            catch (ClaveException)
            {
                ViewBag.Message = "La Clave es incorrecta";
            }
            catch (TelefonoException)
            {
                ViewBag.Message = "El Telefono es incorrecto";
            }


            return View();
        }


        [HttpPost]
        public IActionResult Crear(CrearUsuarioDto usuarioDto)
        {
            try
            {
                _add.Execute(usuarioDto);
                return RedirectToAction("Gestion");
            }
            catch (NombreException)
            {
                ViewBag.Message = "El Nombre es incorrecto";
            }
            catch (ApellidoException)
            {
                ViewBag.Message = "El Apellido es incorrecto";
            }
            catch (CorreoException)
            {
                ViewBag.Message = "El Correo es incorrecto";
            }
            catch (ClaveException)
            {
                ViewBag.Message = "La Clave es incorrecta";
            }
            catch (TelefonoException)
            {
                ViewBag.Message = "El Telefono es incorrecto";
            }
            catch (YaExisteUsuarioException)
            {
                ViewBag.Message = "El Usuario Ya Existe";
            }

            return View();

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
