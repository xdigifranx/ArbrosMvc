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
        public ActionResult<IQueryable<Tiempo>> GetTiempo()
        {
            return Ok(_context.Tiempo);
        }

        // Finalizar un trabajo
        [HttpPost("FinalizarTrabajo/{id}")]
        public async Task<ActionResult> FinalizarTrabajo(int id)
        {
            var trabajo = await _context.Tiempo.FindAsync(id);
            if (trabajo == null)
            {
                return NotFound();
            }

            trabajo.HoraFin = DateTime.Now;

            // Calcular duración aquí si es necesario

            await _context.SaveChangesAsync();
            return Ok(new { trabajo }); // Devuelves el trabajo y la duración calculada
        }


        // Obtener el último trabajo
        [HttpGet("GetUltimoTrabajo")]
        public async Task<ActionResult<Tiempo>> GetUltimoTrabajo()
        {
            var trabajo = await _context.Tiempo.OrderByDescending(t => t.Id).FirstOrDefaultAsync();
            if (trabajo == null)
            {
                return NotFound(); // Retorna 404 si no hay trabajos
            }
            return Ok(trabajo); // Retorna el último trabajo
        }

        // Iniciar un trabajo
        [HttpPost("IniciarTrabajo")]
        public async Task<ActionResult<Tiempo>> IniciarTrabajo([FromBody] string tarea)
        {
            if (string.IsNullOrWhiteSpace(tarea))
            {
                return BadRequest("La tarea no puede ser nula o vacía."); // Validación
            }

            var trabajo = new Tiempo
            {
                HoraInicio = DateTime.Now, // Establece HoraInicio al momento actual
                Tareas = tarea,
                FechaCreacion = DateTime.Now,
                HoraFin = null
            };

            _context.Tiempo.Add(trabajo);
            await _context.SaveChangesAsync(); // Guarda el nuevo trabajo en la base de datos
            return CreatedAtAction(nameof(GetTiempo), new { id = trabajo.Id }, trabajo); // Retorna 201 Created
        }

        // Agregar un nuevo objeto Tiempo
        [HttpPost]
        public async Task<ActionResult<Tiempo>> PostTiempo([FromBody] Tiempo tiempo)
        {
            if (tiempo == null)
            {
                return BadRequest("El objeto de tiempo no puede ser nulo."); // Validación
            }

            try
            {
                // Inicializa propiedades del objeto Tiempo
                tiempo.FechaCreacion = DateTime.Now;
                tiempo.HoraInicio = DateTime.MinValue; // Puedes establecer otro valor predeterminado si es necesario
                tiempo.HoraFin = null;
                // Asegúrate de que esta propiedad tenga un setter

                _context.Tiempo.Add(tiempo);
                await _context.SaveChangesAsync(); // Guarda el nuevo objeto en la base de datos
                return CreatedAtAction(nameof(GetTiempo), new { id = tiempo.Id }, tiempo); // Retorna 201 Created
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}

