namespace PokeApi.Models
{
    public class PokemonAbility
    {
        public string Id { get; set; }
        public string AbilityId { get; set; }
        public Ability Ability { get; set; }
        public Pokemon Pokemon { get; set; }
        public string PokemonId { get; set; }
        public DateTime? LearnedAt { get; set; }
    }
}
