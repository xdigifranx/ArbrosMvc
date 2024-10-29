using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Arbros.Shared.Models;

namespace Arbros.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly AplicacionDbContex _context;

        public PaisesController(AplicacionDbContex context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IQueryable<Paises>> GetPaises()
        {
            return Ok(_context.Paises);
        }

        [HttpGet("{id}")]
        public ActionResult<Paises> GetPais(int id)
        {
            var pais = _context.Paises.Find(id);
            if (pais == null)
            {
                return NotFound();
            }
            return Ok(pais);
        }
    }
}
