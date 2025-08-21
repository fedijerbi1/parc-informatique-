using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Webapp.Models
{
    public class Equipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Type { get; set; } = string.Empty;
        public string? Marque { get; set; }
        public string? Modele { get; set; }

        [Required(ErrorMessage = "Le numéro de série est obligatoire")]
        public string NumeroSerie { get; set; } = string.Empty;

        public DateTime? DateAchat { get; set; }
        public DateTime? DateFinGarantie { get; set; }
        public string Statut { get; set; } = "En service"; 
        public string? Description { get; set; }

        public DateTime? DateDerniereAffectation { get; set; } 



   
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
