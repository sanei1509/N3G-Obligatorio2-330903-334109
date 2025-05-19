using CasoUsoCompartida.DTOs.Envios;
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAplicacion.CasosUso.Usuarios;
using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.Entidades.Usuarios.Empleados;
using LogicaNegocio.Excepciones.UsuarioExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Filtros;

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
        private IGetByEmail<UsuarioDto> _getByEmail;

        public UsuarioController(ILogin<LoginRespuestaDto> login, IGetAll<UsuarioListadoDto> getAll, IAdd<CrearUsuarioDto> add, IRemove remove, IUpdate<CrearUsuarioDto> update, IGetById<UsuarioDto> getById, IGetById<CrearUsuarioDto> getByIdEditar, IGetByEmail<UsuarioDto> getByEmail)
        {
            _login = login;
            _getAll = getAll;
            _add = add;
            _remove = remove;
            _update = update;
            _getById = getById;
            _getByIdEditar = getByIdEditar;
            _getByEmail = getByEmail;
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

                // Obtenemos el usuario para preguntar
                var usuario = _getByEmail.Execute(model.Correo);

                if (usuario.Discriminator.ToLower() == "admin")
                {
                    HttpContext.Session.SetString("Rol", "Admin");
                }
                else
                {
                    HttpContext.Session.SetString("Rol", "NoAdmin");
                }

                // Si todo OK, guardamos la sesión:
                HttpContext.Session.SetString("Logueado", "true");
                HttpContext.Session.SetString("CorreoEmpleado", model.Correo);

                return RedirectToAction("Index", "Home");
        }

        [UsuarioLogueado]
        [HttpGet]
        public IActionResult Gestion()
        {
            TempData["Rol"] = HttpContext.Session.GetString("Rol");
            IEnumerable<UsuarioListadoDto> usuarios = _getAll.Execute();
            var listaUsuarios = usuarios.OrderBy(e => e.Discriminator).ToList();
            return View(listaUsuarios);
        }


        // Acción para dar de alta un usuario
        [UsuarioLogueado]
        [AdminAutorizado]
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

        [UsuarioLogueado]
        [AdminAutorizado]
        [HttpGet]
        public IActionResult Borrar(int id)
        {
            try
            {
                var dtoEliminar = new CrearUsuarioDto(
                                                Id: id,
                                                Nombre: "",
                                                Apellido: "",
                                                Correo: "",
                                                Clave: "",
                                                Telefono: "",
                                                CorreoResponsable: HttpContext.Session.GetString("CorreoEmpleado")
                                );
                _remove.Execute(dtoEliminar);
                return RedirectToAction("Gestion");
            }
            catch (EmpleadoConEnvioException)
            {

                TempData["Message"] = "El Funcionario que desea borrar ya ha creado envios";
                return RedirectToAction("Gestion");

            }

        }

        [UsuarioLogueado]
        [AdminAutorizado]
        [HttpGet]
        public IActionResult Editar(int id)
        {
            // Se llama al caso de uso GetById para obtener los datos actuales
            var usuarioDto = _getByIdEditar.Execute(id);
            return View(usuarioDto);
        }

        [HttpPost]
        [AdminAutorizado]
        public IActionResult Editar(int id, CrearUsuarioDto usuarioDto)
        {
            try
            {
                var dtoModificar = new CrearUsuarioDto(
                    Id: id,
                    Nombre: usuarioDto.Nombre,
                    Apellido: usuarioDto.Apellido,
                    Correo: usuarioDto.Correo,
                    Clave: usuarioDto.Clave,
                    Telefono: usuarioDto.Telefono,
                    CorreoResponsable: HttpContext.Session.GetString("CorreoEmpleado")
    );
                _update.Execute(id, dtoModificar);
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

        [AdminAutorizado]
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
                ViewBag.Message = "La clave debe tener al menos 6 caracteres e incluir letras, números y al menos uno de los siguientes caracteres especiales: + . #";
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
