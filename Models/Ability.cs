using System.ComponentModel.DataAnnotations;

namespace PokeApi.Models
{
    public class Ability : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public ICollection<PokemonAbility> PokemonAbilities { get; set; } = new HashSet<PokemonAbility>();

        public Ability() : base() { }
    }
}
