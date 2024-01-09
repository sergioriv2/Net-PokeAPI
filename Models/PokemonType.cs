namespace PokeApi.Models
{
    public class PokemonType
    {
        public string Id { get; set; }
        public Pokemon Pokemon { get; set; }
        public string PokemonId { get; set; }
        public Models.Type Type { get; set; }
        public string TypeId { get; set; }
        public float Rarity { get; set; }
    }
}
