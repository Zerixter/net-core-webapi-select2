using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_classe.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace core_classe.Pages
{
    public class SeleccionaPersonaModel : PageModel
    {

        private readonly MyContext _context;

        public SeleccionaPersonaModel(MyContext context)
        {
            _context = context;
        }   

        public string Message { get; set; }

        public List<Persona> gent;


        public void OnGet()
        {
         
            gent = _context.Gent.ToList();
            Message = $"Totes les persones";
        }


    }
}
