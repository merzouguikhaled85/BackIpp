   using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class PatientBhse
    {
        [Key]
        public string IDENTIFIANT { get; set; } = string.Empty;
        public string NOM { get; set; } = string.Empty;
        public string PRENOM { get; set; } = string.Empty;
        public DateTime DATE_NAISSANCE { get; set; }
        
        public string SEXE { get; set; } = string.Empty;
        public string MATRICULE_CORPS { get; set; } = string.Empty;
        public string MATRICULE_REC { get; set; } = string.Empty;
        public string ANNEE_REC { get; set; } = string.Empty;
        public string CARTE_SOINS { get; set; } = string.Empty;
        public string CARTE_IDENTITE { get; set; } = string.Empty;
        public string DATE_CIN { get; set; } = string.Empty;
        public string LIEU_CIN { get; set; } = string.Empty;
        public string PASSEPORT { get; set; } = string.Empty;
        public string QUALITE { get; set; } = string.Empty;
        public string MATRICULE_ASSURE { get; set; } = string.Empty;
        public string LIEN_PARENTE { get; set; } = string.Empty;
        public int INDEX_ASSURE { get; set; }
        public string ADRESSE { get; set; } = string.Empty;
        public string TELEPHONE { get; set; } = string.Empty;
        public string ARMEE { get; set; } = string.Empty;
        public int CODE_GRADE { get; set; }
        public int CODE_CORPS { get; set; }
        public int CODE_CATEGORIE { get; set; }
        public int CODE_SCATEGORIE { get; set; }
        public int CODE_SVC_EMPLOYEUR { get; set; }
        public string NOM_EPOUX { get; set; } = string.Empty;
        public string PRENOM_EPOUX { get; set; } = string.Empty;
        public string PERE_EPOUX { get; set; } = string.Empty;
        public string TYP_PATIENT { get; set; } = string.Empty;
        public int CODE_VILLE_R { get; set; }
        public string NUM_CARTE_HAND { get; set; } = string.Empty;
        public string DATE_VALIDITE_CHAND { get; set; } = string.Empty;
        public string SERVICE { get; set; } = string.Empty;
        public string NUMERO_URGENCE { get; set; } = string.Empty;
        public string APSI { get; set; } = string.Empty;
        public string RACINE { get; set; } = string.Empty;
        public string CLE { get; set; } = string.Empty;
        public int INDEX_GREFFE { get; set; }
        public int IU { get; set; }
        public string DATE_HANDICAPE { get; set; } = string.Empty;
        public string DATE_APCI { get; set; } = string.Empty;
        public int ID_HMPIT { get; set; }
        public string TRANCHE_AGE { get; set; } = string.Empty;
        public string VIP { get; set; } = string.Empty;
        public string DATE_VALIDITE_CARNET { get; set; } = string.Empty;
        public string FSI { get; set; } = string.Empty;
        public string NUM_DIALYSE { get; set; } = string.Empty;
        public string POSITION_DIAL { get; set; } = string.Empty;
        public string NUM_GREFFE { get; set; } = string.Empty;
        public string ID_UNIQUE { get; set; } = string.Empty;
        public string POSITION { get; set; } = string.Empty;
        public string IPP { get; set; } = string.Empty;
    }
    


}
