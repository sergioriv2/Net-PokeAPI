using PokeApi.Models;

namespace PokeApi.Interfaces
{
    public interface IPokemonRepository
    {
        Task<List<Pokemon>> GetPokemons();

        Task<Pokemon> GetPokemon(string id);
        Task<Pokemon> GetPokemonByName(string name);

        Task<bool> CreatePokemon(string typeId, string trainerId, Pokemon pokemon);

        Task<bool> PokemonExists(string id);

        Task<bool> Save();

    }
}
