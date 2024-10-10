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
            var lista = await _context.Personas.ToListAsync();
            return Ok(lista);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<Persona>>> GetSinglePersona(int id)
        {
            var miobjeto = await _context.Personas.FirstOrDefaultAsync(ob => ob.Id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }

            return Ok(miobjeto);
        }
        [HttpPost]
        public async Task<ActionResult<Persona>> CreatePersona(Persona objeto)
        {

            try
            {
                _context.Personas.Add(objeto);
                await _context.SaveChangesAsync();
                return Ok(await GetDbPersona());
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            };


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Persona>>> UpdatePersona(Persona objeto)
        {

            var DbObjeto = await _context.Personas.FindAsync(objeto.Id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.Name = objeto.Name;


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

