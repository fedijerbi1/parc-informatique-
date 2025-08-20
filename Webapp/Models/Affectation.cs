using System.ComponentModel.DataAnnotations;

namespace Webapp.Models
{
    public class Affectation
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Veuillez sélectionner un employé")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        
        [Required(ErrorMessage = "Veuillez sélectionner un équipement")]
        public int EquipmentId { get; set; }
        public Equipment? Equipment { get; set; }
        
        [Required(ErrorMessage = "La date d'affectation est obligatoire")]
        public DateTime DateAffectation { get; set; } = DateTime.Now;
        
        public DateTime? DateRetour { get; set; }
        
        public string? Commentaires { get; set; }
        
        public bool IsActif { get; set; } = true;
    }
}