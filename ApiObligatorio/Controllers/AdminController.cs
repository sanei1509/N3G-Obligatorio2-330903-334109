using LogicaAccesoDatos.EF;
using Microsoft.AspNetCore.Mvc;

namespace ApiObligatorio.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly LibreriaContext _ctx;

        public AdminController(LibreriaContext ctx)
        {
            _ctx = ctx;
        }

        [HttpPost("rehash")]
        public IActionResult RehashPasswords()
        {
            _ctx.MigrateSeedPasswords();
            return Ok("Contraseñas rehasheadas correctamente.");
        }
    }
}
