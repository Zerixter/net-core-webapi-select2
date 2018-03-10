using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net_core_webapi_select2.Models;

namespace net_core_webapi_select2.Pages
{
    public class AddRandomPeopleModel : PageModel
    {

        public string Message { get; set; }
        private static readonly Random rnd = new Random();

        private readonly MyContext _context;

        public AddRandomPeopleModel(MyContext context)
        {
            _context = context;
        }   
        public void OnGet()
        {
            
            List<string> noms = new List<string>(new string[] 
                                { "Pere", "Juana", "Matilda", "Marta", "Carles"});
            List<string> cognoms = new List<string>(new string[] 
                                { "Pérez", "Dalmau", "Jiménez", "Sales", "Puig"});

            int n = rnd.Next(200);
            for ( int i=0; i <= n; i++) {
                Persona p = new Persona();
                p.Nom = noms[rnd.Next(noms.Count)];
                p.Cognoms = cognoms[rnd.Next(cognoms.Count)] + " " + cognoms[rnd.Next(cognoms.Count)];
                p.Perfil = "";
                _context.Add(p);
            }
            _context.SaveChanges();
            Message = $"Afegides {n} persones";
        }
    }
}
