using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using core_classe.Models;

namespace core_classe.Controllers
{
    [Produces("application/json")]
    [Route("api/GentApi")]
    public class GentApiController : Controller
    {
        private readonly MyContext _context;

        public GentApiController(MyContext context)
        {
            _context = context;
        }


        // GET: api/BuscaGent
        [HttpGet("search")]
        [HttpGet("search/{term}", Name = "SearchPersona")]
        public IActionResult BuscaGent(String term="", 
                                              int pag=0)
        {
            int tamanyDePagina = 10;
            var resposta_dades = (_context
                            .Gent
                            .Where(x=>  (x.Nom + " " + x.Cognoms)
                                         .ToLower()
                                         .Contains(term.ToLower()))                                          
                            .Skip( pag * tamanyDePagina)
                            .Take(tamanyDePagina)
                            .Select(x=>new {id=x.Id, text=(x.Nom + " " + x.Cognoms)})
                            .ToList()
                           );
            var pagination = new { more = ( resposta_dades.Count() == tamanyDePagina)};
            var resposta = new { results = resposta_dades ,
                                 pagination= pagination };
            return Ok( resposta );
        }

        // GET: api/GentApi
        [HttpGet]
        public IEnumerable<Persona> GetGent()
        {
            return _context.Gent;
        }

        // GET: api/GentApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersona([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var persona = await _context.Gent.SingleOrDefaultAsync(m => m.Id == id);

            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        // PUT: api/GentApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona([FromRoute] int id, [FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != persona.Id)
            {
                return BadRequest();
            }

            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GentApi
        [HttpPost]
        public async Task<IActionResult> PostPersona([FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Gent.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersona", new { id = persona.Id }, persona);
        }

        // DELETE: api/GentApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var persona = await _context.Gent.SingleOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Gent.Remove(persona);
            await _context.SaveChangesAsync();

            return Ok(persona);
        }

        private bool PersonaExists(int id)
        {
            return _context.Gent.Any(e => e.Id == id);
        }
    }
}