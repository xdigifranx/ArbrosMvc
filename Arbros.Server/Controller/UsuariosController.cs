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
                return Ok(new { loginRequest.User });
            }
            else {
                return Conflict("error");
            }
            
        }
        //[HttpPost]
        //public async Task<ActionResult<Usuarios>> PostUsuarios([FromBody] Usuarios usuarios)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        _context.Usuarios.Add(usuarios);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Error al guardar el usuario: {ex.Message}");
        //    }

        //    return CreatedAtAction(nameof(GetUsuarios), new { id = usuarios.Email }, usuarios);
        //}
    }
}

