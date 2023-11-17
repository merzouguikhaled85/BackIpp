using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto
{
    public class userDto
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Login { get; set; }
        public string? Role { get; set; }
        public string? Password { get; set; }
        public string? Situation { get; set; }
    }

     
}
