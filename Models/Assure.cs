using PdfSharpCore.Pdf.Content.Objects;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Assure
    {
        [Key]
        [MaxLength(15)]
        public string MATRICULE_ASSURE { get; set; }

        [Required]
        public int CODE_SCATEGORIE { get; set; }
        [Required]
        public int CODE_CASISSE { get; set; }
        public int N_CARNET { get; set; }
        [Required]
        public DateTime DATE_VALIDITE { get; set; }

        [Required]
        [MaxLength(50)]
        public string NOM_PRENOM_ASSURE { get; set; } = string.Empty;
        public string DRAPEAU { get; set; } = string.Empty;
        public string N_ASSURE { get; set; } = string.Empty;
        public string CLE { get; set; } = string.Empty;


    }
}
