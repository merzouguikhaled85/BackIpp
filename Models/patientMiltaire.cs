using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class patientMiltaire
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(16)]
        public string Identifiant { get; set; } = string.Empty;

        public string? PatientsIpp { get; set; } // ce champs ajouter apres la creation du table patient miltaire  pour faciler l'affichage des données avec ipp



        [Required]
        [StringLength(20)]
        public string MatriculeRec { get; set; } = string.Empty;

        [StringLength(4)]
        public string AnneeRec { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Armee { get; set; } = string.Empty;

        [Required]
        public int CodeGrade { get; set; } 

        [Required]
        public int CodeCorps { get; set; }

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
