using System.ComponentModel.DataAnnotations;

namespace PokeApi.Models
{
    public class PokemonType : BaseModel
    {
        public Pokemon Pokemon { get; set; }
        [Required]
        public string PokemonId { get; set; }
        public Models.Type Type { get; set; }
        [Required]
        public string TypeId { get; set; }
        public float Rarity { get; set; }

        public PokemonType() : base()
        {
            
        }
    }
}
