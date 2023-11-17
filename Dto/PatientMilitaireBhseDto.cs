using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto
{
    public class PatientMilitaireBhseDto
    {
        [Key]
        public string IDENTIFIANT { get; set; } = string.Empty;
        public string NOM { get; set; } = string.Empty;
        public string PRENOM { get; set; } = string.Empty;
        public DateTime DATE_NAISSANCE { get; set; }
       
        public string SEXE { get; set; } = string.Empty;
        public string MATRICULE_CORPS { get; set; } = string.Empty;
        public string MATRICULE_REC { get; set; } = string.Empty;

        [StringLength(4)]
        public string ANNEE_REC { get; set; } = string.Empty;
        public string CARTE_IDENTITE { get; set; } = string.Empty;
        public string DATE_CIN { get; set; } = string.Empty;
        public string LIEU_CIN { get; set; } = string.Empty;
        public string ADRESSE { get; set; } = string.Empty;
        public string TELEPHONE { get; set; } = string.Empty;

        [MaxLength(5)]
        public string ARMEE { get; set; } = string.Empty;
        public int CODE_GRADE { get; set; }
        public int CODE_CORPS { get; set; }
        public int CODE_CATEGORIE { get; set; }
        public int CODE_SCATEGORIE { get; set; }
        public int CODE_SVC_EMPLOYEUR { get; set; }
        public string IPP { get; set; } = string.Empty;
    }
}
