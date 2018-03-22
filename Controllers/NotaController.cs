using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using core_classe.Models;

namespace core_classe.Controllers
{
    public class NotaController : Controller
    {
        private readonly MyContext _context;

        public NotaController(MyContext context)
        {
            _context = context;
        }

        // GET: Nota
        public async Task<IActionResult> Index()
        {
            var myContext = _context.Nota.Include(n => n.alumne);
            return View(await myContext.ToListAsync());
        }

        // GET: Nota/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Nota
                .Include(n => n.alumne)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // GET: Nota/Create
        public IActionResult Create()
        {
            var consulta = _context.Gent.Select(x=> new {Id=x.Id, Text= $"{x.Cognoms}, {x.Nom}"});
            ViewData["persona_fk"] = new SelectList(consulta, "Id", "Text");
            return View();
        }

        // POST: Nota/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Observacions,nota,persona_fk")] Nota nota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var consulta = _context.Gent.Select(x=> new {Id=x.Id, Text= $"{x.Cognoms}, {x.Nom}"});
            ViewData["persona_fk"] = new SelectList(consulta, "Id", "Text");
            return View(nota);
        }

        // GET: Nota/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Nota.SingleOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }
            var consulta = _context.Gent.Select(x=> new {Id=x.Id, Text= $"{x.Cognoms}, {x.Nom}"});
            ViewData["persona_fk"] = new SelectList(consulta, "Id", "Text");
            return View(nota);
        }

        // POST: Nota/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Observacions,nota,persona_fk")] Nota nota)
        {
            if (id != nota.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaExists(nota.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var consulta = _context.Gent.Select(x=> new {Id=x.Id, Text= $"{x.Cognoms}, {x.Nom}"});
            ViewData["persona_fk"] = new SelectList(consulta, "Id", "Text");
            return View(nota);
        }

        // GET: Nota/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Nota
                .Include(n => n.alumne)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // POST: Nota/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nota = await _context.Nota.SingleOrDefaultAsync(m => m.Id == id);
            _context.Nota.Remove(nota);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaExists(int id)
        {
            return _context.Nota.Any(e => e.Id == id);
        }
    }
}
