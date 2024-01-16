using System.ComponentModel.DataAnnotations;

namespace PokeApi.Models
{
    public class Pokemon : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string TrainerId { get; set; }

        public string? Image { get; set; }

        public Trainer Trainer { get; set; }
        public DateTime? Birthdate { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<PokemonAbility> PokemonAbilities { get; set; } = new List<PokemonAbility>();

        public ICollection<PokemonType> PokemonTypes { get; set; } = new List<PokemonType>();

        public Pokemon() : base() { }
    }
}
