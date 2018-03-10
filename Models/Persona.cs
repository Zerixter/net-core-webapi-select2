using System.ComponentModel.DataAnnotations;

namespace net_core_webapi_select2.Models
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
    }
}