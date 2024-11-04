using Arbros.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Arbros.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiempoController : ControllerBase
    {
        private readonly TiempoDbContex _context;

        public TiempoController(TiempoDbContex context)
        {
            _context = context;
        }

        // Obtener todas las tareas
        [HttpGet]
        public async Task<ActionResult<List<Tiempo>>> GetTiempo()
        {
            var tiempos = await _context.Tiempo.ToListAsync();
            return Ok(tiempos);
        }


        // Agregar un nuevo objeto Tiempo
        [HttpPost]
        public async Task<ActionResult<Tiempo>> AgregarTiempo([FromBody] Tiempo tiempo)
        {
            if (tiempo == null || string.IsNullOrWhiteSpace(tiempo.Tareas))
            {
                return BadRequest("El objeto de tiempo o la tarea no pueden ser nulos."); // Validación
            }

            _context.Tiempo.Add(tiempo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTiempo), new { id = tiempo.Id }, tiempo); // Devuelve la tarea creada
        }

        // Obtener el último trabajo
        [HttpGet("GetUltimoTrabajo")]
        public async Task<ActionResult<Tiempo>> GetUltimoTrabajo()
        {
            var trabajo = await _context.Tiempo.OrderByDescending(t => t.Id).FirstOrDefaultAsync();
            if (trabajo == null)
            {
                return NotFound();
            }
            return Ok(trabajo);
        }

        // Agregar un nuevo objeto Tiempo o iniciar un trabajo
        [HttpPost("IniciarTrabajo")]
        public async Task<ActionResult<Tiempo>> IniciarTrabajo([FromBody] Tiempo tiempo)
        {
            if (tiempo == null || string.IsNullOrWhiteSpace(tiempo.Tareas))
            {
                return BadRequest("El objeto de tiempo o la tarea no pueden ser nulos."); // Validación
            }

            try
            {
                var trabajoExistente = await _context.Tiempo.FindAsync(tiempo.Id);
                if (trabajoExistente == null)
                {
                    return NotFound("El trabajo no existe."); // Manejo de no encontrado
                }

                trabajoExistente.HoraInicio = DateTime.Now; // Establece HoraInicio al momento actual
                _context.Entry(trabajoExistente).State = EntityState.Modified; // Marca el objeto como modificado
                await _context.SaveChangesAsync();

                return Ok(trabajoExistente); // Devuelve el trabajo actualizado
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpPost("FinalizarTrabajo/{id}")]
        public async Task<ActionResult> FinalizarTrabajo(int id)
        {
            var trabajo = await _context.Tiempo.FindAsync(id);
            if (trabajo == null)
            {
                return NotFound();
            }

            trabajo.HoraFin = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(new { trabajo });
        }
    }
}

