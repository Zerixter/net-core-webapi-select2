using System.ComponentModel.DataAnnotations;

namespace core_classe.Models
{
    public class Nota
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Observacions { get; set; }

        public int nota { get; set; }

        //fk a persona
        public int persona_fk {get; set;}
        public Persona alumne {get; set; }

    }
}