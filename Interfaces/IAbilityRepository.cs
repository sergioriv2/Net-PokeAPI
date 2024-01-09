using PokeApi.Models;

namespace PokeApi.Interfaces
{
    public interface IAbilityRepository
    {
        ICollection<Ability> GetAbilities();
        Ability GetAbility(string id);
        ICollection<Ability> GetPokemonAbilities(string pokemonId);
        bool PokemonExist(string pokemonId);
        bool AbilityExists(string id);
    }
}
