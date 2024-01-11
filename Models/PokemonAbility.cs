using System.ComponentModel.DataAnnotations;

namespace PokeApi.Models
{
    public class PokemonAbility : BaseModel
    {
        [Required] 
        public string AbilityId { get; set; }

        [Required]
        public string PokemonId { get; set; }

        public DateTime? LearnedAt { get; set; }

        public Ability Ability { get; set; }
        public Pokemon Pokemon { get; set; }

        public PokemonAbility() : base() { }
    }
}
