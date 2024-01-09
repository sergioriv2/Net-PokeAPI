namespace PokeApi.Models
{
    public class Type
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<PokemonType> PokemonTypes { get; set; }
    }
}
