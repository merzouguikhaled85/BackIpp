using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class PatientCnam
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(16)]
        public string Identifiant { get; set; } = string.Empty;
        public string? PatientsIpp { get; set; } // ce champs ajouter apres la creation du table patient miltaire  pour faciler l'affichage des données avec ipp
        [Required]
        public int IndexAssure { get; set; }
        [Required]
        public string Matricule_Assure { get; set; } = string.Empty;
        [Required]
        public string Lien_Parente { get; set; } = string.Empty;

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

