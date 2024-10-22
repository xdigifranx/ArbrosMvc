using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Arbros.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arbros.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosDbContext _context;

        public UsuariosController(UsuariosDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuarios>>> GetUsuarios()
        {
            var users = await _context.Usuarios.ToListAsync();
            if (users == null || !users.Any())
            {
                return NotFound("No hay usuarios disponibles.");
            }
            return Ok(users);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Usuarios loginRequest)
        {
            var resultado = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == loginRequest.Email && x.Password == loginRequest.Password);

            if (resultado != null)
            {
                return Ok(new { User = resultado.User });
            }
            else {
                return Conflict();
            }

        }
        [HttpPost("Registro")]
        public async Task<IActionResult> Register([FromBody] Usuarios NuevoUsuario)
        {
            if (NuevoUsuario == null || string.IsNullOrEmpty(NuevoUsuario.Email) || string.IsNullOrEmpty(NuevoUsuario.Password))
            {
                return BadRequest(new { message = "Datos de usuario incompletos o inválidos" });
            }

            if (_context.Usuarios.Any(u => u.Email == NuevoUsuario.Email))
            {
                return Conflict(new { message = "El correo ya está registrado" });
            }

            _context.Usuarios.Add(NuevoUsuario);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario registrado exitosamente" });
        }

    }
}


