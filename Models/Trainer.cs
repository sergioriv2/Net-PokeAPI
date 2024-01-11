using System.ComponentModel.DataAnnotations;

namespace PokeApi.Models
{
    public class Trainer : BaseModel
    {
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
        public ICollection<RefreshTokens> RefreshTokens { get; set; } = new List<RefreshTokens>();

        public Trainer() : base()
        {
            
        }
    }
}
