using PokeApi.Data;
using PokeApi.Interfaces;
using PokeApi.Models;

namespace PokeApi.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _dataContext;
        public ReviewRepository(DataContext context)
        {
            this._dataContext = context;
        }

        public Review GetReview(string id)
        {
            return this._dataContext.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return this._dataContext.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsByPokemon(string pokemonId)
        {
            return this._dataContext.Reviews.Where(r => r.PokemonId == pokemonId).ToList();
        }

        public bool PokemonExist(string pokemonId)
        {
            return this._dataContext.Pokemons.Any(r => r.Id == pokemonId);
        }

        public bool ReviewExist(string id)
        {
            return this._dataContext.Reviews.Any(r => r.Id == id);
        }
    }
}
