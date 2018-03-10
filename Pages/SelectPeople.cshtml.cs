using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net_core_webapi_select2.Models;

namespace net_core_webapi_select2.Pages
{
    public class SelectPeopleModel : PageModel
    {
        private readonly MyContext _context;

        public SelectPeopleModel(MyContext context)
        {
            _context = context;
        }   
        public string Message { get; set; }
        public List<Persona> Tothom {get; set;}

        public void OnGet()
        {
            Tothom = _context.Gent.ToList();
            Message = "Primera prova on surt tothom";
        }
    }
}
