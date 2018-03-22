using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace core_classe.Models
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Nom { get; set; }

        [MaxLength(150)]
        public string Cognoms { get; set; }

        [DataType(DataType.MultilineText)]
        public string Perfil { get; set; }

        //propietat de navegació
        public virtual List<Nota> notes {get; set; } = new List<Nota>();
    }
}