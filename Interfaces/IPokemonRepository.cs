using PokeApi.Models;

namespace PokeApi.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();

        Pokemon GetPokemon(string id);
        Pokemon GetPokemonByName(string name);
        bool PokemonExists(string id);

    }
}
