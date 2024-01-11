using System.ComponentModel.DataAnnotations;

namespace PokeApi.Models
{
    public class Type : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public ICollection<PokemonType> PokemonTypes { get; set; } = new List<PokemonType>();

        public Type() : base()
        {
            
        }
    }
}
