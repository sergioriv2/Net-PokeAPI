using PokeApi.Data;
using PokeApi.Interfaces;
using PokeApi.Models;

namespace PokeApi.Repository
{
    public class AbilityRepository : IAbilityRepository
    {
        private readonly DataContext _context;
        public AbilityRepository(DataContext dataContext)
        {
            this._context = dataContext;
        }

        public bool AbilityExists(string id)
        {
            return this._context.Abilities.Any(a => a.Id == id);
        }

        public ICollection<Ability> GetAbilities()
        {
            return this._context.Abilities.ToList();
        }

        public Ability GetAbility(string id)
        {
            return this._context.Abilities.Where(a => a.Id == id).FirstOrDefault();
        }

        public ICollection<Ability> GetPokemonAbilities(string pokemonId)
        {
            return this._context.PokemonAbilities.Where(pa => pa.PokemonId == pokemonId).Select(pa => pa.Ability).ToList();
        }

        public bool PokemonExist(string pokemonId)
        {
            return this._context.Pokemons.Any(p => p.Id == pokemonId);
        }
    }
}
