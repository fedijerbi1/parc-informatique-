using System.ComponentModel.DataAnnotations;

namespace Webapp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone]
        [StringLength(15)]
        public string? Telephone { get; set; }
        public string Poste { get; set; } = string.Empty;
        public string? Departement { get; set; }
        public DateTime DateEmbauche { get; set; } = DateTime.Now;
        public bool IsActif { get; set; } = true;
    }
}
