using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class PatientAyantDroit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(16)]
        public string Identifiant { get; set; } = string.Empty;
        public string? PatientsIpp { get; set; } // ce champs ajouter apres la creation du table patient miltaire  pour faciler l'affichage des données a
        [Required]
        public int CodeCategorie { get; set; }
        [Required]
        public int CodeSCategorie { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdateBy { get; set; } = string.Empty;
        public string UpdateAt { get; set; } = string.Empty;
    }
}
