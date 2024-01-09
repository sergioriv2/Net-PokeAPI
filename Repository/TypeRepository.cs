using PokeApi.Data;
using PokeApi.Interfaces;
using PokeApi.Models;
using System.Xml.Linq;

namespace PokeApi.Repository
{
    public class TypeRepository : ITypeRepository
    {
        private readonly DataContext _context;

        public TypeRepository(DataContext context)
        {
            this._context = context;
        }

        public ICollection<Pokemon> GetPokemonsByType(string typeId)
        {
           return this._context.PokemonTypes.Where(e => e.TypeId == typeId)
                .Select(e => e.Pokemon).ToList();
        }

        public Models.Type GetType(string id)
        {
            return this._context.Types.Where(t => t.Id == id).FirstOrDefault();
        }

        public Models.Type GetTypeByName(string name)
        {
            return this._context.Types.Where(t => t.Name == name).FirstOrDefault();
        }

        public ICollection<Models.Type> GetTypes()
        {
            return this._context.Types.ToList();
        }

        public bool TypeExists(string id)
        {
            return this._context.Types.Any(t => t.Id == id);
        }
    }
}
