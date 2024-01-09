namespace PokeApi.Models
{
    public class Pokemon
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<PokemonAbility> PokemonAbilities { get; set; }

        public ICollection<PokemonType> PokemonTypes { get; set; }
    }
}
