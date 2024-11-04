using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Arbros.Shared.Models;
using Arbros.Server;

namespace Arbros.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {

        private readonly AplicacionDbContex _context;

        public PersonaController(AplicacionDbContex context)
        {

            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Persona>>> GetPersona()
        {
            var lista = await _context.Personas
                .Include(p => p.Pais)  // Esto incluye la información del país relacionado
                .ToListAsync();
            foreach (var persona in lista)
            {
                Console.WriteLine($"Nombre: {persona.Name}, País: {persona.Pais?.Pais}");
            }

            return Ok(lista);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Persona>> GetSinglePersona(int id)
        {
            // Incluir la relación con Paises
            var miobjeto = await _context.Personas
                .Include(p => p.Pais) 
                .FirstOrDefaultAsync(ob => ob.Id == id);

            if (miobjeto == null)
            {
                return NotFound(" :/");
            }

            return Ok(miobjeto);
        }

        [HttpPost]
        public async Task<ActionResult<Persona>> CreatePersona(Persona objeto)
        {
            // Validar si PaisId fue proporcionado
            if (objeto.PaisId <= 0)
            {
                return BadRequest("PaisId es requerido.");
            }
            objeto.Pais = null;

            try
            {
                _context.Personas.Add(objeto);
                await _context.SaveChangesAsync();
                return Ok(await GetDbPersona());
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<List<Persona>>> UpdatePersona(int id, Persona objeto)
        {
            if (id != objeto.Id) return BadRequest("ID no coincide.");

            // Asegúrate de que PaisId sea válido
            if (objeto.PaisId <= 0) return BadRequest("PaisId es requerido.");

            var DbObjeto = await _context.Personas.FindAsync(id);
            if (DbObjeto == null) return NotFound("No se encuentra la persona.");

            // Actualiza las propiedades necesarias
            DbObjeto.Name = objeto.Name;
            DbObjeto.PaisId = objeto.PaisId;

            await _context.SaveChangesAsync();

            return Ok(await _context.Personas.ToListAsync());
        }





        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<List<Persona>>> DeletePersona(int id)
        {
            var DbObjeto = await _context.Personas.FirstOrDefaultAsync(Ob => Ob.Id == id);
            if (DbObjeto == null)
            {
                return NotFound("no existe :/");
            }

            _context.Personas.Remove(DbObjeto);
            await _context.SaveChangesAsync();

            return Ok(await GetDbPersona());
        }


        private async Task<List<Persona>> GetDbPersona()
        {
            return await _context.Personas.ToListAsync();
        }
    }
}

