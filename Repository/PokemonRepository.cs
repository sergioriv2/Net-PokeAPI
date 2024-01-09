using PokeApi.Data;
using PokeApi.Interfaces;
using PokeApi.Models;

namespace PokeApi.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            this._context = context;
        }

        public Pokemon GetPokemon(string id)
        {
            return _context.Pokemons.Where(x => x.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemonByName(string name)
        {
            return _context.Pokemons.Where(x => x.Name == name).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(string id)
        {
            return _context.Pokemons.Any(p => p.Id == id);
        }
    }
}
