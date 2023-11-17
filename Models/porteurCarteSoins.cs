using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class porteurCarteSoins
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(16)]
        public string Identifiant { get; set; } = string.Empty;
        public string? PatientsIpp { get; set; } // ce champs ajouter apres la creation du table patient miltaire  pour faciler l'affichage des données avec ipp


        [Required]
        public int CodeCategorie { get; set; }

        [Required]
        public int CodeSCategorie { get; set; }

        [StringLength(20)]
        public string NumCarteSoin { get; set; }=string.Empty;

        [Required]
        public string DateValidite { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Situation { get; set; }= string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdateBy { get; set; } = string.Empty;
        public string UpdateAt { get; set; } = string.Empty;
    }
}
