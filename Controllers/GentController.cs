using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using core_classe.Models;
//using Google.Cloud.Language.V1;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace core_classe.Controllers
{
    public class GentController : Controller
    {
        private readonly MyContext _context;

        public GentController(MyContext context)
        {
            _context = context;
        }

        // GET: Gent
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gent.ToListAsync());
        }

        // GET: Gent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Gent
                .SingleOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Gent/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Cognoms,Perfil")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Gent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Gent.SingleOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Gent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Cognoms,Perfil")] Persona persona)
        {
            if (id != persona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                /***aquí***    
                var client = LanguageServiceClient.Create(  );
                var response = client.AnalyzeSentiment(new Document()
                {
                    Content = persona.Perfil,
                    Type = Document.Types.Type.PlainText
                });
                var sentiment = response.DocumentSentiment;
                */

                string post_uri = @"https://language.googleapis.com/v1beta2/documents:analyzeSentiment";
                var document = new { document= new {
                                                content=  persona.Perfil,
                                                type= "PLAIN_TEXT"
                                               },
                                     encodingType= "NONE"
                                    };
                var client = new HttpClient
                {
                    BaseAddress = new Uri(post_uri)
                };

                var resposta = client.PostAsJsonAsync( "?key=AIzaSyBHRNR69inLnLdyVRwSSlkjJmtZUHLo4xg",
                                                        document).Result;

                var json_resultant = resposta.Content.ReadAsStringAsync().Result;
                var objecte_resultant = JsonConvert.DeserializeObject<dynamic>( json_resultant );


                if (objecte_resultant.documentSentiment.score<0f) {
                    ModelState.AddModelError("Perfil","A Google no li agrada el teu profile");

                } else {
                    try
                    {
                        _context.Update(persona);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PersonaExists(persona.Id))
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

                
            }
            return View(persona);
        }

        // GET: Gent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Gent
                .SingleOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Gent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _context.Gent.SingleOrDefaultAsync(m => m.Id == id);
            _context.Gent.Remove(persona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
            return _context.Gent.Any(e => e.Id == id);
        }
    }
}
