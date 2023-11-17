using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Login { get; set; }

        [Required(ErrorMessage = "password is a required field.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!#]{8,}$", ErrorMessage = " le mot de passe soit composé d'au moins 8 caractères, comprenant au moins une lettre et un chiffre")]

        public string Password { get; set; } = String.Empty;
        public string? Role { get; set; }
        public string? Situation { get; set; }

    }
}
