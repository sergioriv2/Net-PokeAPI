using System.ComponentModel.DataAnnotations;

namespace PokeApi.Dtos.Pokemon
{
    public class CreatePokemonDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string TypeId { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
