using PokeApi.Models;

namespace PokeApi.Interfaces
{
    public interface ITypeRepository
    {
        ICollection<Models.Type> GetTypes();
        Models.Type GetType(string id);
        Models.Type GetTypeByName(string name);
        ICollection<Pokemon> GetPokemonsByType(string typeId);
        bool TypeExists(string id);
    }
}
