namespace PokeApi.Models
{
    public class Ability
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<PokemonAbility> PokemonAbilities { get; set; }
    }
}
