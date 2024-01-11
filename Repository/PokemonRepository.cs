using Microsoft.EntityFrameworkCore;
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
            _context = context;
        }

        public async Task<bool> CreatePokemon(string typeId, string trainerId, Pokemon pokemon)
        {
            var typeEntity = await _context.Types.Where(e => e.Id == typeId).FirstOrDefaultAsync();
            var PokemonTypeEntity = new PokemonType()
            {
                Pokemon = pokemon,
                Type = typeEntity
            };

            pokemon.TrainerId = trainerId;

            await _context.AddAsync(PokemonTypeEntity);
            await _context.AddAsync(pokemon);
            return await Save();
        }

        public async Task<Pokemon> GetPokemon(string id)
        {
            return await _context.Pokemons.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Pokemon> GetPokemonByName(string name)
        {
            return await _context.Pokemons.Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<Pokemon>> GetPokemons()
        {
            return await _context.Pokemons.ToListAsync();
        }

        public async Task<bool> PokemonExists(string id)
        {
            return await _context.Pokemons.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
