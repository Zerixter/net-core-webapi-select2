using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using net_core_webapi_select2.Models;
using System.Linq;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class ApiGentController : Controller
    {
        private readonly MyContext _context;

        public ApiGentController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Persona> GetAll()
        {
            return _context.Gent.ToList();
        }

        [HttpGet("search")]
        [HttpGet("search/{term}", Name = "SearchPersona")]
        public IActionResult SearchByTerm(string term = "", int page = 0)
        {
            var consulta = _context
                            .Gent
                            .Where( x=> (x.Nom + " " + x.Cognoms).Contains(term) )
                            .OrderBy( x=> x.Nom )
                            .ThenBy(x=>x.Cognoms)
                            .Select( x=> new {id = x.Id, text = $"{x.Nom} {x.Cognoms}" } )
                            ;
            var items = consulta
                        .Skip( page * 10 )
                        .Take(10)
                        .ToList();
            var pagination_object = new {more = consulta.Count() > page * 10 };
            var result = new { results = items, pagination = pagination_object };
            return new ObjectResult(result);
        }

        [HttpGet("{id:long}", Name = "GetPersona")]
        public IActionResult GetById(long id)
        {
            var item = _context.Gent.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Persona item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Gent.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Persona item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var persona = _context.Gent.FirstOrDefault(t => t.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            persona.Nom = item.Nom;
            persona.Cognoms = item.Cognoms;
            persona.Perfil = item.Perfil;

            _context.Gent.Update(persona);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Gent.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Gent.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}