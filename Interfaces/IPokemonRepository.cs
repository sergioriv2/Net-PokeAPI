using PokeApi.Dtos.FileUpload;
using PokeApi.Models;

namespace PokeApi.Interfaces
{
    public interface IPokemonRepository
    {
        Task<List<Pokemon>> GetPokemons();

        Task<Pokemon> GetPokemon(string id);
        Task<Pokemon> GetPokemonByName(string name);

        Task<Pokemon> CreatePokemon(Pokemon pokemon, string typeId);

        Task<Pokemon> UpdatePokemonImage(Pokemon pokemon, FileUploadDto image);

        Task<bool> PokemonExists(string id);

        Task<bool> Save();

    }
}
