using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.Dto
{
    public class PatientMilitaireDto
    {
        [Key]
        public string? Ipp { get; set; }
        [Required]
        public string Nom_Patient_Fr { get; set; } = string.Empty;
        [Required]
        public string Prenom_Patient_Fr { get; set; } = string.Empty;
        [Required]
        public string Prenom_Pere_Fr { get; set; } = string.Empty;
        [Required]
        public string Nom_Patient_Ar { get; set; } = string.Empty;
        [Required]
        public string Prenom_Patient_Ar { get; set; } = string.Empty;
        [Required]
        public string Prenom_Pere_Ar { get; set; } = string.Empty;
        [Required]
        public string Date_Naissance { get; set; } = string.Empty;

        public int Lieu_Naissance { get; set; }
        [Required]
        public string Sexe { get; set; } = string.Empty;

        public string Carte_Identite { get; set; } = string.Empty;
        public string Date_Cin { get; set; } = string.Empty;
        public string Passeport { get; set; } = string.Empty;
        [Required]
        public string Adresse_Fr { get; set; } = string.Empty;
        [Required]
        public string Adresse_Ar { get; set; } = string.Empty;
        [Required]
        public int Ville { get; set; }
       
        public string Code_Postal { get; set; } = string.Empty;
        [Required]
        public string Telephone1 { get; set; } = string.Empty;
        public string Telephone2 { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdateBy { get; set; } = string.Empty;
        public string UpdateAt { get; set; } = string.Empty;
        [Required]
        public string Situation { get; set; } = string.Empty;
        public ICollection<patientMiltaire>? patientMiltaires { get; set; }
    }
}
